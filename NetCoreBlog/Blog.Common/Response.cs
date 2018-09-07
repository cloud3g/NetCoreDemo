using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blog.Common
{
    public class Response
    {
        /// <summary>
        /// 返回代码（成功 失败等） 
        /// </summary>
        public ResponseCode Code { get; set; }
        /// <summary>
        /// 返回需要的内容
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 预留字段，自行安排
        /// </summary>
        public string Value { get; set; }
    }

    public enum ResponseCode
    {
        /// <summary>
        /// 失败
        /// </summary>
        [Display(Name = "失败")]
        Fail,
        /// <summary>
        /// 成功
        /// </summary>
        [Display(Name = "成功")]
        Success,
        /// <summary>
        /// 验证错误
        /// </summary>
        [Display(Name = "验证错误")]
        VerificationFailed,
        /// <summary>
        /// 服务器错误
        /// </summary>
        [Display(Name ="服务器错误")]
        ServerError
    }
}
