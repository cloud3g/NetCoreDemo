using Blog.Common;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Repository
{
    public partial class SysUserRepository
    {
        public Response AllotRole(List<int> ids, int userId)
        {
            var opsResult = new Response();
            var userRoles = Db.Set<SysUserRole>().Where(s => s.UserId == userId);
            foreach (var item in userRoles)
            {
                Db.Set<SysUserRole>().Remove(item);
            }
            foreach (var roleId in ids)
            {
                Db.Set<SysUserRole>().Add(new SysUserRole() { CreateTime=DateTime.Now, Enable=true, UpdateTime=DateTime.Now, RoleId=roleId, UserId=userId });
            }
            if(Db.SaveChanges()>0)
            {
                opsResult.Code = ResponseCode.Success;
                opsResult.Message = "分配角色成功！";
                
            }
            else
            {
                opsResult.Code = ResponseCode.Fail;
                opsResult.Message = "分配角色失败！";
            }
            return opsResult;
        }
    }
}
