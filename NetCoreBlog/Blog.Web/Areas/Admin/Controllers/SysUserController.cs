using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.IService;
using Blog.Web.Filter;
using Blog.Common;
using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Blog.Web.Core;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class SysUserController : AdminBaseController
    {
        private ISysUserService _sysUserService;
        private ISysUserRoleService _sysUserRoleService;
        private ISysRoleService _sysRoleService;
        public SysUserController(ISysUserService sysUserService, ISysUserRoleService sysUserRoleService, ISysRoleService sysRoleService)
        {
            _sysUserService = sysUserService;
            _sysUserRoleService = sysUserRoleService;
            _sysRoleService = sysRoleService;


        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        #region 列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pager">分页类</param>
        /// <returns></returns>
        [SetAction(ActionName = "index")]
        public IActionResult GetList(GridPager<SysUser> pager)
        {

            var userList = _sysUserService.GetPageList(pager.page, pager.rows, out int total, u => true, new OrderModelField() { IsDESC = true, PropertyName = "Id" });

            var roleList = _sysUserRoleService.GetList(s => true).Include(s => s.SysRole).Include(s => s.SysUser);

            var list = (from u in userList
                        join r in roleList on u.Id equals r.SysUser.Id into gc
                        from gci in gc.DefaultIfEmpty()
                        select new
                        {
                            Id = u.Id,
                            LoginName = u.LoginName,
                            RealName = u.RealName,
                            IP = u.IP,
                            Password = u.Password,
                            Role = gci.SysRole.Name
                        })
                        .GroupBy(s => new { s.Id, s.LoginName, s.RealName, s.IP, s.Password })
                        .Select(g => new { Id = g.Key.Id, LoginName = g.Key.LoginName, RealName = g.Key.RealName, IP = g.Key.IP, Password = g.Key.Password, Roles = string.Join(",", g.Select(s => s.Role).ToArray()) }).Distinct();

            return Json(new { code = 0, count = total, data = list });

        }
        #endregion

        #region 添加
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView();
        }
        [HttpPost]
        [SetAction(ActionName = "save")]
        public IActionResult Add(SysUser entity)
        {
            Response res;
            if (ModelState.IsValid)
            {
                var model = _sysUserService.Find(s => s.LoginName == entity.LoginName);
                if (model != null)
                {
                    res = new Common.Response() { Code = ResponseCode.Fail, Message = "用户名已存在！" };
                }
                else
                {
                    entity.CreateTime = DateTime.Now;
                    entity.UpdateTime = DateTime.Now;
                    entity.Status = 1;//预留
                    res = _sysUserService.Add(entity);
                }

            }
            else
            {
                res = new Common.Response() { Code = ResponseCode.Fail, Message = Utils.ModelStateMessage(ModelState) };
            }
            return Json(res);
        }
        #endregion

        #region 编辑
        [HttpGet]
        public IActionResult Edit(int id) => PartialView(_sysUserService.Find(s => s.Id == id));
        [HttpPost]
        [SetAction(ActionName = "save")]
        public IActionResult Edit(SysUser entity)
        {
            Response res;
            if (ModelState.IsValid)
            {
                entity.UpdateTime = DateTime.Now;
                entity.Status = 1;//预留
                res = _sysUserService.Edit(entity);
            }
            else
            {
                res = new Common.Response() { Code = ResponseCode.Fail, Message = Utils.ModelStateMessage(ModelState) };
            }
            return Json(res);
        }
        #endregion

        #region 删除
        [HttpPost]
        public IActionResult Delete(int id) => Json(_sysUserService.Delete(id));
        #endregion

        #region 分配角色
        [HttpGet]
        [SetAction(ActionName ="allot")]
        public IActionResult AllotRole(int userId)
        {

            var entity = _sysUserService.FindJoin(s => s.Id == userId, new string[] { "SysUserRoles" });

            ViewBag.userName = entity.RealName;
            var userRoleId = entity.SysUserRoles.Select(u => u.RoleId);
            var allRole = _sysRoleService.GetListJoin(r => true, new string[] { "SysUserRoles" });
            var list = (from r in allRole
                        select new SelectListItem
                        {

                            Value = r.Id.ToString(),
                            Text = r.Name,
                            Selected = userRoleId.Contains(r.Id) ? true : false
                        }).ToList();
            ViewBag.roleList = list;
            return PartialView(entity);
        }
        [HttpPost]
        [SetAction(ActionName = "allot")]
        public IActionResult AllotRole(string roleIds, int userId)
        {
            var list = new List<int>();
            foreach (var item in roleIds.Split(','))
            {
                list.Add(Convert.ToInt32(item));
            }

            return Json(_sysUserService.AllotRole(list, userId));
        }
        #endregion
    }
}
