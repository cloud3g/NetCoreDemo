using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blog.Models.Enum
{
    public enum ModuleType
    {
        /// <summary>
        /// 目录
        /// </summary>
        [Display(Name ="目录")]
        Catelog,
        /// <summary>
        /// 页面
        /// </summary>
        [Display(Name ="页面")]
        Page
    }
}
