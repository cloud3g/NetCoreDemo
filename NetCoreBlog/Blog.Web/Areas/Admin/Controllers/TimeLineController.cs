using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.IService;
using Blog.Common;
using Blog.Models;
using Blog.Web.Core;
using Blog.Web.Filter;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class TimeLineController : AdminBaseController
    {
        ITimeLineService _service;
        public TimeLineController(ITimeLineService service)
        {
            _service = service;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        #region 获取列表
        [SetAction(ActionName ="index")]
        public IActionResult List(GridPager<TimeLine> pager)
        {
            List<TimeLine> list = null;
            pager.sort = "CreateTime";
            pager.order = "desc";

            list = _service.GetPageList(pager, b => true).Entities;
            var rows = from b in list
                       select new
                       {
                           b.Id,
                           b.CreateTime,
                           b.Content
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
        [SetAction(ActionName = "save")]
        public IActionResult Add(TimeLine model)
        {
            Response res = null;
            if (ModelState.IsValid)
            {
                model.CreateTime = DateTime.Now;
                model.UpdateTime = DateTime.Now;

                res = _service.Add(model);
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
            var entity = _service.Find(f => f.Id == id);
            if (entity == null)
            {
                return Content("没有找到编辑的项！");
            }
            return PartialView(entity);
        }
        [HttpPost]
        [SetAction(ActionName = "save")]
        public IActionResult Edit(TimeLine entity)
        {
            Response res = null;
            if (ModelState.IsValid)
            {
                entity.UpdateTime = DateTime.Now;
                res = _service.Edit(entity);
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
        public IActionResult Delete(int id) => Json(_service.Delete(id));
        #endregion
    }
}
