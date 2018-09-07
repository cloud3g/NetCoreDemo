using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Web.Core;
using Blog.Web.Models;
using Blog.Common;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class BlogSettingsController : AdminBaseController
    {
        private IWritableOptions<BlogSettingsViewModel> _option;
        public BlogSettingsController(IWritableOptions<BlogSettingsViewModel> option)
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
        public IActionResult Save(BlogSettingsViewModel model)
        {
            
            try
            {
                //这里会导致多线程操作同个文件
                _option.Update(opt => {
                    opt.Title = model.Title;
                    opt.Logo = model.Logo;
                    opt.Subtitle = model.Subtitle;
                    opt.Url = model.Url;
                    opt.Abstract = model.Abstract;
                    opt.Keywords = model.Keywords;
                    opt.Description = model.Description;
                    opt.LogoText = model.LogoText;
                    opt.QQAppId = model.QQAppId;
                    opt.Statistics = model.Statistics;
                    opt.QQAppKey = model.QQAppKey;
                });
                return Json(new Response() { Code = ResponseCode.Success, Message = "保存成功!" });
            }
            catch(Exception ex)
            {
                return Json(new Response() { Code = ResponseCode.Fail, Message = ex.Message });
            }
            
        }

        
    }
}
