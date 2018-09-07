
using Blog.IRepository;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repository
{
    public class BlogArticleRepository : Repository<BlogArticle>, IBlogArticleRepository
    {
        public BlogArticleRepository(BlogDbContext db) : base(db)
        {
        }
    }
}
