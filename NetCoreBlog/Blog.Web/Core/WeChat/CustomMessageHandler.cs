using Blog.IService;
using Blog.Models;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.MP.MessageHandlers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace Blog.Web.Core.WeChat
{
    public class CustomMessageHandler : MessageHandler<CustomMessageContext>
    {
        IWC_MessageResponseService _responseService;
        IWC_OfficalAccountsService _officalAccountsService;
        int  Id ;
        public CustomMessageHandler(Stream inputStream, PostModel postModel, IWC_MessageResponseService responseService, IWC_OfficalAccountsService officalAccountsService,int id ,int maxRecordCount = 0)
            : base(inputStream, postModel, maxRecordCount)
        {
           
            _responseService = responseService;
            _officalAccountsService = officalAccountsService;
            Id = id;
        }
        public override void OnExecuting()
        {
            if (OmitRepeatedMessage && CurrentMessageContext.RequestMessages.Count > 1)
            {
                var lastMessage = CurrentMessageContext.RequestMessages[CurrentMessageContext.RequestMessages.Count - 2];
                if (lastMessage.MsgId != 0 && lastMessage.MsgId == RequestMessage.MsgId)
                {
                    CancelExcute = true;//重复消息，取消执行
                }
            }
        }
        /// <summary>
        /// 关注
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override Senparc.Weixin.MP.Entities.IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            WC_OfficalAccounts account = _officalAccountsService.Find(a => a.Id == Id);
            List<WC_MessageResponse> messageList = _responseService.GetList(m => m.OfficalAccountId == account.Id &&m.MessageRule== WeChatRequestRuleEnum.Subscriber&&m.IsDefault==true).OrderBy(m => m.Sort).ToList();
            if (messageList.Count > 0)
            {
                if(messageList[0].Category== WeChatReplyCategory.Text)
                {
                    var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
                    responseMessage.CreateTime = DateTime.Now;
                    responseMessage.ToUserName = requestMessage.FromUserName;

                    responseMessage.Content = messageList[0].TextContent;
                    return responseMessage;
                }else if (messageList[0].Category == WeChatReplyCategory.Image)
                {
                    var responseMessage = CreateResponseMessage<ResponseMessageNews>();
                    foreach (var model in messageList.Where(m => m.Category == WeChatReplyCategory.Image))
                    {
                        responseMessage.Articles.Add(new Article()
                        {
                            Title = model.TextContent,
                            Description = model.ImgTextContext,
                            PicUrl = "http://101.201.236.115" + model.ImgTextUrl,
                            Url = model.ImgTextLink
                        });
                    }
                    return responseMessage;
                }
                else
                {
                    var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
                    return responseMessage;
                }
             
            }
            else
            {
                var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
                return responseMessage;
            }
           

        }

        public override Senparc.Weixin.MP.Entities.IResponseMessageBase DefaultResponseMessage(Senparc.Weixin.MP.Entities.IRequestMessageBase requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>(); //ResponseMessageText也可以是News等其他类型
            responseMessage.Content = "这条消息来自DefaultResponseMessage111。";
            return responseMessage;
        }
        /// <summary>
        /// 处理文字请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override Senparc.Weixin.MP.Entities.IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            

            //获得当前公众号
            WC_OfficalAccounts account = _officalAccountsService.Find(a => a.Id==Id);
            List<WC_MessageResponse> messageList = _responseService.GetList(m => m.OfficalAccountId == account.Id && m.MatchKey == requestMessage.Content).OrderBy(m=>m.Sort).ToList();
            if (messageList.Count <= 0)
            {
                messageList = _responseService.GetList(m => m.OfficalAccountId == 2 && m.MessageRule == WeChatRequestRuleEnum.Default && m.IsDefault == true).ToList();
            }

            if (messageList.Count() > 0)
            {
                //文本方式
                if (messageList[0].MessageRule == WeChatRequestRuleEnum.Text)
                {
                    var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
                    responseMessage.CreateTime = DateTime.Now;
                    responseMessage.ToUserName = requestMessage.FromUserName;

                    responseMessage.Content = messageList[0].TextContent;
                    return responseMessage;
                }
                //图文方式
                else if (messageList[0].MessageRule == WeChatRequestRuleEnum.Image)
                {
                    var responseMessage = CreateResponseMessage<ResponseMessageNews>();
                    foreach (var model in messageList.Where(m=>m.Category== WeChatReplyCategory.Image))
                    {
                        responseMessage.Articles.Add(new Article()
                        {
                            Title = model.TextContent,
                            Description = model.ImgTextContext,
                            PicUrl = "http://101.201.236.115" + model.ImgTextUrl,
                            Url = model.ImgTextLink
                        });
                    }
                    return responseMessage;
                }//一般很少用到
                else if (messageList[0].MessageRule == WeChatRequestRuleEnum.Voice)
                {
                    var responseMessage = base.CreateResponseMessage<ResponseMessageMusic>();
                    responseMessage.Music.MusicUrl = messageList[0].MeidaUrl;
                    responseMessage.Music.Title = messageList[0].TextContent;
                    responseMessage.Music.Description = messageList[0].Remark;
                    return responseMessage;
                }//默认回复
                else if (messageList[0].MessageRule == WeChatRequestRuleEnum.Default)
                {
                    if (messageList[0].Category == WeChatReplyCategory.Text)
                    {
                        var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
                        responseMessage.CreateTime = DateTime.Now;
                        responseMessage.ToUserName = requestMessage.FromUserName;

                        responseMessage.Content = messageList[0].TextContent;
                        return responseMessage;
                    }
                    //图文方式
                    else if (messageList[0].Category == WeChatReplyCategory.Image)
                    {
                        var responseMessage = CreateResponseMessage<ResponseMessageNews>();
                        foreach (var model in messageList)
                        {
                            responseMessage.Articles.Add(new Article()
                            {
                                Title = model.TextContent,
                                Description = model.ImgTextContext,
                                PicUrl = "http://101.201.236.115" + model.ImgTextUrl,
                                Url = model.ImgTextLink
                            });
                        }
                        return responseMessage;
                    }//一般很少用到
                    else if (messageList[0].Category == WeChatReplyCategory.Voice)
                    {
                        var responseMessage = base.CreateResponseMessage<ResponseMessageMusic>();
                        responseMessage.Music.MusicUrl = messageList[0].MeidaUrl;
                        responseMessage.Music.Title = messageList[0].TextContent;
                        responseMessage.Music.Description = messageList[0].Remark;
                        return responseMessage;
                    }

                }
                //下面方式用到才启用
                //视频方式
                //位置
            }
            var errorResponseMessage = requestMessage.CreateResponseMessage<ResponseMessageText>();
            //因为没有设置errorResponseMessage.Content，所以这小消息将无法正确返回。
            errorResponseMessage.Content = messageList.Count().ToString();
            return errorResponseMessage;
        }

        public override Senparc.Weixin.MP.Entities.IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.CreateTime = DateTime.Now;
            responseMessage.ToUserName = requestMessage.FromUserName;

            responseMessage.Content = "你点击了菜单按钮，按钮信息key："+requestMessage.EventKey;
            
            return responseMessage;

 
        }
    }
}
