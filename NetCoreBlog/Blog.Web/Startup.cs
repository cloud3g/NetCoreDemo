using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;
using Blog.Web.Models;
using Blog.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Blog.Web.Middleware;
using Blog.Web.PermissionPolicy;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<BlogDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("blogStr")));
            Blog.Core.DependencyInjection.Init(services);
            
            services.AddSession();
            services.AddMvc(options=>options.Filters.Add<Blog.Core.ExcFilter>()).AddJsonOptions(op => op.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver());
            //绑定博客设置json节点
            services.ConfigureWritable<BlogSettingsViewModel>(Configuration.GetSection("BlogSettings"), "appsettings.custom.json");

            //绑定博主信息json节点
            services.ConfigureWritable<BloggerInfoViewModel>(Configuration.GetSection("BloggerInfo"), "appsettings.custom.json");

            services.AddAuthorization(option => {
                option.AddPolicy("Permission", policy => policy.Requirements.Add(new PermissionRequirement("/admin/account/denied")));
            })
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(option=> {
                option.LoginPath = new PathString("/admin/account/index");
                option.AccessDeniedPath = new PathString("/admin/account/denied");
            });
            
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    //app.UseBrowserLink();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}
          
           
            loggerFactory.AddNLog();//添加NLog  
            app.AddNLogWeb();
            env.ConfigureNLog("nlog.config");//读取Nlog配置文件  
            app.UseStaticFiles();
          //  app.UseSession();
            app.UseAuthentication();
            app.UseCustomErrorPages();
       
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name:"admin",
                    template:"{area:exists}/{controller=account}/{action=Index}/{id?}"
                    );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
    
        }
    }
}
