using SMS.Data;
using SMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Service
{
    public class RoleMasterService
    {
        public readonly RoleMasterProvider roleMasterProvider;
        public RoleMasterService()
        {
            roleMasterProvider = new RoleMasterProvider();
        }
        public List<RoleModel> GetAllRoles()
        {
            var roles = roleMasterProvider.GetAllRoles();
            return roles;
        }
       

        public webpages_Roles GetRolesById(int Id)
        {
            return roleMasterProvider.GetRolesById(Id);
        }
        public webpages_Roles GetRolesByName(string roleName)
        {
            return roleMasterProvider.GetRolesByName(roleName);
        }

        public webpages_Roles CreateRole(webpages_Roles role)
        {
            return roleMasterProvider.CreateRole(role);
        }
    }
}
