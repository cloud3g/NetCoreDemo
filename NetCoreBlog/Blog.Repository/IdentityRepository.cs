using Blog.IRepository;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Repository
{
    public partial class SysUserRepository : Repository<SysUser>, ISysUserRepository
    {
        public SysUserRepository(BlogDbContext db) : base(db)
        {
        }
    }
    public partial class SysUserRoleRepository : Repository<SysUserRole>, ISysUserRoleRepository
    {
        public SysUserRoleRepository(BlogDbContext db) : base(db)
        {
        }
    }
    public partial class SysRoleRepository : Repository<SysRole>, ISysRoleRepository
    {
        public SysRoleRepository(BlogDbContext db) : base(db)
        {
        }
    }
    public class SysRoleOperateRepository : Repository<SysRoleOperate>, ISysRoleOperateRepository
    {
        public SysRoleOperateRepository(BlogDbContext db) : base(db)
        {
        }
    }
    public class RolePermissionRepository : Repository<RolePermission>, IRolePermissionRepository
    {
        public RolePermissionRepository(BlogDbContext db) : base(db)
        {
        }
    }
    public class SysModuleRepository : Repository<SysModule>, ISysModuleRepository
    {
        public SysModuleRepository(BlogDbContext db) : base(db)
        {
        }
    }
    public class SysModuleActionRepository : Repository<SysModuleAction>, ISysModuleActionRepository
    {
        public SysModuleActionRepository(BlogDbContext db) : base(db)
        {
        }
    }
}
