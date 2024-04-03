using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model
{
    public class UserProfileModel
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public string Email { get; set; }
        public string mobileNumber { get; set; }
        public string gender { get; set; }
        public DateTime DOB { get; set; }
        public string profileImage { get; set; }
    }
}
