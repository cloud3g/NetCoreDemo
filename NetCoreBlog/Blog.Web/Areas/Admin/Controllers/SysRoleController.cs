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
using Blog.Web.PermissionPolicy;
using Microsoft.AspNetCore.Authorization;
using Blog.Web.Core;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class SysRoleController : AdminBaseController
    {

        private ISysRoleService _sysRoleService;
        private ISysModuleActionService _sysModuleActionService;
        private ISysModuleService _sysModuleService;
        private ISysRoleOperateService _sysRoleOperateService;
        private PermissionHandler _permissionHandler;
        public SysRoleController(ISysRoleService sysRoleService, ISysModuleActionService sysModuleActionService, ISysRoleOperateService sysRoleOperateService, IAuthorizationHandler permissionHandler, ISysModuleService sysModuleService)
        {
            _sysRoleService = sysRoleService;
            _sysModuleActionService = sysModuleActionService;
            _sysRoleOperateService = sysRoleOperateService;
            _permissionHandler = permissionHandler as PermissionHandler; ;
            _sysModuleService = sysModuleService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {

            return View();
        }
        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pager">分页类</param>
        /// <returns></returns>
        [SetAction(ActionName = "index")]
        public IActionResult RoleList(GridPager<SysRole> pager)
        {
            List<SysRole> list = null;
            pager.sort = "CreateTime";
            pager.order = "desc";
            list = _sysRoleService.GetPageList(pager, b => true).Entities;
            var rows = from b in list
                       select new
                       {
                           b.Id,
                           b.CreateTime,
                           b.Remark,
                           b.Name
                       };
            return Json(new { code = 0, count = pager.totalRows, data = rows });

        }
        [SetAction(ActionName = "index")]
        public IActionResult ActionList(int moduleId, int roleId)
        {
            var actionList = _sysModuleActionService.GetListJoin(a => a.ModuleId == moduleId, new[] { "SysModule" });
            var roleAction = _sysRoleOperateService.GetList(r => r.RoleId == roleId);
            var list = (from a in actionList
                        join r in roleAction on new { Action = a.Code, Area = a.SysModule.AreaName, Controller = a.SysModule.ControllerName } equals new { r.Action, r.Area, r.Controller } into temp
                        from tt in temp.DefaultIfEmpty()
                        select new
                        {
                            Id = a.Id,
                            Code = a.Code,
                            Name = a.Name,
                            Checked = tt == null ? false : true
                        }).ToList();
            return Json(new { code = 0, data = list });
        }
        [SetAction(ActionName = "index")]
        public IActionResult ModuleList()
        {
            var list = _sysModuleService.GetList(c => true);
            var rows = from s in list
                       select new
                       {
                           id = s.Id,
                           s.Name,
                           pid = s.Pid,
                           s.AreaName,
                           s.ControllerName,
                           s.Type,
                           s.IsSysMenu
                       };
            return Json(new { code = 0, data = rows });
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
        public IActionResult Add(SysRole entity)
        {
            Response res;
            if (ModelState.IsValid)
            {
                var model = _sysRoleService.Find(s => entity.Name == s.Name);
                if (model != null)
                {
                    res = new Common.Response() { Code = ResponseCode.Fail, Message = "角色名已存在！" };
                }
                else
                {
                    entity.CreateTime = DateTime.Now;
                    entity.UpdateTime = DateTime.Now;
                    res = _sysRoleService.Add(entity);
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
        public IActionResult Edit(int id) => PartialView(_sysRoleService.Find(s => s.Id == id));
        [HttpPost]
        [SetAction(ActionName = "save")]
        public IActionResult Edit(SysRole entity, string oldName)
        {
            Response res;
            if (ModelState.IsValid)
            {
                if (entity.Name != oldName)
                {
                    var model = _sysRoleService.Find(s => entity.Name == s.Name);
                    if (model != null)
                    {
                        res = new Common.Response() { Code = ResponseCode.Fail, Message = "角色名已存在！" };
                    }
                    else
                    {
                        entity.UpdateTime = DateTime.Now;
                        res = _sysRoleService.Edit(entity);
                    }
                }
                else
                {
                    entity.UpdateTime = DateTime.Now;
                    res = _sysRoleService.Edit(entity);
                }


            }
            else
            {
                res = new Common.Response() { Code = ResponseCode.Fail, Message = Utils.ModelStateMessage(ModelState) };
            }
            return Json(res);
        }
        #endregion

        [HttpPost]
        public IActionResult Delete(int id) => Json(_sysRoleService.Delete(id));
        #region 分配权限
        public IActionResult Allot(int roleId, int moduleId, string actionIds)
        {
            Response res;
            List<int> actionIdsList = new List<int>();
            if (!string.IsNullOrEmpty(actionIds))
            {
                foreach (var item in actionIds.Split(","))
                {
                    actionIdsList.Add(Convert.ToInt32(item));
                }

            }
            if (_sysRoleService.Allot(roleId, moduleId, actionIdsList))
            {
                var role = _sysRoleService.Find(r => r.Id == roleId);
                var module = _sysModuleService.Find(m => m.Id == moduleId);
                var actions = _sysModuleActionService.GetList(s => actionIdsList.Contains(s.Id));
                for (int i = _permissionHandler.RolePermission.Count - 1; i >= 0; i--)
                {
                    if (_permissionHandler.RolePermission[i].RoleName == role.Name && _permissionHandler.RolePermission[i].AreaName == module.AreaName && _permissionHandler.RolePermission[i].ControllerName == module.ControllerName)
                    {
                        _permissionHandler.RolePermission.RemoveAt(i);
                    }
                }
                foreach (var item in actions)
                {
                    _permissionHandler.RolePermission.Add(new RolePermissionViewModel() { RoleName = role.Name, ActionName = item.Code, AreaName = module.AreaName, ControllerName = module.ControllerName });
                }

                res = new Common.Response() { Code = ResponseCode.Success, Message = "设置成功!" };
            }
            else
                res = new Common.Response() { Code = ResponseCode.Fail, Message = "设置失败,请联系管理员！" };
            return Json(res);
        }
        #endregion


    }
}
