using Blog.Models.Enum;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Blog.Models
{
    public class SysUser:Entity
    {
        public SysUser() : base() { CreateTime = DateTime.Now; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "用户名必须为6到20个字符")]
        public string LoginName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "密码必须为6到20个字符")]
        public string Password { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [Display(Name = "昵称")]
        public string RealName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [Display(Name ="头像")]
        public string Icon { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        public int Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        ///最后登录失败时间 
        /// </summary>
        [Display(Name = "最后登录失败时间")]
        public DateTime? LastErrTime { get; set; }
        /// <summary>
        ///最后登录Ip
        /// </summary>
        [Display(Name = "最后登录IP")]
        public string IP { get; set; }

        /// <summary>
        ///错误次数 
        /// </summary>
        [Display(Name = "错误次数")]
        public int ErrorCount { get; set; }
        public virtual ICollection<SysUserRole> SysUserRoles { get; set; }
    }
    public class SysUserRole:Entity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public virtual SysUser SysUser { get; set; }
        public virtual SysRole SysRole { get; set; }
        public bool Enable { get; set; }
    }
    public class SysRole:Entity
    {
        public SysRole() : base() { CreateTime = DateTime.Now; }
        [Display(Name ="名称")]
        public string Name { get; set; }
        [Display(Name = "备注")]
        public string Remark { get; set; }
        public virtual ICollection<SysUserRole> SysUserRoles { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
        public virtual ICollection<SysRoleOperate> SysRoleOperate { get; set; }
    }
    public class  SysRoleOperate:Entity
    {
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual SysRole SysRole { get; set; }
        public string Action { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public bool Enable { get; set; }
    }
    public class RolePermission:Entity
    {
        public int RoleId { get; set; }
        public int ModuleId { get; set; }
        public virtual SysRole SysRole { get; set; }
        public virtual SysModule SysModule { get; set; }
    }
    public class SysModule:Entity
    {
        public string ControllerName { get; set; }
        public string Name { get; set; }
        public string AreaName { get; set; }
        public ModuleType  Type { get; set; }
        public bool IsSysMenu { get; set; }
        public string Icon { get; set; }
        public int Pid { get; set; }
        public string PName { get; set; }
        public int Sort { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
        public virtual ICollection<SysModuleAction> SysModuleActions { get; set; }
    }
    public class SysModuleAction:Entity
    {
        /// <summary>
        /// action名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 当前控制器下Action唯一标识
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 是否有权限
        /// </summary>
        public bool  Enable { get; set; }
        public int ModuleId { get; set; }
        [ForeignKey("ModuleId")]
        public virtual SysModule SysModule { get; set; }
    }
}
