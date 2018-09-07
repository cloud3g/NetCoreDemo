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
    public class DiaryController : BaseController
    {
        ITimeLineService _service;
        public DiaryController(ITimeLineService service)
        {
            _service = service;
        }
        // GET: /<controller>/
        public IActionResult TimeLine()
        {
            Dictionary<int, List<IGrouping<int, Blog.Models.TimeLine>>> list = new Dictionary<int, List<IGrouping<int, Blog.Models.TimeLine>>>();
            var timelinesYeah = _service.GetList(t =>true).GroupBy(a => a.CreateTime.Value.Year).OrderByDescending(a => a.Key).ToList();
            foreach (var items in timelinesYeah)
            {
                var timelinesm = items.GroupBy(a => a.CreateTime.Value.Month).OrderByDescending(a => a.Key).ToList();
                list.Add(items.Key, timelinesm);
            }
            return View(list);
        }
    }
}
