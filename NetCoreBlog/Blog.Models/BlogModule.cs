using Blog.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Blog.Models
{
    public class BlogModule:Entity
    {
        //模块名称
        public string Name { get; set; }
        //控制器
        public string ControllerName { get; set; }
        //区域
        public string AareaName { get; set; }
        //模块唯一标识
        public string Code { get; set; }
        //模块备注
        public string Remark { get; set; }
        [Display(Name = "排序")]
        public int? Sort { get; set; }
        [Display(Name = "状态")]
        public bool Enable { get; set; }
        //模块类型
        public ModuleType Type { get; set; }
        [Display(Name = "上级Id")]
        public string ParentId { get; set; }
        [Display(Name = "图标")]
        public string Icon { get; set; }

    }
    /// <summary>
    /// 模块操作
    /// </summary>
    public class BlogModuleOpt:Entity
    {
        //Action名称
        public string Name { get; set; }
        //Action标识
        public string KeyCode { get; set; }
        public int ModuleId { get; set; }
        public bool IsValid { get; set; }
        [ForeignKey("ModuleId")]
        public virtual BlogModule BlogModule { get; set; }

    }
}
