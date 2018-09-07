using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Blog.Web.Models;
using Blog.Common;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Core
{
    public class BaseController : Controller
    {


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            
            if (context.HttpContext.Request.Cookies["full"] != null && context.HttpContext.Request.Cookies["full"] == "true")
            {
                ViewBag.Full = context.HttpContext.Request.Cookies["full"];
            }

        }
    }
}
