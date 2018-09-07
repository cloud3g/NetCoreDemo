using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Blog.Web.Models;
using Blog.IService;
using Blog.Common;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AccountController : Controller
    {
        public ISysUserInfoService SysUserInfoService { get; set; }
        private ISysUserService SysUserService { get; set; }
        private ISysUserRoleService SysUserRoleService { get; set; }
        public AccountController(ISysUserInfoService sysUserInfoService, ISysUserService sysUserService, ISysUserRoleService sysUserRoleService)
        {
            SysUserInfoService = sysUserInfoService;
            SysUserService = sysUserService;
            SysUserRoleService = sysUserRoleService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
#if DEBUG
            //var model = new AccountViewModel() { LoginName="admin", Password="123456" };
            //var userinfo = SysUserService.Find(u => u.LoginName == model.LoginName && u.Password == model.Password);
            //var roles = string.Join(",", SysUserRoleService.GetListJoin(s => s.Enable == true && s.UserId == userinfo.Id, new string[] { "SysRole" }).Select(s => s.SysRole.Name).ToArray());
            //var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            //identity.AddClaim(new Claim(ClaimTypes.Sid, model.LoginName));
            //identity.AddClaim(new Claim("RealName", userinfo.RealName));
            //identity.AddClaim(new Claim(ClaimTypes.Name, userinfo.LoginName));
            //identity.AddClaim(new Claim(ClaimTypes.Role, roles));
            //HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

#endif
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("index", "home", new { area = "admin" });
            else
                return View();
            
        }
        [HttpGet]
        public IActionResult Login()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AccountViewModel model)
        {
            Response res = null;
            //这里密码可以加密一下
            var userinfo = SysUserService.Find(u => u.LoginName == model.LoginName && u.Password == model.Password);
            if(userinfo==null)
            {
                //登陆失败可以记录一下登陆时间跟失败次数，限制多少分钟内不能再登陆
                res = new Common.Response() { Code= ResponseCode.Fail, Message="用户名或密码错误！" };
            }
            else
            {
                //获取用户所有角色
                var roles = string.Join(",", SysUserRoleService.GetListJoin(s => s.Enable == true&&s.UserId==userinfo.Id, new string[] { "SysRole" }).Select(s => s.SysRole.Name).ToArray());
                
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Sid, model.LoginName));
                identity.AddClaim(new Claim(ClaimTypes.Name, userinfo.LoginName));
                identity.AddClaim(new Claim("RealName", userinfo.RealName));
                identity.AddClaim(new Claim(ClaimTypes.Role, roles));
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
      
                res = new Common.Response() { Code = ResponseCode.Success, Message = "登陆成功！" };
               // HttpContext.Session.SetObjectAsJson("account", model);
               
            }
            return Json(res);
        }
        public async Task<IActionResult> LoginOut()
        {
            await HttpContext.SignOutAsync();
            return View("Index");
        }
        [AllowAnonymous]
        public IActionResult Denied(string isAjax)
        {
            var b= HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";
            if (string.IsNullOrEmpty(isAjax))
            {
                return View();
            }
            else
            {
                return Json(new Response() { Code= ResponseCode.Fail, Message="操作失败,不具备权限" });
            }
            
        }
    }
}
