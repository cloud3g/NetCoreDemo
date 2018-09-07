using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Web.Models;
using Blog.Web.Core;
using Microsoft.AspNetCore.Authorization;
using Blog.IService;
using Microsoft.AspNetCore.Mvc.Rendering;
using Blog.Models;
using Blog.Models.Enum;
using Blog.Common;
using Blog.Web.Filter;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ModuleController : AdminBaseController
    {
        ISysModuleService SysModuleService { get; set; }
        ISysModuleActionService SysModuleActionService { get; set; }
        public ModuleController(ISysModuleService _sysModuleService, ISysModuleActionService _sysModuleActionService)
        {
            SysModuleService = _sysModuleService;
            SysModuleActionService = _sysModuleActionService;
        }

        public IActionResult Index()
        {

            return View();
        }
        #region 获取列表
        /// <summary>
        /// 获取module列表
        /// </summary>
        /// <returns></returns>
        [SetAction(ActionName = "index")]
        public IActionResult List()
        {
            var list = SysModuleService.GetList(c => true);
            var rows = from s in list
                       select new
                       {
                           id = s.Id,
                           s.Name,
                           pid = s.Pid,
                           s.AreaName,
                           s.ControllerName,
                           s.Type,
                           s.IsSysMenu,
                           s.Sort
                       };
            
            return Json(new { code = 0, data = rows });
        }
        /// <summary>
        /// 获取模块操作列表
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        [SetAction(ActionName = "index")]
        public IActionResult ActionList(int moduleId)
        {
            var list = SysModuleActionService.GetList(a => a.ModuleId == moduleId).ToList();
            return Json(new { code = 0, data = list });
        }
        [SetAction(ActionName = "index")]
        public IActionResult GetSeleteItem()
        {
            var list = SysModuleService.GetList(s => s.Type == Blog.Models.Enum.ModuleType.Catelog && s.IsSysMenu == true);
            var moduleList = (from s in list
                              select new SelectListItem()
                              {
                                  Text = s.Name,
                                  Value = s.Id.ToString()
                              }).ToList();
            return Json(moduleList);
        }
        #endregion

        #region 添加
        [HttpGet]
        [SetAction(ActionName = "add")]
        public IActionResult AddModule(int id)
        {

            return PartialView();
        }
        [HttpPost]
        [SetAction(ActionName = "save")]
        public IActionResult AddModule(SysModule module)
        {
            Response res = null;
            if (module.Type == ModuleType.Catelog)
            {
                module.ControllerName = "";
                module.AreaName = "";
                module.CreateTime = DateTime.Now;
                module.UpdateTime = DateTime.Now;
                res = SysModuleService.Add(module);
            }
            else
            {
                module.AreaName = module.AreaName.ToLower();
                module.ControllerName = module.ControllerName.ToLower();
                module.CreateTime = DateTime.Now;
                module.UpdateTime = DateTime.Now;
                var entity = SysModuleService.Find(s => s.AreaName == module.AreaName && s.ControllerName == module.ControllerName);
                if (entity != null)
                {
                    res = new Common.Response() { Code = ResponseCode.Fail, Message = "当前area下已存在controller" };
                }
                else
                    res = SysModuleService.Add(module);
            }
            return Json(res);
        }

        [HttpGet]
        [SetAction(ActionName = "add")]
        public IActionResult AddAction(int moduleId)
        {
            ViewBag.moduleId = moduleId;
            return PartialView();
        }
        [HttpPost]
        [SetAction(ActionName = "save")]
        public IActionResult AddAction(SysModuleAction entity)
        {
            Response res;
            if (ModelState.IsValid)
            {
                var model = SysModuleActionService.Find(s => s.Code == entity.Code.ToLower() && entity.ModuleId == s.ModuleId);
                if (model != null)
                {
                    res = new Common.Response() { Code = ResponseCode.Fail, Message = "该模块下已存在" + entity.Code };
                }
                else
                {
                    entity.CreateTime = DateTime.Now;
                    entity.UpdateTime = DateTime.Now;
                    entity.Code = entity.Code.ToLower();
                    res = SysModuleActionService.Add(entity);
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
        [SetAction(ActionName = "edit")]
        public IActionResult EditModule(int id)
        {
            var entity = SysModuleService.Find(s => s.Id == id);
            return PartialView(entity);
        }
        [HttpPost]
        [SetAction(ActionName = "save")]
        public IActionResult EditModule(SysModule entity)
        {
            Response res;
            if (ModelState.IsValid)
            {
                entity.UpdateTime = DateTime.Now;
                res = SysModuleService.Edit(entity);
            }
            else
            {
                res = new Common.Response() { Code = ResponseCode.Fail, Message = Utils.ModelStateMessage(ModelState) };
            }
            return Json(res);
        }
        [HttpGet]
        [SetAction(ActionName = "edit")]
        public IActionResult EditAction(int id)
        {
            var entity = SysModuleActionService.Find(s => s.Id == id);
            return PartialView(entity);
        }
        [HttpPost]
        [SetAction(ActionName = "save")]
        public IActionResult EditAction(SysModuleAction entity)
        {
            Response res;
            if (ModelState.IsValid)
            {
                entity.UpdateTime = DateTime.Now;
                res = SysModuleActionService.Edit(entity);
            }
            else
            {
                res = new Common.Response() { Code = ResponseCode.Fail, Message = Utils.ModelStateMessage(ModelState) };
            }
            return Json(res);
        }
        #endregion

        #region 设置状态
        [HttpPost]
        [SetAction(ActionName = "setstatus")]
        public IActionResult SetMenu(int id)
        {
            var entity = SysModuleService.Find(s => s.Id == id);
            Response res;
            if (entity == null)
            {
                res = new Common.Response() { Code = ResponseCode.Fail, Message = "没有找到相关选项!" };
            }
            else
            {
                entity.IsSysMenu = !entity.IsSysMenu;
                res = SysModuleService.Edit(entity);
            }
            return Json(res);
        }
        [HttpPost]
        [SetAction(ActionName = "setstatus")]
        public IActionResult SetActionEnable(int id)
        {
            var entity = SysModuleActionService.Find(s => s.Id == id);
            Response res;
            if (entity == null)
            {
                res = new Common.Response() { Code = ResponseCode.Fail, Message = "没有找到相关选项!" };
            }
            else
            {
                entity.Enable = !entity.Enable;
                res = SysModuleActionService.Edit(entity);
            }
            return Json(res);
        }
        #endregion


        #region 删除
        [HttpPost]
        public IActionResult Delete(int id) => Json(SysModuleService.Delete(id));
        [HttpPost]
        [SetAction(ActionName = "delete")]
        public IActionResult DeleteAction(int id) => Json(SysModuleActionService.Delete(id));
        #endregion



    }
}
