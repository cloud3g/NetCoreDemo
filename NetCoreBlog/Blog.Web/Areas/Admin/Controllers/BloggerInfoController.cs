using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Common;
using Blog.Web.Models;
using Blog.Web.Core;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class BloggerInfoController : AdminBaseController
    {
        private IWritableOptions<BloggerInfoViewModel> _option;
        public BloggerInfoController(IWritableOptions<BloggerInfoViewModel> option)
        {
            _option = option;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var entity = _option.Value;
            return View(entity);
        }
        /// <summary>
        /// 保存网站设置,这里保存到json文件里面。
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Save(BloggerInfoViewModel model)
        {

            try
            {
                //这里会导致多线程操作同个文件
                _option.Update(opt => {
                    opt.Name = model.Name;
                    opt.Introduction = model.Introduction;
                    opt.Icon = model.Icon;
                    opt.QQ = model.QQ;
                    opt.Weibo = model.Weibo;
                    opt.Git = model.Git;
                    opt.Address = model.Address;
                    opt.Email = model.Email;
                });
                return Json(new Response() { Code = ResponseCode.Success, Message = "保存成功!" });
            }
            catch (Exception ex)
            {
                return Json(new Response() { Code = ResponseCode.Fail, Message = ex.Message });
            }

        }
    }
}
