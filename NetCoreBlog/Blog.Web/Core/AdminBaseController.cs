using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Blog.Web.Models;
using Blog.Common;
using Microsoft.Net.Http.Headers;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Core
{
    [Microsoft.AspNetCore.Authorization.Authorize(Policy = "Permission")]
    public class AdminBaseController : Controller
    {
        

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            bool isAjaxCall = context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";

            //if (!HttpContext.User.Identity.IsAuthenticated)
            //{
            //    if (isAjaxCall)
            //    {
            //        context.HttpContext.Response.Headers.Add("sessionstatus", "timeout");
            //        context.Result = new EmptyResult();
            //    }
            //    else
            //    {
            //        context.Result = RedirectToAction("index", "account", new { area = "admin" });
            //        return;
            //    }

            //}
            //防止直接在地址栏输入地址访问
            //var url = context.HttpContext.Request.Path.ToString();
            //if (!string.Equals(url, "/admin/home", StringComparison.OrdinalIgnoreCase))
            //{
            //    var applicationUrl = $"{context.HttpContext.Request.Scheme}://{context.HttpContext.Request.Host.Value}";
            //    var headersDictionary = context.HttpContext.Request.Headers;
            //    var urlReferrer = headersDictionary[HeaderNames.Referer].ToString();
            //    if (string.IsNullOrEmpty(urlReferrer) && !urlReferrer.Contains(applicationUrl))
            //    {
            //        var view = new PartialViewResult() { ViewName = "~/Views/Shared/404.cshtml" };
            //        context.Result = view;
            //    }
            //}

        }
    }
}
