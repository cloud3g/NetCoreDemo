using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.IService;
using Blog.Common;
using Blog.Web.Models;
using Blog.Models;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Controllers
{
    public class ErrorsController : Controller
    {
        [Route("errors/{statusCode}")]
        public IActionResult CustomError(int statusCode)
        {
            if (statusCode == 404)
            {
                return View("~/Views/Errors/404.cshtml");
            }
            return View("~/Views/Errors/500.cshtml");
        }
    }
}
