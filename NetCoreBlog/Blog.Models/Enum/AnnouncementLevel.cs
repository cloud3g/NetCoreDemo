using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blog.Models.Enum
{
    public enum AnnouncementLevel
    {
        [Display(Name ="普通")]
        /// <summary>
        /// 普通
        /// </summary>
        Normal,
        [Display(Name = "一般")]
        /// <summary>
        /// 一般
        /// </summary>
        Commonly,
        [Display(Name = "重要")]
        /// <summary>
        /// 重要
        /// </summary>
        Important
    }
}
