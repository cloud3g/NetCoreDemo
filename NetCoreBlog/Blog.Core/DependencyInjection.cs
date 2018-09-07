using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Blog.IRepository;
using Blog.Repository;
using Blog.IService;
using Blog.Service;

namespace Blog.Core
{
    public class DependencyInjection
    {
        public static void Init(IServiceCollection service)
        {
            service.AddScoped<ISysUserInfoRepository, SysUserInfoRepository>();
            service.AddScoped<ISysUserInfoService, SysUserInfoService>();

            service.AddScoped<IBlogArticleRepository, BlogArticleRepository>();
            service.AddScoped<IBlogArticleService, BlogArticleService>();

            service.AddScoped<IBlogCategoryRepository, BlogCategoryRepository>();
            service.AddScoped<IBlogCategoryService, BlogCategoryService>();

            service.AddScoped<IBlogAnnouncementRepository, BlogAnnouncementRepository>();
            service.AddScoped<IBlogAnnouncementService, BlogAnnouncementService>();

            service.AddScoped<IFriendslinksRepository, FriendslinksRepository>();
            service.AddScoped<IFriendslinksService, FriendslinksService>();

            service.AddScoped<IResourceRepository, ResourceRepository>();
            service.AddScoped<IResourceService, ResourceService>();

            service.AddScoped<ITimeLineRepository, TimeLineRepository>();
            service.AddScoped<ITimeLineService, TimeLineService>();

            service.AddScoped<ISysUserRepository, SysUserRepository>();
            service.AddScoped<ISysUserService, SysUserService>();

            service.AddScoped<ISysUserRoleRepository, SysUserRoleRepository>();
            service.AddScoped<ISysUserRoleService, SysUserRoleService>();

            service.AddScoped<ISysRoleRepository, SysRoleRepository>();
            service.AddScoped<ISysRoleService, SysRoleService>();

            service.AddScoped<ISysRoleOperateRepository, SysRoleOperateRepository>();
            service.AddScoped<ISysRoleOperateService, SysRoleOperateService>();

            service.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            service.AddScoped<IRolePermissionService, RolePermissionService>();

            service.AddScoped<ISysModuleRepository, SysModuleRepository>();
            service.AddScoped<ISysModuleService, SysModuleService>();

            service.AddScoped<ISysModuleActionRepository, SysModuleActionRepository>();
            service.AddScoped<ISysModuleActionService, SysModuleActionService>();

            service.AddScoped<IWC_OfficalAccountsRepository, WC_OfficalAccountsRepository>();
            service.AddScoped<IWC_OfficalAccountsService, WC_OfficalAccountsService>();

            service.AddScoped<IWC_MessageResponseRepository, WC_MessageResponseRepository>();
            service.AddScoped<IWC_MessageResponseService, WC_MessageResponseService>();
        }
    }
}
