using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Web.Models;
using Blog.Web.Core;
using Microsoft.AspNetCore.Authorization;
using Blog.IService;
using Blog.Web.Areas.Admin.Models;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class HomeController : AdminBaseController
    {
        private ISysModuleService _sysModuleService;
        public HomeController(ISysModuleService sysModuleService)
        {
            _sysModuleService = sysModuleService;
        }
        public IActionResult Index()
        {
            // ViewBag.CurrentAccount = CurrentAccount;
            return View();
        }
        public IActionResult GetNav()
        {
            var rootList = _sysModuleService.GetList(s => s.IsSysMenu == true && s.Pid == 0).OrderBy(s=>s.Sort).ToList();
            var list = from s in rootList
                       select new NavViewModel()
                       {
                           title = s.Name,
                           href = "/" + s.AreaName + "/" + s.ControllerName,
                           children = GetChildList(s.Id)
                       };
            return Json(list);
        }
        private List<NavViewModel> GetChildList(int pid)
        {
            var list = _sysModuleService.GetList(s => s.IsSysMenu == true && s.Pid == pid).OrderBy(s => s.Sort);
            var viewlist = (from s in list
                            select new NavViewModel()
                            {
                                title = s.Name,
                                href = "/" + s.AreaName + "/" + s.ControllerName,

                            }).ToList();
            return viewlist;

        }

        public IActionResult Main()
        {
            return View();
        }

    }
}
