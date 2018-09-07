using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.PermissionPolicy
{
    public class RolePermissionViewModel
    {
        /// <summary>
        /// 角色名
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 请求action
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// 请求controller
        /// </summary>
        public string ControllerName { get; set; }
        /// <summary>
        /// 请求area
        /// </summary>
        public string AreaName { get; set; }

    }
}
