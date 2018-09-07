using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blog.Models
{
    public class TimeLine:Entity
    {
        /// <summary>
        /// 内容
        /// </summary>
        [Display(Name ="内容")]
        public string Content { get; set; }

    }
}
