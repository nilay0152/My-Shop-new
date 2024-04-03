using SMS.Data;
using SMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Service
{
    public class FormRoleMappingService
    {
        public readonly FormRoleMappingProvider formRoleMappingProvider;
        public FormRoleMappingService()
        {
            formRoleMappingProvider = new FormRoleMappingProvider();
        }
        public FormRoleMapping CheckFormAccess(string _formaccessCode)
        {
            return formRoleMappingProvider.CheckFormAccess(_formaccessCode);
        }

        public int InsertRoleRights(FormRoleMapping rolerights)
        {
            return formRoleMappingProvider.InsertRoleRights(rolerights);
        }
        public List<FormRoleMapping> GetAllRoleRightsById(int RoleId)
        {
            return formRoleMappingProvider.GetAllRoleRightsById(RoleId);
        }

        public bool UpdateRoleRights(IEnumerable<FormRoleMapping> rolerights, int CreatedBy, int UpdatedBy)
        {

            return formRoleMappingProvider.UpdateRoleRights(rolerights, CreatedBy, UpdatedBy);
        }

        public List<MenuVW> GetMenu(int userID)
        {
            return formRoleMappingProvider.GetMenu(userID);
        }
    }
}
