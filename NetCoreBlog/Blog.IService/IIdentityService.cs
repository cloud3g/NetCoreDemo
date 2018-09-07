using Blog.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.IService
{
    public partial interface ISysUserService : IService<SysUser>
    {
    }
    public partial interface ISysUserRoleService : IService<SysUserRole>
    {
    }
    public partial interface ISysRoleService : IService<SysRole>
    {
    }
    public interface ISysRoleOperateService : IService<SysRoleOperate>
    {
    }
    public interface IRolePermissionService : IService<RolePermission>
    {
    }
    public interface ISysModuleService : IService<SysModule>
    {
    }
    public interface ISysModuleActionService : IService<SysModuleAction>
    {
    }
}
