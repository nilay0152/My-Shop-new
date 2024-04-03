namespace SMS.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public  class User
    {
        [Key]
        public int Userid { get; set; }

        [Required]
        [StringLength(56)]

        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "User Name")]
        [Required]
        public string UserName { get; set; }
       
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [NotMapped]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "the password and confirm password do not match.")]
        public string ConfirmPassword { get; set; }

        [NotMapped]
        [Display(Name = "Role Name")]
        public int RoleId { get; set; }
        public string mobileNumber { get; set; }
        public string gender { get; set; }
        public DateTime DOB { get; set; }
        public string profileImage { get; set; }
        public bool Status { get; set; } = true;

    }
}
