using SMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Data
{
   
    public class UserMasterProvider:BaseProvider
    {
        public List<User> GetAllUser()
        {
            var User = _db.usersProfile.Where(X => X.Userid !=1).ToList();
            return User;
        }

        public User GetUserById(int id)
        {
            
            var currentUser = _db.usersProfile.Find(id);

            var roleid = (from rolemapping in _db.webpages_UsersInRoles
                          join role in _db.webpages_Roles on rolemapping.RoleId equals role.RoleId
                            where rolemapping.UserId == currentUser.Userid
                            orderby rolemapping.UserId descending
                            select role.RoleId).FirstOrDefault();
            var user = new User()
            {
                Userid=currentUser.Userid,
                UserName = currentUser.UserName,
                Email = currentUser.Email,
                RoleId = roleid
            };
            return user;
        }
        public User UpdateUsersRole(User pur)
        {                
                var v = _db.webpages_UsersInRoles.Where(a => a.UserId == pur.Userid).FirstOrDefault();
                if (v != null)
                {
                    webpages_UsersInRoles userRoleMapping = _db.webpages_UsersInRoles.FirstOrDefault(x => x.Id == v.Id);
                    userRoleMapping.RoleId = pur.RoleId;
                    userRoleMapping.UserId = pur.Userid;
                    _db.SaveChanges();
                }
                else
                {
                webpages_UsersInRoles _userRoleMapping = new webpages_UsersInRoles();
                    _userRoleMapping.RoleId = pur.RoleId;
                    _userRoleMapping.UserId = pur.Userid;
                    _db.webpages_UsersInRoles.Add(_userRoleMapping);
                    _db.SaveChanges();
                }
            
            

            return pur;
        }
        public List<DropDownList> BindRole()
        {
            return _db.webpages_Roles.Where(s => s.IsActive == true && s.RoleCode != "SADMIN").Select(x => new DropDownList { Key = x.RoleName, Value = x.RoleId }).ToList();
        }

        public void DeleteUser(int id)
        {
            var data = GetUserById(id);
            if (data != null)
            {
                _db.usersProfile.Remove(data);
                _db.SaveChanges();
            }
           
        }
        public User CreateUser(User user)
        {

            User obj = new User()
            {               
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                ConfirmPassword = user.ConfirmPassword
            };
            _db.usersProfile.Add(obj);
            _db.SaveChanges();
            return user;
        }
        public webpages_Membership GetWebpages_MembershipByUserId(int userid)
        {
            return _db.webpages_Memberships.Where(a => a.UserId == userid).FirstOrDefault();
        }

        public User GetEmailById(string EmailId)
        {
            return _db.usersProfile.Where(e => e.Email == EmailId).FirstOrDefault();
        }
    }
}
