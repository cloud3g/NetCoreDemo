using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.IService;
using Blog.Models;
using Blog.Common;
using Blog.Web.Filter;
using Blog.Web.Core;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class MessageResponseController : AdminBaseController
    {
        private IWC_OfficalAccountsService _officalService;
        private IWC_MessageResponseService _messageResponseService;
        public MessageResponseController(IWC_OfficalAccountsService officalService, IWC_MessageResponseService messageResponseService)
        {
            _officalService = officalService;
            _messageResponseService = messageResponseService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var offical = _officalService.Find(o => o.IsDefault == true);
            if (offical == null)
            {
                return Content("请选择默认公众号后再刷新此页面！");
            }
            var defaultentity = _messageResponseService.Find(m => m.MessageRule == Blog.Models.WeChatRequestRuleEnum.Default && m.IsDefault == true);
            ViewBag.defaultCategory = defaultentity == null ? -1 : (int)defaultentity.Category;

            var subscriberentity = _messageResponseService.Find(m => m.MessageRule == Blog.Models.WeChatRequestRuleEnum.Subscriber && m.IsDefault == true);
            ViewBag.defaultCategory = defaultentity == null ? -1 : (int)defaultentity.Category;
            ViewBag.subscriberCategory = subscriberentity == null ? -1 : (int)subscriberentity.Category;
            ViewBag.defaultOfficealId = offical.Id;
            ViewBag.defaultOfficealName = offical.OfficalName;
            return View();
        }



        #region 获取列表
        /// <summary>
        /// 返回列表
        /// </summary>
        /// <param name="type">category类型(默认,关注,文本,图文)</param>
        /// <param name="defaultOfficealId">默认公众号Id</param>
        /// <param name="RequestRule">返回类型(图文,文本)</param>
        /// <param name="matchKey">关键词</param>
        /// <param name="pager"></param>
        /// <returns></returns>
        [SetAction(ActionName ="index")]
        public IActionResult GetDefaultContent(int type, int defaultOfficealId, int RequestRule, string matchKey, GridPager<WC_MessageResponse> pager)
        {

            if (type == (int)WeChatReplyCategory.Text && ((int)WeChatRequestRuleEnum.Default == RequestRule || (int)WeChatRequestRuleEnum.Subscriber == RequestRule))
            {

                var entity = _messageResponseService.Find(m => m.OfficalAccountId == defaultOfficealId && (int)m.Category == type && (int)m.MessageRule == RequestRule);
                return Json(entity);
            }
            else
            {
                if (string.IsNullOrEmpty(matchKey))
                {
                    var list = _messageResponseService.GetPageList(pager.page, pager.rows, out int total, m => m.OfficalAccountId == defaultOfficealId && (int)m.Category == type && (int)m.MessageRule == RequestRule);
                    return Json(new { list = list.ToList(), count = total });
                }
                else
                {
                    var list = _messageResponseService.GetPageList(pager.page, pager.rows, out int total, m => m.OfficalAccountId == defaultOfficealId && (int)m.Category == type && (int)m.MessageRule == RequestRule && m.MatchKey == matchKey);
                    return Json(new { list = list.ToList(), count = total });
                }

            }


        }
        /// <summary>
        /// 返回关键词列表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="defaultOfficealId"></param>
        /// <param name="RequestRule"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        [SetAction(ActionName = "index")]
        public IActionResult GetListProperty(int type, int defaultOfficealId, int RequestRule, GridPager<WC_MessageResponse> pager)
        {

            var list = _messageResponseService.GetPageList(pager.page, pager.rows, out int total, m => m.OfficalAccountId == defaultOfficealId && (int)m.Category == type && (int)m.MessageRule == RequestRule).Select(m => m.MatchKey).Distinct();
            return Json(new { list = list.ToList(), count = total });


        }
        #endregion

        #region 添加
        /// <summary>
        /// 全部类型通过此方法添加数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [SetAction(ActionName ="save")]
        public JsonResult PostData(WC_MessageResponse model)
        {
            var offical = _officalService.Find(o => o.IsDefault == true);

            model.CreateTime = DateTime.Now;
            model.UpdateTime = DateTime.Now;
            model.OfficalAccountId = offical.Id;
            model.Enable = true;
            model.IsDefault = true;
            return Json(_messageResponseService.PostData(model));

        }

        /// <summary>
        /// 返回默认回复中的图片回复表单
        /// </summary>
        /// <returns></returns>
        [SetAction(ActionName = "add")]
        public IActionResult AddDefaultImageContent()
        {

            return PartialView();
        }

        /// <summary>
        /// 返回关注回复中的图片回复表单
        /// </summary>
        /// <returns></returns>
        [SetAction(ActionName = "add")]
        public IActionResult AddSubscriberImageContent()
        {

            return PartialView();
        }
        /// <summary>
        /// 返回图片回复表单
        /// </summary>
        /// <returns></returns>
        [SetAction(ActionName = "add")]
        public IActionResult AddImageContent()
        {
            return PartialView();
        }
        #endregion


        #region 编辑
        [HttpPost]
        [SetAction(ActionName = "save")]
        public IActionResult Edit(WC_MessageResponse model)
        {
            return Json(_messageResponseService.Edit(model));
        }
        /// <summary>
        /// 返回默认回复和关注回复中图片回复的编辑表单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [SetAction(ActionName = "edit")]
        public IActionResult EditImageContent(int id)
        {
            var entity = _messageResponseService.Find(m => m.Id == id);
            return PartialView(entity);
        }
        /// <summary>
        /// 返回文本回复的编辑表单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [SetAction(ActionName = "edit")]
        public IActionResult EditTextContent(int id)
        {
            var entity = _messageResponseService.Find(m => m.Id == id);
            return PartialView(entity);
        }
        /// <summary>
        /// 返回图片回复的编辑表单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [SetAction(ActionName = "edit")]
        public IActionResult EditImage(int id)
        {
            var entity = _messageResponseService.Find(m => m.Id == id);
            return PartialView(entity);
        }
        #endregion


        #region 设置状态
        [HttpPost]
        [SetAction(ActionName = "setstatus")]
        public IActionResult SetDefault(int officealId, int category, int request)
        {
            return Json(_messageResponseService.SetDefault(officealId, category, request));
        }
        #endregion


        #region 删除
        /// <summary>
        /// 通用删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(int id)
        {
            return Json(_messageResponseService.Delete(id));
        }
        #endregion
    }
}
