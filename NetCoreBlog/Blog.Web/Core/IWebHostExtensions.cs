using Blog.Models;
using Blog.Web.Core.WeChat;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Core
{
    /// <summary>
    /// IWebHost扩展类,用于初始化数据
    /// </summary>
    public static class IWebHostExtensions
    {
        public static IWebHost Migrate(this IWebHost webhost)
        {
            using (var scope = webhost.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var dbContext = scope.ServiceProvider.GetRequiredService<BlogDbContext>())
                {
                    dbContext.Database.Migrate();
                    SeedData.Initialize(dbContext);//初始化数据
               
                    return webhost;
                }
            }
        }
        public static IWebHost WechatTokenRegister(this IWebHost webhost)
        {
            using (var scope = webhost.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var dbContext = scope.ServiceProvider.GetRequiredService<BlogDbContext>())
                {
                 
                    WechatAccessTokenRegister.Register(dbContext);//注册微信
                    return webhost;
                }
            }
        }
    }
}
