using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Blog.Web.Core;
using Microsoft.Extensions.DependencyInjection;
using Blog.Web.PermissionPolicy;
using Blog.Web.Core.WeChat;

namespace Blog.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // BuildWebHost(args).Run();
            
            var host = BuildWebHost(args) 
             .Migrate().WechatTokenRegister();//初始化数据
            using (var scope = host.Services.CreateScope())

            {

                var services = scope.ServiceProvider;

                try

                {

                    RolePermissionInit.Init(services);
                  
                }

                catch (Exception ex)

                {

                    //var logger = services.GetRequiredService<ILogger<Program>>();

                    //logger.LogError(ex, "An error occurred seeding the DB");
                    throw new Exception(ex.Message+ ",An error occurred seeding the DB");
                }

            }
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
               .ConfigureAppConfiguration(config => config.AddJsonFile("appsettings.custom.json", optional: true, reloadOnChange: true))
                .UseStartup<Startup>()
                .Build();
    }
}
