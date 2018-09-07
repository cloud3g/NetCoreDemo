using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Web.Models;
using Blog.IService;
using Blog.Models;
using Blog.Common;
using Blog.Web.Core;
using Blog.Web.Filter;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class CategoryController : AdminBaseController
    {
        IBlogCategoryService BlogCategoryService { get; set; }
        public CategoryController(IBlogCategoryService blogCategoryService)
        {
            BlogCategoryService = blogCategoryService;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region 获取列表
        [SetAction(ActionName ="index")]
        public IActionResult GetList()
        {
            var list = BlogCategoryService.GetList(c => true);
            var rows = from c in list
                       select new
                       {
                           id = c.Id,
                           name = c.Name,
                           pid = c.Pid,
                           type = c.CategoryType,
                           remark = c.Remark,
                           imgUrl = c.ImgUrl,
                           enable = c.Enable,
                           isNav = c.IsNav
                       };
            return Json(rows);
        }
        [SetAction(ActionName = "index")]
        public IActionResult GetSelectItem()
        {
            var list = BlogCategoryService.GetList(c => c.Pid == 0 && c.Enable == true);
            return Json(list);
        }
        #endregion

        #region 添加
        [HttpGet]
        public IActionResult Add(int id)
        {
            ViewBag.selectId = id;
            return View();
        }
        [HttpPost]
        [SetAction(ActionName = "save")]
        public IActionResult Add(BlogCategory entity)
        {

            Response res = null;
            if (ModelState.IsValid)
            {
                entity.Id = BlogCategoryService.GetList(c => true).Count() + 1;
                entity.CreateTime = DateTime.Now;
                entity.UpdateTime = DateTime.Now;
                entity.Pid = entity.Pid ?? 0;
                res = BlogCategoryService.Add(entity);
            }
            else
            {
                res = new Common.Response() { Code = ResponseCode.Fail, Message = Utils.ModelStateMessage(ModelState) };
            }
            return Json(res);
        }
        #endregion

        #region 设置状态
        [SetAction(ActionName = "setstatus")]
        public IActionResult SetNav(int id, bool value)
        {
            Response res = null;
            if (BlogCategoryService.SetNav(id, value))
            {
                res = new Common.Response() { Code = ResponseCode.Success, Message = "设置成功！", Value = value ? "1" : "0" };
            }
            else
            {
                res = new Common.Response() { Code = ResponseCode.Success, Message = "设置失败,请联系管理员！" };
            }
            return Json(res);
        }
        [SetAction(ActionName = "setstatus")]
        public IActionResult SetEnable(int id, bool value)
        {
            Response res = null;
            if (BlogCategoryService.SetEnable(id, value))
            {
                res = new Common.Response() { Code = ResponseCode.Success, Message = "设置成功！" };
            }
            else
            {
                res = new Common.Response() { Code = ResponseCode.Success, Message = "设置失败,请联系管理员！" };
            }
            return Json(res);
        }
        #endregion

        #region 编辑
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var entity = BlogCategoryService.Find(c => c.Id == id);
            if (entity == null)
            {
                return Json(new Response() { Code= ResponseCode.Fail, Message="没有找到相关内容!" });
            }
            return View(entity);
        }
        [HttpPost]
        [SetAction(ActionName = "save")]
        public IActionResult Edit(BlogCategory blogCategory)
        {
            Response res = null;
            if (ModelState.IsValid)
            {
                blogCategory.UpdateTime = DateTime.Now;
                res = BlogCategoryService.Edit(blogCategory);
            }
            else
            {
                res = new Common.Response() { Code= ResponseCode.Fail, Message=Utils.ModelStateMessage(ModelState) };
            }
            return Json(res);
        }
        #endregion

        #region 删除
        public IActionResult Delete(int id)
        {
            var res = BlogCategoryService.CascadingDeletion(id);
            return Json(res);
        }
        #endregion

    }
}
