using SMS.Data;
using SMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Service
{
    public class UserMasterService
    {
        private readonly UserMasterProvider userMasterProvider;
        public UserMasterService()
        {
            userMasterProvider = new UserMasterProvider();
        }
        public List<User> GetAllUser()
        {
            return userMasterProvider.GetAllUser();
        }

        public User GetUserById(int id)
        {
            return userMasterProvider.GetUserById(id);
        }

        public User UpdateUsersRole(User pur)
        {
            return userMasterProvider.UpdateUsersRole(pur);
        }

        public List<DropDownList> BindRole()
        {
            return userMasterProvider.BindRole();
        }

        public void DeleteUser(int id)
        {
            userMasterProvider.DeleteUser(id);
        }
        public User CreateUser(User user)
        {
            return userMasterProvider.CreateUser(user);
        }
        public webpages_Membership GetWebpages_MembershipByUserId(int userid)
        {
            return userMasterProvider.GetWebpages_MembershipByUserId(userid);
        }
        public User GetEmailById(string EmailId)
        {
            return userMasterProvider.GetEmailById(EmailId);
        }
    }
}
