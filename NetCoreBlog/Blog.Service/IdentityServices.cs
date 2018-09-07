using Blog.IRepository;
using Blog.IService;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Service
{
    public partial class SysUserService : Service<SysUser>, ISysUserService
    {
        public SysUserService(ISysUserRepository rep) : base(rep)
        {
        }
    }
    public partial class SysUserRoleService : Service<SysUserRole>, ISysUserRoleService
    {
        public SysUserRoleService(ISysUserRoleRepository rep) : base(rep)
        {
        }
    }
    public partial class SysRoleService : Service<SysRole>, ISysRoleService
    {
        private ISysModuleRepository _moduleRepository;
        private ISysModuleActionRepository _actionRepository;
        private ISysRoleOperateRepository _operateRepository;
        public SysRoleService(ISysRoleRepository rep, ISysModuleRepository moduleRepository, ISysModuleActionRepository actionRepository, ISysRoleOperateRepository operateRepository) : base(rep)
        {
            _operateRepository = operateRepository;
            _moduleRepository = moduleRepository;
            _actionRepository = actionRepository;
        }
    }
    public class SysRoleOperateService : Service<SysRoleOperate>, ISysRoleOperateService
    {
        public SysRoleOperateService(ISysRoleOperateRepository rep) : base(rep)
        {
        }
    }
    public class RolePermissionService : Service<RolePermission>, IRolePermissionService
    {
        public RolePermissionService(IRolePermissionRepository rep) : base(rep)
        {
        }
    }
    public class SysModuleService : Service<SysModule>, ISysModuleService
    {
        public SysModuleService(ISysModuleRepository rep) : base(rep)
        {
        }
    }
    public class SysModuleActionService : Service<SysModuleAction>, ISysModuleActionService
    {
        public SysModuleActionService(ISysModuleActionRepository rep) : base(rep)
        {
        }
    }
}
