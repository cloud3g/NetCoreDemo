using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Models
{
    /// <summary>
    /// 博客设置
    /// </summary>
    public class BlogSettingsViewModel
    {
        /// <summary>
        /// 网站标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 副标题
        /// </summary>
        public string Subtitle { get; set; }
        /// <summary>
        /// 网站链接
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 网站logo
        /// </summary>
        public string Logo { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Abstract { get; set; }
        /// <summary>
        /// 网站首页显示logo
        /// </summary>
        public string LogoText { get; set; }
        /// <summary>
        /// 网站关键词
        /// </summary>
        public string Keywords { get; set; }
        /// <summary>
        /// 网站描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 第三方统计代码
        /// </summary>
        public string Statistics { get; set; }
        /// <summary>
        /// 页脚
        /// </summary>
        public string Footer { get; set; }
        /// <summary>
        /// qq登陆Id
        /// </summary>
        public string QQAppId { get; set; }
        /// <summary>
        /// qq登陆key
        /// </summary>
        public string QQAppKey { get; set; }
    }
}
