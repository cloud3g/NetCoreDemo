using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Models
{
    public class ArticleViewModel
    {
        /// <summary>
        /// 没有数据时提示
        /// </summary>
        public string Msg { get; set; }
        public List<BlogArticle> Articles { get; set; }
    }
}
