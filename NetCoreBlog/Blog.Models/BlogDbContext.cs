using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Models
{
    public class BlogDbContext:DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> option):base(option)
        {

        }
        public DbSet<SysUserInfo> SysUserInfo { get; set; }
        public DbSet<BlogArticle> BlogArticle { get; set; }
        public DbSet<BlogCategory> BlogCategory { get; set; }
        public DbSet<BlogAnnouncement> BlogAnnouncement { get; set; }
        public DbSet<Friendslinks> Friendslinks { get; set; }
        public DbSet<Resource> Resource { get; set; }
        public DbSet<TimeLine> TimeLine { get; set; }

        public DbSet<SysUser> SysUser { get; set; }
        public DbSet<SysModule> SysModule { get; set; }
        public DbSet<SysRole> SysRole { get; set; }
        public DbSet<SysUserRole> SysUserRole { get; set; }
        public DbSet<SysModuleAction> SysModuleAction { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }
        public DbSet<SysRoleOperate> SysRoleOperate { get; set; }

        public DbSet<WC_OfficalAccounts> WC_OfficalAccounts { get; set; }
        public DbSet<WC_MessageResponse> WC_MessageResponse { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //关闭级联删除
            modelBuilder.Entity<BlogArticle>().HasOne(b => b.BlogCategory).WithMany(b=>b.BlogArticles).HasForeignKey(b=>b.CategoryId).OnDelete(DeleteBehavior.Restrict);

            //用户角色表

            modelBuilder.Entity<SysUserRole>().HasOne(s => s.SysUser).WithMany(s => s.SysUserRoles).HasForeignKey(s => s.UserId);
            modelBuilder.Entity<SysUserRole>().HasOne(s => s.SysRole).WithMany(s => s.SysUserRoles).HasForeignKey(s => s.RoleId);

            //角色模块表

            modelBuilder.Entity<RolePermission>().HasOne(p => p.SysRole).WithMany(p => p.RolePermissions).HasForeignKey(p => p.RoleId);
            modelBuilder.Entity<RolePermission>().HasOne(p => p.SysModule).WithMany(p => p.RolePermissions).HasForeignKey(p => p.ModuleId);
             

        }
    }
}
