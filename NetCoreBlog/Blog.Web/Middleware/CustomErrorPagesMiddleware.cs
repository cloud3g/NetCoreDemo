using Blog.Common;
using Blog.Common.Log;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Web.Middleware
{
    public class CustomErrorPagesMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Microsoft.Extensions.Logging.ILogger _logger;
        private readonly IHostingEnvironment _env;
        public CustomErrorPagesMiddleware(
            RequestDelegate next,
            ILoggerFactory loggerFactory,
            IHostingEnvironment env)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<CustomErrorPagesMiddleware>();
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(0, ex, "An unhandled exception has occurred while executing the request");

                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, the error page middleware will not be executed.");
                    throw;
                }
                try
                {
                    context.Response.Clear();
                    context.Response.StatusCode = 500;
                    return;
                }
                catch (Exception ex2)
                {
                    _logger.LogError(0, ex2, "An exception was thrown attempting to display the error page.");
                }
                throw;
            }
            finally
            {
                var statusCode = context.Response.StatusCode;
                if (statusCode == 404 || statusCode == 500||statusCode==400|| statusCode == 403)
                {
                  
                    await ErrorPage.ResponseAsync(context.Response, statusCode,_env);
                }
                
            }
        }
    }

    public static class ErrorPage
    {
        public static async Task ResponseAsync(HttpResponse response, int statusCode, IHostingEnvironment env)
        {
            if (statusCode == 404)
            {
                 
                 await response.WriteAsync(Page404,Encoding.UTF8);
           
            }
            else if (statusCode == 500)
            {
                await response.WriteAsync(Page500);
            }else if (statusCode == 400)
            {
                await response.WriteAsync(Page400);
            }else if (statusCode == 403)
            {
                //response.Redirect("/admin/account/Denied");
                await response.WriteAsync("操作失败,没有权限");
            }
        }

        private static string Page404 => "<!DOCTYPE html><html><head><meta http-equiv=\"X-UA-Compatible\" content=\"IE=EmulateIE7\"><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"><meta name=\"viewport\" content=\"width=device-width\"><title>嗨呀…您访问的页面不存在</title><style>body{text-align:center}.statuscode{margin-top:18vh;font-size:100px;color:#009688;margin-bottom:20px}h2{font-size:20px;color:#393D49}h3,h4{margin:20px 0}h3{font-size:16px}h3 a{padding:10px 20px}</style></head><body><div class=\"statuscode\">404</div><h2>嗨呀…您访问的页面不存在</h2><h3><a href=\"javascript:history.back(-1);\"><i class=\"fa fa-arrow-circle-left fa-fw\"></i>返回</a> <a href=\"http://www.lyblogs.cn\"><i class=\"fa fa-home fa-fw\"></i>网站首页</a></h3><h4><a href=\"http://www.lyblogs.cn\" style=\"color:#009688;font-weight:bold\">不落阁</a>&nbsp;提醒您 - 您可能输入了错误的网址，或者该网页已删除或移动</h4></body></html>";

        private static string Page500 => "<!DOCTYPE html><html><head><meta http-equiv=\"X-UA-Compatible\" content=\"IE=EmulateIE7\"><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"><meta name=\"viewport\" content=\"width=device-width\"><title>嗨呀…服务器发生错误</title><style>body{text-align:center}.statuscode{margin-top:18vh;font-size:100px;color:#009688;margin-bottom:20px}h2{font-size:20px;color:#393D49}h3,h4{margin:20px 0}h3{font-size:16px}h3 a{padding:10px 20px}</style></head><body><div class=\"statuscode\">500</div><h2>嗨呀…服务器发生错误</h2><h3><a href=\"javascript:history.back(-1);\"><i class=\"fa fa-arrow-circle-left fa-fw\"></i>返回</a> <a href=\"http://www.lyblogs.cn\"><i class=\"fa fa-home fa-fw\"></i>网站首页</a></h3><h4><a href=\"http://www.lyblogs.cn\" style=\"color:#009688;font-weight:bold\">不落阁</a>&nbsp;提醒您 - 服务器发生错误,请联系管理员</h4></body></html>";

        private static string Page400 => "<!DOCTYPE html><html><head><meta http-equiv=\"X-UA-Compatible\" content=\"IE=EmulateIE7\"><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"><meta name=\"viewport\" content=\"width=device-width\"><title>嗨呀…您没有权限访问该页面</title><style>body{text-align:center}.statuscode{margin-top:18vh;font-size:100px;color:#009688;margin-bottom:20px}h2{font-size:20px;color:#393D49}h3,h4{margin:20px 0}h3{font-size:16px}h3 a{padding:10px 20px}</style></head><body><div class=\"statuscode\">400</div><h2>嗨呀…您没有权限访问该页面</h2><h3><a href=\"javascript:history.back(-1);\"><i class=\"fa fa-arrow-circle-left fa-fw\"></i>返回</a> <a href=\"http://www.lyblogs.cn\"><i class=\"fa fa-home fa-fw\"></i>网站首页</a></h3><h4><a href=\"http://www.lyblogs.cn\" style=\"color:#009688;font-weight:bold\">不落阁</a>&nbsp;提醒您 - 服务器发生错误,请联系管理员</h4></body></html>";
    }

    public static class CustomErrorPagesExtensions
    {
        public static IApplicationBuilder UseCustomErrorPages(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<CustomErrorPagesMiddleware>();
        }
    }

  
}
