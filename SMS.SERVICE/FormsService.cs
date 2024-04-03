using SMS.Data;
using SMS.Model;

using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Service
{
    public class FormsService
    {
        public readonly FormProvider _formProvider;

        public FormsService()
        {
            _formProvider = new FormProvider();
        }
        public List<FormModel> GetAllForms()
        {
            var forms = _formProvider.GetAllForms();
            return forms;
        }
        public FormModel SaveUpdateForm(FormModel model)
        {
            return _formProvider.SaveUpdateForm(model);
        }
        public FormModel GetFormsById(int Id)
        {
            var data = _formProvider.GetFormsById(Id);
            FormModel form = new FormModel()
            {

                Id = data.Id,
                Name = data.Name,
                NavigateURL = data.NavigateURL,
                FormAcessCode = data.FormAcessCode,
                DisplayOrder = data.DisplayOrder,
                IsActive = data.IsActive,
                IsDisplayMenu = data.IsDisplayMenu,
                ParentForm = data.ParentForm,
                CreatedOn = DateTime.Now
            };
            return form;
        }
        public FormModel GetFormsByCode(string formcode)
        {
            return _formProvider.GetFormsByCode(formcode);
        }
           }
}
