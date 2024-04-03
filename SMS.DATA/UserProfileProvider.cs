
using SMS.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SMS.Data
{
    public class UserProfileProvider:BaseProvider
    {
        public UserProfileProvider()
        {

        }
        public User GetUserProfileById(int id)
        {
            return _db.usersProfile.Find(id);
        }
        public User UpdateUserProfile(User userProfileModel)
        {
            var userid = SessionHelper.UserId;
            var v = _db.usersProfile.Where(a => a.Userid == userid).FirstOrDefault();
            if (v != null)
            {
                string startupPath = System.IO.Directory.GetCurrentDirectory();

                string startupPath1 = Environment.CurrentDirectory;

                User obj = _db.usersProfile.FirstOrDefault(x => x.Userid == v.Userid);
                obj.UserName = userProfileModel.UserName;
                obj.Email = userProfileModel.Email;
                obj.mobileNumber = userProfileModel.mobileNumber;
                obj.gender = userProfileModel.gender;
                obj.DOB = userProfileModel.DOB;
                //obj.profileImage = userProfileModel.profileImage;              
                _db.SaveChanges();
            }
            
            
            return userProfileModel;
        }
    }
}
