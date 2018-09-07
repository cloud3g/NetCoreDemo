using Blog.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Components
{
    [ViewComponent(Name = "LookAround")]
    public class LookAroundViewComponent: ViewComponent
    {
        public IBlogArticleService _service { get; set; }
        public LookAroundViewComponent(IBlogArticleService service)
        {
            _service = service;
        }
        public IViewComponentResult Invoke()
        {
            var list = _service.GetList(b => true).OrderBy(b => Guid.NewGuid()).Take(10).ToList();
            return View(list);
        }
    }
}
