using Blog.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Components
{
    [ViewComponent(Name = "Recomment")]
    public class RecommentViewComponent:ViewComponent
    {
        public IBlogArticleService _service { get; set; }
        public RecommentViewComponent(IBlogArticleService service)
        {
            _service = service;
        }
        public IViewComponentResult Invoke()
        {
            var list = _service.GetPageList(1, 10, out int total, b => b.Recommend == true, new Common.OrderModelField() {  IsDESC=true, PropertyName="CreateTime"}).ToList();
            return View(list);
        }
    }
}
