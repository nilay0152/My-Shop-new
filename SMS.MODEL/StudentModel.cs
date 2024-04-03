using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model
{
   public class StudentModel
    {
        public Guid StudentId { get; set; }
        [Required(ErrorMessage = "FirstName is required.")]
        [Display(Name = "First Name")]
        [StringLength(20)]
        public string Firstname { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "LastName is required.")]
        [StringLength(20)]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Age is required.")]
        [Range(2, 60, ErrorMessage ="Age is must be between 2 to 60")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Standard is required.")]
        [Range(1, 12, ErrorMessage="Standard is must be between 1 to 12")]
        public int Standard { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Contact Number")]
        [Required(ErrorMessage = "Contact Number is required.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string ContactNumber { get; set; }
        public bool Status { get; set; } = true;

    }
}
