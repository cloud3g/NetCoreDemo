using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using System.IO;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.MvcExtension;
using Blog.Web.Core.WeChat;
using Blog.IService;
using Microsoft.Extensions.Primitives;
using Blog.Models;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class WeChatController : Controller
    {
     
        public IWC_MessageResponseService  _responseService { get; set; }
        public IWC_OfficalAccountsService  _officalAccountsService { get; set; }
        public WeChatController(IWC_MessageResponseService responseService, IWC_OfficalAccountsService officalAccountsService)
        {
            _responseService = responseService;
            _officalAccountsService = officalAccountsService;
        }
  

        [HttpGet]
        [ActionName("Index")]
        public Task<ActionResult> Get(string signature, string timestamp, string nonce, string echostr)
        {
            return Task.Factory.StartNew(() =>
            {
                //没有参数
                if (!Request.Query.TryGetValue("id", out StringValues value))
                {
                    return "非法路径请求！"; ;
                }

                WC_OfficalAccounts model = _officalAccountsService.Find(o => o.Id == Convert.ToInt32(value));
                if (CheckSignature.Check(signature, timestamp, nonce, model.Token))
                {
                    return echostr; //返回随机字符串则表示验证通过
                }
                else
                {
                  
                    return "failed:" + signature + "," + CheckSignature.GetSignature(timestamp, nonce, model.Token) + "。" +
                        "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。";
                }
            }).ContinueWith<ActionResult>(task => Content(task.Result));
        }

        /// <summary>
        /// 最简化的处理流程
        /// </summary>
        [HttpPost]
        [ActionName("Index")]
        public Task<ActionResult> Post(PostModel postModel)
        {
            return Task.Factory.StartNew<ActionResult>(() =>
            {
                    
                //没有参数
                if (!Request.Query.TryGetValue("id", out StringValues value))
                {
                    return new WeixinResult("非法路径请求！");
                }

                WC_OfficalAccounts model = _officalAccountsService.Find(o => o.Id == Convert.ToInt32(value));

                if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, model.Token))
                {
                    return new WeixinResult("参数错误！");
                }

                postModel.Token = model.Token;
                postModel.EncodingAESKey = model.OfficalKey; //根据自己后台的设置保持一致
                postModel.AppId = model.AppId; //根据自己后台的设置保持一致
                var memoryStream = new MemoryStream();
                Request.Body.CopyTo(memoryStream);
                var messageHandler = new CustomMessageHandler(memoryStream, postModel, _responseService, _officalAccountsService,model.Id);
                messageHandler.OmitRepeatedMessage = true;//启用消息去重功能
                messageHandler.Execute(); //执行微信处理过程

                return new FixWeixinBugWeixinResult(messageHandler);

            }).ContinueWith<ActionResult>(task => task.Result);
        }

    }
}
