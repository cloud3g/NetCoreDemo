using Blog.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Components
{
    [ViewComponent(Name = "HotArticle")]
    public class HotArticleViewComponent : ViewComponent
    {
        private IBlogArticleService _service;
        public HotArticleViewComponent(IBlogArticleService service)
        {
            _service = service;
        }
        public IViewComponentResult Invoke()
        {
            var list =_service.GetPageList(1, 10, out int total, c => true, new Common.OrderModelField() {  IsDESC=true, PropertyName="Traffic"}).ToList();
            return View(list);
        }
    }
}
