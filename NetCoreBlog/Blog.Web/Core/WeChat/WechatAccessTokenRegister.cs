using Blog.Models;
using Microsoft.Extensions.DependencyInjection;
using Senparc.Weixin.MP.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Core.WeChat
{
    public static class WechatAccessTokenRegister
    {
        public  static void Register(BlogDbContext context)
        {
             
            var list = context.WC_OfficalAccounts.Where(a => true);
            foreach (var item in list)
            {
                if (!AccessTokenContainer.CheckRegistered(item.AppId))
                {
                    AccessTokenContainer.Register(item.AppId,item.AppSecret);
                }
            }
        }
    }
}
