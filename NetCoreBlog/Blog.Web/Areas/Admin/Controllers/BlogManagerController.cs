using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Web.Models;
using Blog.IService;
using Blog.Models;
using Blog.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using Blog.Web.Core;
using Blog.Web.Filter;
namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class BlogManagerController : AdminBaseController
    {
        IBlogCategoryService BlogCategoryService { get; set; }
        IBlogArticleService BlogArticleService { get; set; }
        public BlogManagerController(IBlogCategoryService blogCategoryService, IBlogArticleService blogArticleService)
        {
            BlogCategoryService = blogCategoryService;
            BlogArticleService = blogArticleService;
        }
        public IActionResult Index()
        {
          
            return View();
        }

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pager">分页类</param>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        [SetAction(ActionName ="index")]
        public IActionResult GetList(GridPager<BlogArticle> pager, string query)
        {
            List<BlogArticle> list = null;
            pager.sort = "Id";
            pager.order = "desc";
            if (string.IsNullOrEmpty(query))
            {
                list = BlogArticleService.GetPageList(pager, b => true).Entities;
            }
            else
            {
                list = BlogArticleService.GetPageList(pager, b => b.Title.Contains(query)).Entities;
            }
            var rows = from b in list
                       select new
                       {
                           b.Id,
                           b.Title,
                           b.CategoryName,
                           b.Traffic,
                           b.Remark,
                           b.CreateTime,
                           b.Stick,
                           b.Recommend
                       };
            return Json(new { code = 0, count = pager.totalRows, data = rows });

        }

        /// <summary>
        /// 获取selecte下拉选择框
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public IActionResult GetSelect(int pid) => Json(BlogCategoryService.GetList(c => c.Pid == pid && c.Enable == true).Select(c => new { Text = c.Name, Value = c.Id }));
        #endregion


        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IActionResult Delete(List<int> ids)
        {
            if (ids == null || ids.Count <= 0)
            {
                return Json(new Response() { Code = ResponseCode.Fail, Message = "没有选择删除的项！" });
            }
            foreach (var id in ids)
            {
                BlogArticleService.Delete(id, false);
            }
            Response res = null;
            if (BlogArticleService.SaveChanges() > 0)
            {
                res = new Response() { Code = ResponseCode.Success, Message = "删除成功！" };
            }
            else
            {
                res = new Response() { Code = ResponseCode.Fail, Message = "删除失败！" };
            }
            return Json(res);
        }
        #endregion

        #region 添加
        [HttpGet]
        public IActionResult Add()
        {
            var list = BlogCategoryService.GetList(c => c.Enable == true && c.CategoryType == Blog.Models.Enum.BlogCategoryType.General && c.Pid == 0);
            var categoryList = (from c in list
                                select new SelectListItem
                                {
                                    Value = c.Id.ToString(),
                                    Text = c.Name
                                }).ToList();
            ViewBag.categoryList = categoryList;
            return PartialView();
        }
        [HttpPost]
        [SetAction(ActionName ="save")]
        public IActionResult Add(BlogArticle entity)
        {
            Response res = null;
            if (ModelState.IsValid)
            {
 
                Request.Form.TryGetValue("childCategory", out StringValues categoryId);
                if (!string.IsNullOrEmpty(categoryId.ToString()))
                {
                    entity.CategoryId = Convert.ToInt32(categoryId.ToString());

                }
                entity.Stick = false;
                entity.Recommend = false;
                entity.Submitter = HttpContext.User.Claims.SingleOrDefault(u => u.Type == "RealName").Value;
                entity.Traffic = 0;
                entity.CommentNum = 0;
                entity.CreateTime = DateTime.Now;
                entity.UpdateTime = DateTime.Now;
                entity.CategoryName = BlogCategoryService.Find(c => c.Id == entity.CategoryId).Name;
                res = BlogArticleService.Add(entity);
            }
            else
            {
                res = new Common.Response() { Code = ResponseCode.Success, Message = Utils.ModelStateMessage(ModelState) };
            }
            return Json(res);
        }
        #endregion

        #region 编辑
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var entity = BlogArticleService.FindJoin(b => b.Id == id, new string[] { "BlogCategory" });
            if (entity == null)
            {
                return Content("没有找到相关文章！");
            }
            else
            {

                var list = BlogCategoryService.GetList(c => c.Enable == true && c.CategoryType == Blog.Models.Enum.BlogCategoryType.General && c.Pid == 0);
                var categoryList = (from c in list
                                    select new SelectListItem
                                    {
                                        Value = c.Id.ToString(),
                                        Text = c.Name,
                                        Selected = (c.Id == entity.BlogCategory.Id || c.Id == entity.BlogCategory.Pid) ? true : false
                                    }).ToList();
                ViewBag.categoryList = categoryList;
                return PartialView(entity);
            }
        }
        [HttpPost]
        [SetAction(ActionName = "save")]
        public IActionResult Edit(BlogArticle article)
        {
            Response res = null;
            if (ModelState.IsValid)
            {
                article.UpdateTime = DateTime.Now;
            
                res = BlogArticleService.Edit(article);
            }
            else
            {
                res = new Common.Response() { Code = ResponseCode.Fail, Message = Utils.ModelStateMessage(ModelState) };
            }
            return Json(res);
        }
        #endregion

        #region 设置状态
        
        /// <summary>
        /// 设置置顶
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [SetAction(ActionName = "setstatus")]
        public IActionResult SetTopOrNot(int id)
        {
            var entity = BlogArticleService.Find(b => b.Id == id);
            if (entity == null)
            {
                return Json(new Response() { Code = ResponseCode.Fail, Message = "没有找到相关数据！" });
            }
            Response res = null;
            entity.Stick = !entity.Stick;
            res = BlogArticleService.Edit(entity);
            res.Message = res.Code == ResponseCode.Success ? "设置成功！" : "设置失败";
            return Json(res);
        }
        /// <summary>
        /// 设置推荐
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [SetAction(ActionName = "setstatus")]
        public IActionResult SetRecommendOrNot(int id)
        {
            var entity = BlogArticleService.Find(b => b.Id == id);
            if (entity == null)
            {
                return Json(new Response() { Code = ResponseCode.Fail, Message = "没有找到相关数据！" });
            }
            Response res = null;
            entity.Recommend = !entity.Recommend;
            res = BlogArticleService.Edit(entity);
            res.Message = res.Code == ResponseCode.Success ? "设置成功！" : "设置失败";
            return Json(res);
        }
        #endregion

    }
}
