using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model
{
    public class Signups
    {
        [Key]
        public int Userid { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }       
        public string Password { get; set; }
        
        [NotMapped]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [NotMapped]
        [Display(Name = "Role Name")]
        public int RoleId { get; set; }
    }
}
