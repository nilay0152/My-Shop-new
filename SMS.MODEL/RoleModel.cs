using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model
{
    public class RoleModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Role Name")]
        public string Name { get; set; }

        [DisplayName("Active")]
        public bool IsActive { get; set; }

        public string _RoleCode { get; set; }


        [Required]
        [Display(Name = "Role Code")]

        public string RoleCode
        {
            get
            {
                if (string.IsNullOrEmpty(_RoleCode))
                {
                    return _RoleCode;
                }
                return _RoleCode.ToUpper();
            }
            set
            {
                _RoleCode = value;
            }
        }
    }
}
