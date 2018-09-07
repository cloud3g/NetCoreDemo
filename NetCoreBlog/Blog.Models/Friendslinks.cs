using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blog.Models
{
    /// <summary>
    /// 友情链接表
    /// </summary>
    public class Friendslinks:Entity
    {
        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name ="排序")]
        public int Sort { get; set; }
        /// <summary>
        /// 网站url
        /// </summary>
        [Display(Name = "链接地址")]
        public string Url { get; set; }
        /// <summary>
        /// 启用
        /// </summary>
        [Display(Name = "启用")]
        public bool Enable { get; set; }

        /// <summary>
        /// 网站名称
        /// </summary>
        [Display(Name = "网站名称")]
        public string WebName { get; set; }
        /// <summary>
        /// 站长email
        /// </summary>
        [Display(Name = "站长Email")]
        public string Email { get; set; }
        /// <summary>
        /// 网站logo
        /// </summary>
        [Display(Name = "Logo")]
        public string Logo { get; set; }
     
    }
}
