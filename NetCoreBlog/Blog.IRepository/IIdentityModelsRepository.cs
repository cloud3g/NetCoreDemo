using Blog.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.IRepository
{
    public partial interface ISysUserRepository : IRepository<SysUser>
    {
    }
    public partial interface ISysUserRoleRepository : IRepository<SysUserRole>
    {
    }
    public partial interface ISysRoleRepository : IRepository<SysRole>
    {
    }
    public interface ISysRoleOperateRepository : IRepository<SysRoleOperate>
    {
    }
    public interface IRolePermissionRepository : IRepository<RolePermission>
    {
    }
    public interface ISysModuleRepository : IRepository<SysModule>
    {
    }
    public interface ISysModuleActionRepository : IRepository<SysModuleAction>
    {
    }
}
