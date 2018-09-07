using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Models;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Menu;
using Blog.IService;
using Senparc.Weixin.MP;
using Senparc.Weixin.Entities;
using Blog.Web.Filter;
using Senparc.Weixin.MP.AdvancedAPIs.User;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.AdvancedAPIs.Media;
using Senparc.Weixin.MP.AdvancedAPIs.GroupMessage;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class WechatMenuSettingController : Controller
    {
        private IWC_OfficalAccountsService _service;
        public WechatMenuSettingController(IWC_OfficalAccountsService service)
        {
            _service = service;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {

            WC_OfficalAccounts model = _service.Find(a=>a.IsDefault==true);
            ViewBag.CurrentOfficalAcount = model;
            GetMenuResult result = new GetMenuResult(new ButtonGroup());

            //初始化
            for (int i = 0; i < 3; i++)
            {
                var subButton = new SubButton();
                for (int j = 0; j < 5; j++)
                {
                    var singleButton = new SingleClickButton();
                    subButton.sub_button.Add(singleButton);
                }
            }
            return View();
        }

        [SetAction(ActionName = "Edit")]
        public IActionResult CreateMenu(GetMenuResultFull resultFull, MenuMatchRule menuMatchRule)
        {
            var useAddCondidionalApi = menuMatchRule != null && !menuMatchRule.CheckAllNull();
            var apiName = string.Format("使用接口：{0}。", (useAddCondidionalApi ? "个性化菜单接口" : "普通自定义菜单接口"));
            var model = _service.Find(o=>o.IsDefault==true);

            try
            {
                //重新整理按钮信息
                WxJsonResult result = null;
                IButtonGroupBase buttonGroup = null;
                if (useAddCondidionalApi)
                {
                    //个性化接口
                    buttonGroup = Senparc.Weixin.MP.CommonAPIs.CommonApi.GetMenuFromJsonResult(resultFull, new ConditionalButtonGroup()).menu;
                    
                    var addConditionalButtonGroup = buttonGroup as ConditionalButtonGroup;
                    addConditionalButtonGroup.matchrule = menuMatchRule;
                    result = Senparc.Weixin.MP.CommonAPIs.CommonApi.CreateMenuConditional(model.AppId, addConditionalButtonGroup);
                    apiName += string.Format("menuid：{0}。", (result as CreateMenuConditionalResult).menuid);
                }
                else
                {
                    //普通接口
                    buttonGroup = Senparc.Weixin.MP.CommonAPIs.CommonApi.GetMenuFromJsonResult(resultFull, new ButtonGroup()).menu;
                    result = Senparc.Weixin.MP.CommonAPIs.CommonApi.CreateMenu(model.AppId, buttonGroup);
                }

                var json = new
                {
                    Success = result.errmsg == "ok",
                    Message = "菜单更新成功。" + apiName
                };
                return Json(json);
            }
            catch (Exception ex)
            {
                var json = new { Success = false, Message = string.Format("更新失败：{0}。{1}", ex.Message, apiName) };
                return Json(json);
            }
        }
        [SetAction(ActionName = "Edit")]
        [HttpGet]
        public ActionResult GetMenu()
        {
            var model = _service.Find(o => o.IsDefault == true);
            GetMenuResult result;
            try
            {
                result = Senparc.Weixin.MP.CommonAPIs.CommonApi.GetMenu(model.AppId);
                if (result == null)
                {
                    return Json(new { error = "菜单不存在或验证失败！" });
                }
            }
            catch(Exception ex)
            {
                return Json(new { error = ex.Message });
            }
            return Json(result);
        }
        [SetAction(ActionName = "delete")]
        public ActionResult DeleteMenu()
        {
            try
            {
                var model = _service.Find(o => o.IsDefault == true);
             
                var result = Senparc.Weixin.MP.CommonAPIs.CommonApi.DeleteMenu(model.AppId);
                var json = new
                {
                    Success = result.errmsg == "ok",
                    Message = result.errmsg
                };
                return Json(json);
            }
            catch (Exception ex)
            {
                var json = new { Success = false, Message = ex.Message };
                return Json(json);
            }
        }

        public IActionResult Send()
        {
            WC_OfficalAccounts accountModel = _service.Find(o => o.IsDefault == true);
            List<BatchGetUserInfoData> list = new List<BatchGetUserInfoData>();
            //批量获取粉丝的OPENID信息
            OpenIdResultJson resultList = UserApi.Get(accountModel.AppId, "");
            string[] openid = resultList.data.openid.ToArray();
            string access_token = AccessTokenContainer.GetAccessTokenResult(accountModel.AppId).access_token;   //获取AccessToken结果  
            string media_id_img = "";     //图片media_id  
            string media_id_tuwen = "";   //图文media_id  

            //新增永久素材（图片）  将作为微信群发消息的封面图片  
            UploadForeverMediaResult uploadResult_IMG = MediaApi.UploadForeverMedia(access_token, @"E:\Visual Studio 2017\Cjf.Blog+pgsql\Blog.Web\wwwroot\upload\Pictures\wechat\ac8bd715-45bc-4378-b49c-1ea0cde150b2.jpg");
            if (uploadResult_IMG.errcode.ToString().Contains("成功"))
            {
                media_id_img = uploadResult_IMG.media_id;
            }
            else
            {
     
                throw new Exception("上传素材出错");
            }
            NewsModel newsmodel = new NewsModel();
            newsmodel.title = "测试";
            newsmodel.thumb_media_id = media_id_img;    //上面的图片素材media_id  
            newsmodel.author = "";
            newsmodel.digest = "测试描述";
            newsmodel.show_cover_pic = "1";
            newsmodel.content = "测试内容";
            newsmodel.content_source_url = "";
            UploadForeverMediaResult uploadResult_TUWEN = MediaApi.UploadNews(accountModel.AppId, 10000, newsmodel);
            if (uploadResult_TUWEN.errcode.ToString().Contains("成功"))    //返回的是请求成功  
            {
                media_id_tuwen = uploadResult_TUWEN.media_id;
            }
            else
            {
                throw new Exception("上传图文素材出错");
            }
            //群发图文消息需要权限
            GroupMessageApi.SendGroupMessageByOpenId(accountModel.AppId, GroupMessageType.mpnews, media_id_tuwen, 1000, openid);
            // GroupMessageApi.SendGroupMessageByOpenId(accountModel.AppId, GroupMessageType.text, "内容", 1000, openid); 发文本消息不用权限
            return Json("");
        }
    }
}
