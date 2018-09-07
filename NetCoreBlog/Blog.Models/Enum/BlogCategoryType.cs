using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blog.Models.Enum
{
    public enum BlogCategoryType
    {
        [Display(Name = "常规栏目")]
        General,
        [Display(Name = "单页栏目")]
        Page,
        [Display(Name = "链接栏目")]
        Link
    }
}
