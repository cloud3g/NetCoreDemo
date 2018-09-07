using Blog.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Service
{
    public partial class SysRoleService
    {
        public bool Allot(int roleId, int moduleId, List<int> actionId)
        {
            var module = _moduleRepository.Find(m => m.Id == moduleId);
            var roles= _operateRepository.GetList(s => s.RoleId == roleId&&s.Area==module.AreaName&&s.Controller==module.ControllerName).ToList();
            var actions = _actionRepository.GetList(a => actionId.Contains(a.Id));
            foreach (var role in roles)
            {
                _operateRepository.Delete(role, false);
            }
            foreach (var action in actions)
            {
                _operateRepository.Add(new Models.SysRoleOperate() { Enable=true, Area=module.AreaName, Controller=module.ControllerName, CreateTime=DateTime.Now, UpdateTime=DateTime.Now, RoleId=roleId, Action=action.Code }, false);
            }
            return Rep.SaveChanges()>0;
        }
    }
}
