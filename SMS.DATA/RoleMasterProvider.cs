using SMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Data
{
    public class RoleMasterProvider:BaseProvider
    {
        public RoleMasterProvider()
        {

        }
        
        public webpages_Roles GetRolesById(int Id)
        {
            return _db.webpages_Roles.Find(Id);
        }
        public List<RoleModel> GetAllRoles()
        {
            var data = (from a in _db.webpages_Roles
                        select new RoleModel
                        {
                            Id = a.RoleId,
                            Name = a.RoleName,
                            RoleCode = a.RoleCode,
                            IsActive = a.IsActive
                        }).ToList();

            return data;
        }
        public webpages_Roles GetRolesByName(string roleName)
        {
            return _db.webpages_Roles.Where(x => x.RoleName == roleName).FirstOrDefault();
        }
        public webpages_Roles CreateRole(webpages_Roles role)
        {
            webpages_Roles _webpages_Roles = new webpages_Roles()
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName,
                RoleCode = role.RoleCode,
                IsActive = role.IsActive,
                
            };

            _db.webpages_Roles.Add(_webpages_Roles);
            _db.SaveChanges();

            return role;
        }

    }
}
