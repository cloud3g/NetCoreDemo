using Blog.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Components
{
    [ViewComponent(Name ="nav")]
    public class NavViewComponent:ViewComponent
    {
        private IBlogCategoryService _service;
        public NavViewComponent(IBlogCategoryService service)
        {
            _service = service;
        }
        public IViewComponentResult Invoke()
        {
            var list =  _service.GetList(c => c.Enable == true&&c.IsNav==true&&c.CategoryType== Blog.Models.Enum.BlogCategoryType.General).ToList();
            return View(list);
        }
    }
}
