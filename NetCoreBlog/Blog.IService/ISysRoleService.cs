using Blog.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.IService
{
    public partial interface ISysRoleService
    {
        bool Allot(int roleId, int moduleId,List<int> actionId);
    }
}
