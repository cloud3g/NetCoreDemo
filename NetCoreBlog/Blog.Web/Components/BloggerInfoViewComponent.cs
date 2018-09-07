using Blog.Common;
using Blog.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Components
{
    [ViewComponent(Name = "BloggerInfo")]
    public class BloggerInfoViewComponent : ViewComponent
    {
        private IWritableOptions<Models.BloggerInfoViewModel> _option;
        public BloggerInfoViewComponent(IWritableOptions<Models.BloggerInfoViewModel> option)
        {
            _option = option;
        }
        public IViewComponentResult Invoke()
        {
            var entity = _option.Value;
            return View(entity);
        }
    }
}
