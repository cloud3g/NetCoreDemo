using Blog.Common.Log;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Blog.Core
{
    public class ExcFilter : ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public ExcFilter(
            IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public override void OnException(ExceptionContext context)
        {
            if (_hostingEnvironment.IsDevelopment())
            {
               
                return;
            }
            Exception exp = context.Exception;
            //获取ex的第一级内部异常
            Exception innerEx = exp.InnerException == null ? exp : exp.InnerException;
            //循环获取内部异常直到获取详细异常信息为止
            while (innerEx.InnerException != null)
            {
                innerEx = innerEx.InnerException;
            }
            NLogLogger nlog = new NLogLogger();
            bool isAjaxCall = context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";
            if (isAjaxCall)
            {

                nlog.Error(innerEx.Message);
                JsonConvert.SerializeObject(new { status = 1, msg = "请求发生错误，请联系管理员" });
            }
            else
            {
                nlog.Error("Error", exp);
                ViewResult vireResult = new ViewResult();
                vireResult.ViewName = "error";
                context.Result = vireResult;
            }
            context.ExceptionHandled = true;
        }

    }
}
