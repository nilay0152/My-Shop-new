using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model
{
    public class Teacher
    {
        public int Id { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "required")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "required")]
        public string LastName { get; set; }
        [Display(Name = "Email")]
        [EmailAddress]
        [Required(ErrorMessage = "required")]
        public string Email { get; set; }
        [Display(Name = "Contact Number")]
        [Required(ErrorMessage = "required")]
        public string MobileNumber { get; set; }

        [Display(Name = "Active")]
        [Required(ErrorMessage = "required")]
        public bool IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool Status { get; set; } = true;

    }
}
