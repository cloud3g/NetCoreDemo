
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Models
{
    /// <summary>
    /// 数据初始化
    /// </summary>
    public static class SeedData
    {
        public static void Initialize(BlogDbContext context)
        {
     
            using (context)
            {
                
                if (context.SysUserInfo.Any())
                {
                    return;//如果有管理员信息表示已经初始化
                }
                context.SysUserInfo.Add(
                    new SysUserInfo() { Status=1, LoginName="admin", RealName="管理员", Password="123456", CreateTime= DateTime.Now, UpdateTime=DateTime.Now }
                    );
                context.BlogCategory.Add(
                    new BlogCategory() { Id=1, CategoryType= Enum.BlogCategoryType.General,Enable=true, Name="默认分类" , Pid=0, Sort=99, CreateTime= DateTime.Now, UpdateTime= DateTime.Now, IsNav=false}
                    );
                context.SaveChanges();
            }
        }
    }
}
