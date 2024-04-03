using SMS.Data;
using SMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Service
{
    
    public class UserProfileService
    {
        public readonly UserProfileProvider userProfileProvider;
        public UserProfileService()
        {
            userProfileProvider = new UserProfileProvider();
        }
        public User UpdateUserProfile(User userProfileModel)
        {
            return userProfileProvider.UpdateUserProfile(userProfileModel);
        }
        public User GetUserProfileById()
        {
            var userid = SessionHelper.UserId;
            var data = userProfileProvider.GetUserProfileById(userid);
            
            var username = SessionHelper.UserName;
            User userProfileModel = new User()
            {
                
                Userid = userid,
                UserName = data.UserName,
                Email = data.Email,
                mobileNumber = data.mobileNumber,
                gender=data.gender,
                DOB=data.DOB,
                //profileImage= data.profileImage ,
                Status = data.Status
            };
            return userProfileModel;
           
        }

    }
    
}
