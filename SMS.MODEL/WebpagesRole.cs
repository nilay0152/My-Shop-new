using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model
{
    public class WebpagesRole
    {
        [Key]

        public int RoleId { get; set; }
        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        [Display(Name = "Active")]
        [Required]
        public bool IsActive { get; set; }
        [Display(Name = "Role Code")]
        [Required]
        public string RoleCode { get; set; }
        
    }
}
