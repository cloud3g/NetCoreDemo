using Blog.IService;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.PermissionPolicy
{
    public static class RolePermissionInit
    {

        public static void Init(IServiceProvider app)
        {

            PermissionHandler _permissionHandler = app.GetRequiredService<IAuthorizationHandler>() as PermissionHandler;
            using (BlogDbContext db = app.GetRequiredService<BlogDbContext>())
            {
                var roleList = db.SysRoleOperate.Include(s => s.SysRole);
                _permissionHandler.RolePermission = (from r in roleList
                                                     select new RolePermissionViewModel
                                                     {
                                                         ActionName = r.Action,
                                                         AreaName = r.Area,
                                                         ControllerName = r.Controller,
                                                         RoleName = r.SysRole.Name
                                                     }).ToList();
            }
            //var permlist = db.RolePermission.Include(r => r.SysRole).Include(r => r.SysModule);
            //var actionlist = db.SysModuleAction.Include(r => r.SysModule);

            //var list = (from p in permlist
            //           join a in actionlist on p.ModuleId equals a.ModuleId into gc
            //           from gci in gc.DefaultIfEmpty()
            //           select new RolePermissionViewModel
            //           {
            //               ActionName = gci.Code,
            //               ControllerName = p.SysModule.ControllerName,
            //               AreaName = p.SysModule.AreaName,
            //               RoleName = p.SysRole.Name
            //           }).ToList();

        }
    }
}
