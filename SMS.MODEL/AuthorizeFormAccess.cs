using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model
{
    public class AuthorizeFormAccess
    {
        public enum FormAccessCode
        {
            Home=1,
            Student=2,
            Teacher=3,
            Annoucement = 4,
            Rolemaster=6,
            Formmaster=7,
            Usermaster=8,
            EMAILTEMPLATE = 9,
            ROLES =10

        }
    }
}
