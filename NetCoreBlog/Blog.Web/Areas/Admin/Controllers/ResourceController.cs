using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using Blog.Common;
using Blog.Models;
using Blog.Web.Core;
using Blog.Web.Filter;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ResourceController : AdminBaseController
    {
        Blog.IService.IResourceService ResourceService { get; set; }
        public ResourceController(Blog.IService.IResourceService resourceService)
        {
            ResourceService = resourceService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        #region 获取列表
        [SetAction(ActionName ="index")]
        public IActionResult List(GridPager<Resource> pager)
        {
            List<Resource> list = null;
            pager.sort = "CreateTime";
            pager.order = "desc";
         
            list = ResourceService.GetPageList(pager, b => true).Entities;
            var rows = from b in list
                       select new
                       {
                           b.Id,
                           b.Name,
                           b.CreateTime,
                           b.Abstract,
                           b.Submitter,
                           b.Type
                       };
            return Json(new { code = 0, count = pager.totalRows, data = rows });
        }
        #endregion


        #region 添加

        [HttpGet]
        public IActionResult Add()
        {
            return PartialView();
        }
        [HttpPost]
        [SetAction(ActionName ="save")]
        public IActionResult Add(Blog.Models.Resource model)
        {
            Response res = null;
            if (ModelState.IsValid)
            {
                model.CreateTime = DateTime.Now;
                model.UpdateTime = DateTime.Now;
                
                res = ResourceService.Add(model);
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
            var entity = ResourceService.Find(f => f.Id == id);
            if (entity == null)
            {
                return Content("没有找到编辑的项！");
            }
            return PartialView(entity);
        }
        [HttpPost]
        [SetAction(ActionName = "save")]
        public IActionResult Edit(Resource entity)
        {
            Response res = null;
            if (ModelState.IsValid)
            {
                entity.UpdateTime = DateTime.Now;
                res = ResourceService.Edit(entity);
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
        public IActionResult Delete(int id) => Json(ResourceService.Delete(id));
        #endregion
    }
}
