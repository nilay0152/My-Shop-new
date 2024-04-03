using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model
{
    public class TeacherModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="FirstName is required")]
        [Display(Name = "First Name")]
        [StringLength(20)]

        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "LastName is required")]
        [StringLength(20)]
        public string LastName { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Contact Number  is required")]
        [MinLength(10,ErrorMessage ="mobile no should be 11")]
        [Display(Name = "Contact Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string MobileNumber { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool Status { get; set; } = true;
    }
}
