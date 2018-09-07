using Blog.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.IService
{
    public partial interface ISysUserService
    {
        Response AllotRole(List<int> ids, int userId);
    }
}
