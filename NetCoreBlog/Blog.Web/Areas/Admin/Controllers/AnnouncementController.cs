using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Web.Core;
using Blog.IService;
using Blog.Common;
using Blog.Models;
using Blog.Web.Filter;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AnnouncementController : AdminBaseController
    {
        IBlogAnnouncementService BlogAnnouncementService { get; set; }

        public AnnouncementController(IBlogAnnouncementService blogAnnouncementService)
        {
            BlogAnnouncementService = blogAnnouncementService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        #region 获取列表
        [SetAction(ActionName ="index")]
        public IActionResult List(GridPager<BlogAnnouncement> pager)
        {
            List<BlogAnnouncement> list = null;
            pager.sort = "CreateTime";
            pager.order = "desc";
            list = BlogAnnouncementService.GetPageList(pager, b => true).Entities;
            var rows = from b in list
                       select new
                       {
                           b.Id,
                           b.CreateTime,
                           b.Content,
                           b.Level,
                           b.Enable
                       };
            return Json(new { code = 0, count = pager.totalRows, data = rows });
        }
        #endregion

        #region 添加

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [SetAction(ActionName ="save")]
        public IActionResult Add(Blog.Models.BlogAnnouncement model)
        {
            Response res = null;
            if (ModelState.IsValid)
            {
                model.CreateTime = DateTime.Now;
                model.UpdateTime = DateTime.Now;

                res = BlogAnnouncementService.Add(model);
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
        public IActionResult Edit(int id)
        {
            var entity = BlogAnnouncementService.Find(f => f.Id == id);
            if (entity == null)
            {
                return Content("没有找到编辑的项！");
            }
            return View(entity);
        }
        [HttpPost]
        [SetAction(ActionName = "save")]
        public IActionResult Edit(BlogAnnouncement entity)
        {
            Response res = null;
            if (ModelState.IsValid)
            {
                entity.UpdateTime = DateTime.Now;
                res = BlogAnnouncementService.Edit(entity);
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
        public IActionResult Delete(int id) => Json(BlogAnnouncementService.Delete(id));
        #endregion

        #region 设置状态
        [SetAction(ActionName = "setstatus")]
        public IActionResult SetEnable(int id)
        {
            Response res = null;
            var entity = BlogAnnouncementService.Find(f => f.Id == id);
            if (entity == null)
            {
                res = new Common.Response() { Code = ResponseCode.Fail, Message = "没有找到相关数据！" };
            }
            else
            {
                entity.Enable = !entity.Enable;
                res = BlogAnnouncementService.Edit(entity);
            }
            return Json(res);
        }
        #endregion
    }
}
