using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.IService;
using Blog.Web.Core;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Controllers
{
    public class TimeLineController : BaseController
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
    }
}
