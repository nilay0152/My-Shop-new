using SMS.Data;
using SMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Service
{
    public class EmailTemplateService
    {
       private readonly EmailTemplateProvider  _emailTemplateProvider;

        public EmailTemplateService()
        {
            _emailTemplateProvider = new EmailTemplateProvider();
        }
        public EmailFP GetEmailTemplateById(int id)
        {
            return _emailTemplateProvider.GetEmailTemplateById(id);
        }
        public int CreateEmailTemplate(EmailFP model)
        {
            return _emailTemplateProvider.CreateEmailTemplate(model);
        }
        public List<EmailTemplate> GetAllEmailTemplate()
        {
            return _emailTemplateProvider.GetAllEmailTemplate();
        }
        public int UpdateEmailTemplates(EmailFP model)
        {
            return _emailTemplateProvider.UpdateEmailTemplates(model);
        }
        public EmailFP GetEmailTemplateByCode(string model)
        {
           var template =  _emailTemplateProvider.GetEmailTemplateByCode(model);
            return template;
        }
        public EmailTemplateNew ReplaceParameterValuesInEmailTemplate(EmailFP template, Dictionary<string, string> subjectVariables = null, Dictionary<string, string> bodyVariables = null)
        {
            return _emailTemplateProvider.ReplaceParameterValuesInEmailTemplate(template, subjectVariables, bodyVariables);
        }
    }
}
