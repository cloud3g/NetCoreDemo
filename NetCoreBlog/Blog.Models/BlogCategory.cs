using Blog.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Blog.Models
{
    public class BlogCategory:Entity
    {
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
        public override int Id { get => base.Id; set => base.Id = value; }
        /// <summary>
        /// 栏目名
        /// </summary>
        [Display(Name = "栏目名")]
        public string Name { get; set; }
        /// <summary>
        /// 上级Id
        /// </summary>
        [Display(Name = "上级栏目")]
        public int? Pid { get; set; }
        /// <summary>
        /// 栏目类型
        /// </summary>
        [Display(Name = "栏目类型")]
        public BlogCategoryType CategoryType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string  Remark { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序")]
        public int? Sort { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        [Display(Name = "图片")]
        public string ImgUrl { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [StringLength(8000)]
        [Display(Name = "内容")]
        public string BodyContent { get; set; }
        /// <summary>
        /// 首页显示
        /// </summary>
        [Display(Name = "首页显示")]
        public bool IsNav { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [Display(Name = "是否启用")]
        public bool Enable { get; set; }
        /// <summary>
        /// 搜索引擎关键字
        /// </summary>
        [StringLength(255, ErrorMessage = "{0}长度最多{1}个字符。")]
        [Display(Name = "META关键词")]
        public string Meta_Keywords { get; set; }
        // <summary>
        /// 搜索引擎描述
        /// </summary>
        [StringLength(255, ErrorMessage = "{0}长度最多{1}个字符。")]
        [Display(Name = "META网页描述")]
        public string Meta_Description { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        [DataType(DataType.Url)]
        [StringLength(500)]
        [Display(Name = "链接地址")]
        public string LinkUrl { get; set; }
        public virtual ICollection<BlogArticle> BlogArticles{ get; set; }

    }
}
