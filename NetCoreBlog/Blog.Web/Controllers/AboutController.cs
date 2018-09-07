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
    public class AboutController : Controller
    {
        IWritableOptions<BlogSettingsViewModel> _blogSettingsOption;
        IWritableOptions<BloggerInfoViewModel> _bloggerInfoOption;
        IFriendslinksService _friendslinksService;
        public AboutController(IWritableOptions<Models.BlogSettingsViewModel> blogSettingsOption, IWritableOptions<Models.BloggerInfoViewModel> bloggerInfoOption, IFriendslinksService friendslinksService)
        {
            _blogSettingsOption = blogSettingsOption;
            _bloggerInfoOption = bloggerInfoOption;
            _friendslinksService = friendslinksService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            BlogSettingsViewModel bloginfo = _blogSettingsOption.Value;
            BloggerInfoViewModel bloggerinfo = _bloggerInfoOption.Value;
            List<Friendslinks> list = _friendslinksService.GetList(f => f.Enable == true).ToList();
            ViewBag.bloginfo = bloginfo;
            ViewBag.bloggerinfo = bloggerinfo;
            ViewBag.friendslinks = list;
            return View();
        }
    }
}
