using SMS.Model;
using SMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Controllers
{
    public class FormRoleMappingController : BaseController
    {
        private readonly FormRoleMappingService formRoleMappingService;
        private readonly RoleMasterService roleMasterService;
        public FormRoleMappingController()
        {
            formRoleMappingService = new FormRoleMappingService();
            roleMasterService = new RoleMasterService();
        }
        // GET: FormRoleMapping
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewPermission(int Id = 0)
        {
            FormRoleMapping model = new FormRoleMapping();
            if (Id > 0)
            {
                model.RoleId = Id;
                model.RoleName = roleMasterService.GetRolesById(Id).RoleName;
            }
            List<FormRoleMapping> Formrolemapping = FormRoleMapping_Read(model.RoleId);
            return View(Formrolemapping);
        }
        public JsonResult UpdatePermission(IEnumerable<FormRoleMapping> rolerights)
        {
            int CreatedBy = SessionHelper.UserId;
            int UpdatedBy = SessionHelper.UserId;
            var result = formRoleMappingService.UpdateRoleRights(rolerights, CreatedBy, UpdatedBy);
            if (result)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public List<FormRoleMapping> FormRoleMapping_Read(int RoleID)
        {
            var getrolerights = formRoleMappingService.GetAllRoleRightsById(RoleID).ToList();
            return getrolerights;

        }
    }
}