
using System.Collections.Generic;
using Blog.IRepository;
using Blog.IService;
using Blog.Models;

namespace Blog.Service
{
    public class SysUserInfoService : Service<SysUserInfo>, ISysUserInfoService
    {
        public SysUserInfoService(ISysUserInfoRepository rep) : base(rep)
        {
        }

 
    }
}
