using Blog.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.IRepository
{
    public partial interface ISysUserRepository
    {
        Response AllotRole(List<int> ids, int userId);
    }
}
