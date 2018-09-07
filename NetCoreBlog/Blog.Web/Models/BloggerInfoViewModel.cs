using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Models
{
    /// <summary>
    /// 博主信息
    /// </summary>
    public class BloggerInfoViewModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string  Icon { get; set; }
        /// <summary>
        /// 介绍
        /// </summary>
        public string Introduction { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// QQ交流网址
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// Email发送网址
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 微博交流网址
        /// </summary>
        public string Weibo { get; set; }
        /// <summary>
        /// Git网址
        /// </summary>
        public string Git { get; set; }
    }
}
