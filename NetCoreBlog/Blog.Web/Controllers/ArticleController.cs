using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.IService;
using Blog.Models;
using System.Text;
using Microsoft.Extensions.Primitives;
using Blog.Web.Core;
using System.Web;
using Blog.Web.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Controllers
{
    public class ArticleController : BaseController
    {
        IBlogArticleService BlogArticleService { get; set; }
        public ArticleController(IBlogArticleService blogArticleService)
        {
            BlogArticleService = blogArticleService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {

            List<BlogArticle> list = BlogArticleService.GetPageList<DateTime?>(1, 10, out int total, a => true, a => a.CreateTime, false).ToList();
            ArticleViewModel data = new ArticleViewModel() { Articles = list };
            if (list.Count() <= 0)
            {
                data.Msg = "本博客还未添加任何文章!";

            }

            return View(data);
        }
        [HttpPost]
        public IActionResult GetArticlesByFlow(int currentIndex, int pageSize, string categoryId, string keyWords)
        {
            List<BlogArticle> list;
            int total;
            if (!string.IsNullOrWhiteSpace(categoryId))
            {

                list = BlogArticleService.GetPageList<DateTime?>(currentIndex, pageSize, out total, a => a.CategoryId == Convert.ToInt32(categoryId), a => a.CreateTime, false).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(keyWords))
            {
                list = BlogArticleService.GetPageList<DateTime?>(currentIndex, pageSize, out total, a => a.Title.Contains(HttpUtility.UrlDecode(keyWords)), a => a.CreateTime, false).ToList();
            }
            else
            {
                list = BlogArticleService.GetPageList<DateTime?>(currentIndex, pageSize, out total, a => true, a => a.CreateTime, false).ToList();
            }

            var totalPage = (int)Math.Ceiling(total * 1.0 / pageSize);
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                string img = string.IsNullOrWhiteSpace(item.ImgUrl) ? "/images/timg.gif" : item.ImgUrl;
                sb.Append($"<div class=\"article shadow animated zoomIn\"><div class=\"article-left\"><img src = \"{img}\" alt=\"{item.Title}\"/></div><div class=\"article-right\"><div class=\"article-title\"><a href = \"{"/Article/Detail/" + item.Id}\" > {item.Title}</a></div><div class=\"article-abstract\">{item.Remark}</div></div><div class=\"clear\"></div><div class=\"article-footer\"><span><i class=\"fa fa-clock-o\"></i>&nbsp;&nbsp;{item.UpdateTime}</span><span class=\"article-author\"><i class=\"fa fa-user\"></i>&nbsp;&nbsp;{item.Submitter}</span><span><i class=\"fa fa-tag\"></i>&nbsp;&nbsp;<a href = \"#\" > {item.CategoryName}</a></span ><span class=\"article-viewinfo\"><i class=\"fa fa-eye\"></i>&nbsp;{item.CommentNum}</span><span class=\"article-viewinfo\"><i class=\"fa fa-commenting\"></i>&nbsp;{item.Traffic}</span></div></div>");
            }
            return Json(new { Success = true, Message = "", SubCode = totalPage, Data = sb.ToString() });
        }
        public IActionResult Detail(int id)
        {
            var entity = BlogArticleService.Find(c => c.Id == id);
            if (entity == null)
            {
                return PartialView("404");
            }
            else
            {
                entity.Traffic = entity.Traffic + 1;
                BlogArticleService.Edit(entity);
                return View(entity);
            }

        }
        public IActionResult Category(int id)
        {
            List<BlogArticle> list = BlogArticleService.GetPageList<DateTime?>(1, 10, out int total, a => a.CategoryId == id, a => a.CreateTime, false).ToList();
            ViewBag.CategoryId = id;
            ArticleViewModel data = new ArticleViewModel() { };
            if (list.Count() <= 0)
            {
                data.Msg = "本栏目还未添加任何文章,随便看看吧!";
                data.Articles = BlogArticleService.GetList(b => true).OrderBy(b => Guid.NewGuid()).Take(10).ToList();
            }
            else
            {
                data.Articles = list;
            }

            return View("index", data);
        }
        public IActionResult Search(string keywords)
        {

            List<BlogArticle> list = BlogArticleService.GetPageList<DateTime?>(1, 10, out int total, a => a.Title.Contains(HttpUtility.UrlDecode(keywords)), a => a.CreateTime, false).ToList();
            ViewBag.KeyWords = keywords;
            ArticleViewModel data = new ArticleViewModel() { };
            if (list.Count() <= 0)
            {
                data.Msg = $"未搜索到与【<span style=\"color: #FF5722;\">{HttpUtility.UrlDecode(keywords)}</span>】有关的文章，随便看看吧！";
                data.Articles = BlogArticleService.GetList(b => true).OrderBy(b => Guid.NewGuid()).Take(10).ToList();
            }
            else
            {
                data.Articles = list;
            }
            return View("index", data);
        }
    }
}
