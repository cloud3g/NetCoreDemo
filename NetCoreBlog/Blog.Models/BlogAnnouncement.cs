using Blog.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blog.Models
{
    /// <summary>
    /// 网站公告
    /// </summary>
    public class BlogAnnouncement:Entity
    {
        /// <summary>
        /// 内容
        /// </summary>
        [Display(Name = "内容")]
        public string Content { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        [Display(Name = "级别")]
        public AnnouncementLevel Level { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [Display(Name = "是否启用")]
        public bool Enable { get; set; }
    }
}
