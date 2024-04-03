using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SMS.Model
{
     public class EmailTemplate
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "TemplateCode is required")]
        [Remote("CheckDuplicateTemplateCode", "EmailTemplate", HttpMethod = "Post", AdditionalFields = "Id")]
        public string TemplateCode { get; set; }
        [Required]
        public string Name { get; set; }
        public string Subject { get; set; }
        //[Required]
        public string MailBody { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public bool IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
