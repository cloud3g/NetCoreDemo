using Blog.Common;
using Blog.IRepository;
using Blog.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Service
{
    public partial class SysUserService
    {
        public Response AllotRole(List<int> ids, int userId)
        {
            return (Rep as ISysUserRepository).AllotRole(ids, userId);
        }
    }
}
