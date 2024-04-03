using SMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Data
{
    public class EmailTemplateProvider : BaseProvider
    {
        public EmailTemplateProvider()
        {

        }
        public EmailFP GetEmailTemplateById(int id)
        {
            return _db.emailtemplate.Find(id);
        }
        public int CreateEmailTemplate(EmailFP model)
        {
            try
            {
                _db.emailtemplate.Add(model);
                _db.SaveChanges();
                return model.Id;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<EmailTemplate> GetAllEmailTemplate()
        {
            var query = (from a in _db.emailtemplate
                         where a.IsActive == true
                         select new EmailTemplate
                         {
                             Id = a.Id,
                             Name = a.Name,
                             BCC = a.BCC,
                             CC = a.CC,
                             CreatedOn = a.CreatedOn,
                             UpdatedBy = a.UpdatedBy,
                             UpdatedOn = a.UpdatedOn,
                             CreatedBy = a.CreatedBy,
                             TemplateCode = a.TemplateCode,
                             MailBody = a.MailBody,
                             Subject = a.Subject,
                             IsDeleted = a.IsDeleted
                         }
                         ) ;
            return query.ToList();
        }
        public int UpdateEmailTemplates(EmailFP model)
        {
            try
            {
                _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return model.Id;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public EmailFP GetEmailTemplateByCode(string model)
        {
            return _db.emailtemplate.Where(a => a.TemplateCode == model).FirstOrDefault();
            
        }
        public EmailTemplateNew ReplaceParameterValuesInEmailTemplate(EmailFP template, Dictionary<string, string> subjectVariables = null, Dictionary<string, string> bodyVariables = null)
        {
            EmailTemplateNew model = new EmailTemplateNew();
            string subject = template.Subject;
            string mailBody = template.MailBody;
            if (subjectVariables != null)
            {
                foreach (var dictionary in subjectVariables)
                {
                    subject = subject.Replace(dictionary.Key, dictionary.Value);
                }
            }

            if (bodyVariables != null)
            {
                foreach (var dictionary in bodyVariables)
                {
                    mailBody = mailBody.Replace(dictionary.Key, dictionary.Value);
                }
            }

            model.Subject = subject;
            model.MailBody = mailBody;

            return model;
        }
        
    }
}
