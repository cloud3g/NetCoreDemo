using Blog.Web.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Common;
using Blog.IService;

namespace Blog.Web.Core
{
    public class PermissionFilterAttribute: ActionFilterAttribute
    {
        public ISysUserInfoService SysUserInfoService { get; set; }
        private string _actionName;
        public PermissionFilterAttribute(ISysUserInfoService sysUserInfoService,string name=null)
        {
            SysUserInfoService = sysUserInfoService;
            _actionName = name;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            bool isAjaxCall = context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";
            if(isAjaxCall)
            {
                return;
            }
            string controllerName = context.RouteData.Values["controller"].ToString();
            string areaName = context.RouteData.Values["area"].ToString();
            string actionName = context.RouteData.Values["action"].ToString();
            AccountViewModel account = context.HttpContext.Session.GetObjectFromJson<AccountViewModel>("account");
            if (account.LoginName == "admin")
            {
                return;
            }
            var permList = SysUserInfoService.GetPermission(account.Id,areaName, controllerName);
            
            base.OnActionExecuting(context);
        }
    }
}
