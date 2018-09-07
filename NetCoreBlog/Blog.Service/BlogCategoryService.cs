
using Blog.IRepository;
using Blog.IService;
using Blog.Models;
using System.Data.SqlClient;
using Blog.Common;
using System.Linq;
using Npgsql;

namespace Blog.Service
{
    public class BlogCategoryService : Service<BlogCategory>, IBlogCategoryService
    {
        IBlogArticleRepository BlogArticleRepository { get; set; }
        public BlogCategoryService(IBlogCategoryRepository rep, IBlogArticleRepository blogArticleRepository) : base(rep)
        {
            BlogArticleRepository = blogArticleRepository;
        }

        public bool SetNav(int id, bool value)
        {
           return Rep.ExecuteSqlCommand($"UPDATE \"BlogCategory\" SET \"IsNav\"=@value WHERE \"Id\"=@id",new NpgsqlParameter("@value",value),new NpgsqlParameter("@id",id))>0;
        }
        public bool SetEnable(int id, bool value)
        {
            return Rep.ExecuteSqlCommand($"UPDATE \"BlogCategory\" SET \"Enable\"=@value WHERE \"Id\"=@id", new NpgsqlParameter("@value", value), new NpgsqlParameter("@id", id)) > 0;
        }


        public Response CascadingDeletion(int id)
        {
           return (Rep as IBlogCategoryRepository).CascadingDeletion(id);

        }
    }
}
