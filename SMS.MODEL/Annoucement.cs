using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SMS.Model
{
    public class Annoucement
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "AnnoucementDetail is required")]
        [AllowHtml]
        public string AnnoucementDetail { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool Status { get; set; } = true;
        public int RoleId { get; set; }
    }
}
