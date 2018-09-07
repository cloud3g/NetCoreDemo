using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.PermissionPolicy
{
    public class PermissionRequirement:IAuthorizationRequirement
    {
        /// <summary>
        /// 无权限action
        /// </summary>
        public string DeniedAction { get; set; }
        public PermissionRequirement(string deniedAction)
        {
            DeniedAction = deniedAction;
        }
    }
}
