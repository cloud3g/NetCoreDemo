using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Common;
using Blog.Web.Core;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Controllers
{
    public class ResourceController : BaseController
    {
        Blog.IService.IResourceService _service;
        public ResourceController(Blog.IService.IResourceService service)
        {
            _service = service;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var list = _service.GetList(r => true).ToList();
            return View(list);
        }
    }
}
