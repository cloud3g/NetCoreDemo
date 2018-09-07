
using Blog.IRepository;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Blog.Repository
{
    public class SysUserInfoRepository : Repository<SysUserInfo>, ISysUserInfoRepository
    {
        public SysUserInfoRepository(BlogDbContext db) : base(db)
        {
        }

   
    }
}
