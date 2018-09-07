
using Blog.IRepository;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Blog.Common;
using System.Linq;
using System.Data.SqlClient;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Npgsql;

namespace Blog.Repository
{
    public class BlogCategoryRepository : Repository<BlogCategory>, IBlogCategoryRepository
    {
        IHostingEnvironment HostingEnvironment { get; set; }
        public BlogCategoryRepository(BlogDbContext db, IHostingEnvironment _hostingEnvironment) : base(db)
        {
            HostingEnvironment = _hostingEnvironment;
        }

        public Response CascadingDeletion(int id)
        {
            if (id == 1)
            {
                return new Response() { Code = ResponseCode.Fail, Message = "不能删除默认分类" };
            }
            var entity = Db.Set<BlogCategory>().FirstOrDefault(c=>c.Id==id);
            if (entity == null)
            {
                return new Response() { Code = ResponseCode.Fail, Message = "未能找到删除项" };
            }
            var imgUrls = Db.Set<BlogCategory>().Where(c => c.Pid == id).Select(c => c.ImgUrl).ToList();
            imgUrls.Insert(0, entity.ImgUrl);
    
            using (var tran = Db.Database.BeginTransaction())
            {
                try
                {
                    var updateSql = "update BlogArticle set CategoryId=1 where CategoryId in(select Id from BlogCategory where Id=@id or Pid=@id)";
                    var deleteSql = "delete BlogCategory where Id=@id or Pid=@id";
                    int updateResult = Db.Database.ExecuteSqlCommand(updateSql, new NpgsqlParameter("@id", id));
                    int deleteResult = Db.Database.ExecuteSqlCommand(deleteSql, new NpgsqlParameter("@id", id));
                    
                    if (deleteResult > 0)
                    {
                        tran.Commit();
                        //删除已删除记录的图片
                        foreach (var item in imgUrls)
                        {
                            if(!string.IsNullOrEmpty(item))
                            {
                               var file = Path.Combine(HostingEnvironment.WebRootPath, item.TrimStart('/'));
                                if (File.Exists(file))
                                {
                                    File.Delete(file);
                                }
                            }
                        }
                        return new Response() { Code= ResponseCode.Success, Message="删除成功！" };
                    }
                    else
                    {
                        tran.Rollback();
                        return new Response() { Code = ResponseCode.Success, Message = "删除失败,请联系管理员！" };
                    }
                }
                catch
                {
                    
                    tran.Rollback();
                    return new Response() { Code = ResponseCode.Success, Message = "删除失败,请联系管理员！" };
                }
            }
        }
    }
}
