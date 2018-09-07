using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Blog.Models
{
    public class SysUserInfo : Entity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name ="用户名")]
        [StringLength(20,MinimumLength =6,ErrorMessage ="用户名必须为6到20个字符")]
        public string LoginName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name ="密码")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "密码必须为6到20个字符")]
        public string Password { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [Display(Name ="昵称")]
        public string RealName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name ="状态")]
        public int Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name ="备注")]
        public string Remark { get; set; }

        /// <summary>
        ///最后登录失败时间 
        /// </summary>
        [Display(Name = "最后登录失败时间")]
        public DateTime? LastErrTime { get; set; }

        /// <summary>
        ///错误次数 
        /// </summary>
        [Display(Name = "错误次数")]
        public int ErrorCount { get; set; }

        
    }
}
