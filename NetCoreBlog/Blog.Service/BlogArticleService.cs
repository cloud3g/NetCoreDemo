
using Blog.IRepository;
using Blog.IService;
using Blog.Models;

namespace Blog.Service
{
    public class BlogArticleService : Service<BlogArticle>, IBlogArticleService
    {
        public BlogArticleService(IBlogArticleRepository rep) : base(rep)
        {
        }
    }
}
