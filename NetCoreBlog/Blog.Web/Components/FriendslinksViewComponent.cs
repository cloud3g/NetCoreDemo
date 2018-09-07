using Blog.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Components
{
    [ViewComponent(Name = "Friendslinks")]
    public class FriendslinksViewComponent: ViewComponent
    {
        public IFriendslinksService FriendslinksService { get; set; }
        public FriendslinksViewComponent(IFriendslinksService friendslinksService)
        {
            FriendslinksService = friendslinksService;
        }
        public IViewComponentResult Invoke()
        {
            var list = FriendslinksService.GetList(f => f.Enable == true).ToList();
            return View(list);
        }
    }
}
