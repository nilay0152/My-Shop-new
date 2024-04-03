using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model
{
   public class UsersContext : DbContext
    {
        public UsersContext()
            : base("name=StudentEntites")
        {
           
        }
        public DbSet<UserProfileData> UserProfile { get; set; }
    }
    [Table("User")]

    public class UserProfileData
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Userid { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
    }

    public class ForgotPassword
    {
        [Required(ErrorMessage = "Current Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string OldPassword { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} Characters Long .", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "The New Password And Confirm Password Do Not Match.")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Display(Name = "User Name")]
        [Required]
        public string UserName { get; set; }
        [Display(Name = "Email Id")]
        
        public string EmailId { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

    }
    public class RegisterModel
    {
        
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} Characters Long .", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "confirm password")]
        [Compare("password", ErrorMessage =  "the password and confirm password do not match.")]
        public string confirmpassword { get; set; }

    }
    public class ForgotPasswordViewModel
    { 
        [Required]
        [EmailAddress]
        [Display(Name = "Email Id")]
        public string EmailId { get; set; }
    }
    public class ChangePassword_VM
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string  NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "confirm password")]
        [Compare("NewPassword", ErrorMessage = "the password and confirm password do not match.")]
        public string ConfirmPassword { get; set; }
        public string Forgotuid { get; set; }
        public string UserName { get; set; }
        public string ReturnToken { get; set; }

    }
}
