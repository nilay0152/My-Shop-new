using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model
{
     public class EmailFP
    {
        public int Id { get; set; }
        public string TemplateCode { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string MailBody { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

    }
    public class EmailTemplateNew
    {
        public string Subject { get; set; }
        public string MailBody { get; set; }
    }
}
