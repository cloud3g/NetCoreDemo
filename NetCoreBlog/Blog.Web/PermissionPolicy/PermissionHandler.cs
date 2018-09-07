using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Blog.Web.Filter;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.PermissionPolicy
{
    public class PermissionHandler : Microsoft.AspNetCore.Authorization.AuthorizationHandler<PermissionRequirement>
    {
        /// <summary>
        /// 用户权限
        /// </summary>
        public List<RolePermissionViewModel> RolePermission { get; set; }
        /// <summary>
        /// 当前方法的名称
        /// </summary>
        private string _actionName = string.Empty;
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            //从AuthorizationHandlerContext转成HttpContext，以便取出表求信息  
            var httpContext = (context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext).HttpContext;
            //是否ajax
            bool isAjaxCall = httpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";
            var isAuthenticated = httpContext.User.Identity.IsAuthenticated;
            //登陆用户为admin 直接跳过
            if(isAuthenticated)
            {
                var currentUser = httpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Name).Value;
                if (currentUser == "admin")
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
            }
            var authorizationFilterContext = context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext;
            //得到Controller类型   
            Type t = (authorizationFilterContext.ActionDescriptor as ControllerActionDescriptor).ControllerTypeInfo;
            //得到方法名    
            string actionName = authorizationFilterContext.ActionDescriptor.RouteValues["action"].ToString();
            //得到控制器名
            string controllerName = authorizationFilterContext.ActionDescriptor.RouteValues["controller"].ToString();
            //得到区域名
            string areaName = authorizationFilterContext.ActionDescriptor.RouteValues["area"] == null ? "" : authorizationFilterContext.RouteData.Values["area"].ToString();
            
 
            //获取自定义的特性    
            var actionAttribute = (authorizationFilterContext.ActionDescriptor as ControllerActionDescriptor).MethodInfo.GetCustomAttributes(typeof(SetActionAttribute), false).FirstOrDefault() as SetActionAttribute;
             
            _actionName = actionAttribute == null ? actionName : actionAttribute.ActionName;

           
            //请求Url
            var questUrl = httpContext.Request.Path.Value.ToLower();
            //是否经过验证
            
            if (isAuthenticated)
            {
                if (controllerName.ToLower() == "home"  && areaName.ToLower() == "admin")
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
                bool hasCurrentControllerRole = RolePermission.GroupBy(g => new { ControllerName = g.ControllerName, ActionName = g.ActionName, AreaName = g.AreaName }).Where(w => w.Key.ActionName.ToLower() == _actionName.ToLower() && w.Key.AreaName.ToLower() == areaName.ToLower() && w.Key.ControllerName.ToLower() == controllerName.ToLower()).Count() > 0;
                if (hasCurrentControllerRole)
                {
                    //当前用户角色名
                    var roleName = httpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Role).Value.Split(',');
                    if (RolePermission.Where(w => roleName.Contains(w.RoleName) && w.ControllerName.ToLower() == controllerName.ToLower() && w.ActionName == _actionName.ToLower() && w.AreaName == areaName.ToLower()).Count() > 0)
                    {
                        //有权限标记处理成功
                        context.Succeed(requirement);
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
