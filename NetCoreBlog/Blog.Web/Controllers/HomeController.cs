using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Web.Models;
using Blog.IService;
using Blog.Common;
using System.Text;
using Blog.Models;
using Blog.Web.Core;

namespace Blog.Web.Controllers
{
    
    public class HomeController : BaseController
    {
        public IBlogArticleService BlogArticleService { get; set; }
        public IBlogAnnouncementService BlogAnnouncementService { get; set; }

        public HomeController(IBlogArticleService blogArticleService, IBlogAnnouncementService blogAnnouncementService)
        {
            BlogArticleService = blogArticleService;
            BlogAnnouncementService = blogAnnouncementService;
        }
        public IActionResult Index()
        {
            //获取公告列表
            var announcementList = BlogAnnouncementService.GetList(a => a.Enable == true).OrderByDescending(a => a.Level).ThenByDescending(a=>a.UpdateTime).ToList();
            ViewBag.announcementList = announcementList;
            //获取文章列表
           var list  = BlogArticleService.GetPageList(1, 10, out int total, a => true, new OrderModelField() { IsDESC=true, PropertyName="Stick" },new OrderModelField() {IsDESC=true, PropertyName="Recommend" },new OrderModelField() { IsDESC=true,PropertyName="UpdateTime" }).ToList();
            ViewBag.articleList = list;
            return View();
        }
        [HttpPost]
        public IActionResult GetArticlesByFlow(int currentIndex, int pageSize)
        {

            List<BlogArticle> list = BlogArticleService.GetPageList(currentIndex, pageSize, out int total, a => true, new OrderModelField() { IsDESC = true, PropertyName = "Stick" }, new OrderModelField() { IsDESC = true, PropertyName = "Recommend" }, new OrderModelField() { IsDESC = true, PropertyName = "UpdateTime" }).ToList();
            var totalPage = (int)Math.Ceiling(total * 1.0 / pageSize);
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                string img = string.IsNullOrWhiteSpace(item.ImgUrl) ? "/images/timg.gif" : item.ImgUrl;
                sb.Append($"<div class=\"article shadow animated zoomIn\"><div class=\"article-left\"><img src = \"{img}\" alt=\"{item.Title}\"/></div><div class=\"article-right\"><div class=\"article-title\">");
                if (item.Stick == true)
                {
                    sb.Append("<span class=\"icon-stick\">置顶</span>");
                }
                if (item.Recommend == true)
                {
                    sb.Append("<span class=\"icon-tuijian\">推荐</span>");
                }
                sb.Append($"<a href = \"{"/Article/Detail/" + item.Id}\" > {item.Title}</a></div><div class=\"article-abstract\">{item.Remark}</div></div><div class=\"clear\"></div><div class=\"article-footer\"><span><i class=\"fa fa-clock-o\"></i>&nbsp;&nbsp;{item.UpdateTime}</span><span class=\"article-author\"><i class=\"fa fa-user\"></i>&nbsp;&nbsp;{item.Submitter}</span><span><i class=\"fa fa-tag\"></i>&nbsp;&nbsp;<a href = \"#\" > {item.CategoryName}</a></span ><span class=\"article-viewinfo\"><i class=\"fa fa-eye\"></i>&nbsp;{item.CommentNum}</span><span class=\"article-viewinfo\"><i class=\"fa fa-commenting\"></i>&nbsp;{item.Traffic}</span></div></div>");
            }
            return Json(new { Success = true, Message = "", SubCode = totalPage, Data = sb.ToString() });
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
