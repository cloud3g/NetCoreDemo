using Blog.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blog.Models
{
    public class Resource:Entity
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Display(Name ="标题")]
        public string Name { get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        [Display(Name = "摘要")]
        public string Abstract { get; set; }
        /// <summary>
        /// 演示地址
        /// </summary>
        [Display(Name = "演示地址")]
        public string Demolink { get; set; }
        /// <summary>
        /// 下载地址
        /// </summary>
        [Display(Name = "下载地址")]
        public string Downloadlink { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        [Display(Name = "分类")]
        public ResourceType Type { get; set; }
        /// <summary>
        /// 封面
        /// </summary>
        [Display(Name = "封面")]
        public string ResourceCoverSrc { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        [Display(Name = "作者")]
        public string Submitter { get; set; }
    }
}
