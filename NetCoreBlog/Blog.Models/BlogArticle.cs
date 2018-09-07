using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Blog.Models
{
    public class BlogArticle:Entity
    {
        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
        public string Submitter { get; set; }
        /// <summary>
        /// 置顶
        /// </summary>
        [Display(Name = "置顶")]
        public bool? Stick { get; set; }
        /// <summary>
        /// 推荐
        /// </summary>
        [Display(Name ="推荐")]
        public bool? Recommend { get; set; }
        /// <summary>
        /// 博客标题
        /// </summary>
        [Display(Name = "博客标题")]
        public string Title { get; set; }

        /// <summary>
        /// 类别Id
        /// </summary>
        [Display(Name = "类别Id")]
        public int CategoryId { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        [Display(Name = "类别名称")]
        public string CategoryName { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [Display(Name = "内容")]
        public string Content { get; set; }

        /// <summary>
        /// 访问量
        /// </summary>
        [Display(Name = "访问量")]
        public int? Traffic { get; set; }

        /// <summary>
        /// 评论数量
        /// </summary>
        [Display(Name = "评论数量")]
        public int? CommentNum { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }
        /// <summary>
        /// 文章图片
        /// </summary>
        [Display(Name = "图片")]
        public string ImgUrl { get; set; }

        public virtual BlogCategory BlogCategory { get; set; }
    }
}
