using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Web.Core;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Areas.Admin.Controllers
{
    public class SysUserInfoController : AdminBaseController
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
