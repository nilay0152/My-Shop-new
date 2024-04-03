using SMS.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Data
{
    public class FormRoleMappingProvider:BaseProvider
    {
        public List<FormRoleMapping> GetAllRoleRights()
        {
            var getallrolerights = (from rights in _db.FormRoleMappings where rights.IsActive == true select rights).ToList();
            return getallrolerights;
        }

        public FormRoleMapping GetRoleRightsById(int Id)
        {
            return _db.FormRoleMappings.Find(Id);
        }
        public int InsertRoleRights(FormRoleMapping rolerights)
        {
            rolerights.CreatedBy = 1;
            _db.FormRoleMappings.Add(rolerights);
            _db.SaveChanges();
            return rolerights.Id;
        }
        public List<FormRoleMapping> GetAllRoleRightsById(int RoleId)
        {
            var getformdata = (from f in _db.formModel
                               select new
                               {
                                   Id = f.Id,
                                   Name = ((f.ParentForm == null ? 0 : f.ParentForm) == 0 ? f.Name : ((from fc in _db.formModel where fc.Id == (f.ParentForm == null ? 0 : f.ParentForm) select fc).FirstOrDefault().Name) + " " + ">>" + " " + f.Name),
                               }).AsEnumerable().Select(x => new FormMst()
                               {
                                   Id = x.Id,
                                   Name = x.Name
                               }).ToList();

            List<FormRoleMapping> roleRights = new List<FormRoleMapping>();

            foreach (var formdata in getformdata)
            {
                FormRoleMapping roleRightsdata = new FormRoleMapping();

                var permission = GetAllRoleRights().Where(x => x.RoleId == RoleId && x.MenuId == formdata.Id).FirstOrDefault();

                if (permission != null)
                {
                    roleRightsdata.RoleId = permission.RoleId;
                    roleRightsdata.MenuId = formdata.Id;
                    roleRightsdata.FormName = formdata.Name;
                    roleRightsdata.AllowMenu = permission.AllowMenu;
                    roleRightsdata.FullRights = permission.FullRights;
                    roleRightsdata.AllowView = permission.AllowView;
                    roleRightsdata.AllowInsert = permission.AllowInsert;
                    roleRightsdata.AllowUpdate = permission.AllowUpdate;
                    roleRightsdata.AllowDelete = permission.AllowDelete;
                }
                else
                {
                    roleRightsdata.RoleId = RoleId;
                    roleRightsdata.MenuId = formdata.Id;
                    roleRightsdata.FormName = formdata.Name;
                    roleRightsdata.AllowMenu = false;
                    roleRightsdata.FullRights = false;
                    roleRightsdata.AllowView = false;
                    roleRightsdata.AllowInsert = false;
                    roleRightsdata.AllowUpdate = false;
                    roleRightsdata.AllowDelete = false;
                }
                roleRights.Add(roleRightsdata);
            }
            return roleRights;
        }
        public void UpdateRoleRights(FormRoleMapping rolerights)
        {
            var getrolerights = GetRoleRightsById(rolerights.Id);

            if (getrolerights != null)
            {
                getrolerights.MenuId = rolerights.MenuId;
                getrolerights.FullRights = rolerights.FullRights;
                getrolerights.AllowMenu = rolerights.AllowMenu;
                getrolerights.AllowView = rolerights.AllowView;
                getrolerights.AllowInsert = rolerights.AllowInsert;
                getrolerights.AllowUpdate = rolerights.AllowUpdate;
                getrolerights.AllowDelete = rolerights.AllowDelete;
                getrolerights.UpdatedBy = rolerights.UpdatedBy;
                getrolerights.UpdatedOn = rolerights.UpdatedOn;
                getrolerights.IsActive = rolerights.IsActive;
                _db.SaveChanges();
            }
        }
        public bool UpdateRoleRights(IEnumerable<FormRoleMapping> rolerights, int CreatedBy, int UpdatedBy)
        {

            FormRoleMapping frm = new FormRoleMapping();
            int roleID = Convert.ToInt16(rolerights.Select(p => p.RoleId).First());

            foreach (FormRoleMapping RoleRights in rolerights)
            {
                int MenuID = Convert.ToInt16(RoleRights.MenuId);
                string formCode = _db.formModel.Where(a => a.Id == RoleRights.MenuId).FirstOrDefault().FormAcessCode;
                frm = GetAllRoleRights().Where(x => x.RoleId == roleID && x.MenuId == MenuID).FirstOrDefault();
                if (frm == null)
                {
                    if (RoleRights.AllowView == true || RoleRights.AllowInsert == true || RoleRights.AllowUpdate == true || RoleRights.AllowDelete == true)
                    {
                        frm = new FormRoleMapping();
                        frm.RoleId = roleID;
                        frm.MenuId = RoleRights.MenuId;
                        frm.FullRights = RoleRights.FullRights;
                        frm.AllowMenu = RoleRights.AllowMenu;
                        frm.AllowView = RoleRights.AllowView;
                        frm.AllowInsert = RoleRights.AllowInsert;
                        frm.AllowUpdate = RoleRights.AllowUpdate;
                        frm.AllowDelete = RoleRights.AllowDelete;
                        frm.CreatedBy = CreatedBy;
                        frm.CreatedOn = DateTime.UtcNow;
                        frm.UpdatedBy = UpdatedBy;
                        frm.UpdatedOn = DateTime.UtcNow;
                        frm.IsActive = true;
                        InsertRoleRights(frm);

                    }
                }
                else
                {
                    frm.RoleId = roleID;
                    frm.MenuId = RoleRights.MenuId;
                    frm.FullRights = RoleRights.FullRights;
                    frm.AllowMenu = RoleRights.AllowMenu;
                    frm.AllowView = RoleRights.AllowView;
                    frm.AllowInsert = RoleRights.AllowInsert;
                    frm.AllowUpdate = RoleRights.AllowUpdate;
                    frm.AllowDelete = RoleRights.AllowDelete;
                    frm.UpdatedBy = UpdatedBy;
                    frm.UpdatedOn = DateTime.UtcNow;
                    frm.IsActive = true;
                    UpdateRoleRights(frm);
                }
            }
            return true;
        }

        public FormRoleMapping CheckFormAccess(string _formaccessCode)
        {
            int _roleID = (from user in _db.usersProfile
                           join roles in _db.webpages_UsersInRoles on user.Userid equals roles.UserId
                           where user.Userid == SessionHelper.UserId
                           select roles.RoleId).FirstOrDefault();

            SqlParameter param1 = new SqlParameter("@formaccessCode", _formaccessCode);
            SqlParameter param2 = new SqlParameter("@roleID", _roleID);

            var _roleRights = _db.Database.SqlQuery<FormRoleMapping>("CheckFormAccess_sp  @formaccessCode, @roleID", param1, param2).FirstOrDefault();
            if (_roleRights != null)
            {

                AcessPermission.AllowMenu = _roleRights.AllowMenu;
                AcessPermission.View = _roleRights.AllowView;
                AcessPermission.Add = _roleRights.AllowInsert;
                AcessPermission.Edit = _roleRights.AllowUpdate;
                AcessPermission.Delete = _roleRights.AllowDelete;
            }
            //else if ("sadmin" != null )
            //{
            //    AcessPermission.AllowMenu = _roleRights.AllowMenu;
            //    AcessPermission.View = _roleRights.AllowView;
            //    AcessPermission.Add = _roleRights.AllowInsert;
            //    AcessPermission.Edit = _roleRights.AllowUpdate;
            //    AcessPermission.Delete = _roleRights.AllowDelete;
            //}
            else
            {
                AcessPermission.AllowMenu = false;
                AcessPermission.View = false;
                AcessPermission.Add = false;
                AcessPermission.Edit = false;
                AcessPermission.Delete = false;

            }
            return _roleRights;
        }
        public List<MenuVW> GetMenu(int userID)
        {
            var _fullmenuVW = new List<MenuVW>();
            var _menuVW = new List<MenuVW>();

            SqlParameter param = new SqlParameter("@userID", userID);

            _fullmenuVW = _db.Database.SqlQuery<MenuVW>("getMenu_sp @userID", param).ToList();
            //_menuVW = MenuTree(_fullmenuVW, null);

            var mainMenu = (from _m in _fullmenuVW
                            where (_m.ParentForm == null || _m.ParentForm == 0)
                            select _m).ToList();


            foreach (var _menu in mainMenu)
            {
                var _submenu = (from _mm in _fullmenuVW
                                where _mm.ParentForm == _menu.Id
                                select _mm).ToList();

                if (_submenu.Count > 0)
                {
                    _menu.SubMenu = BindNLevelMenu(_submenu, _fullmenuVW);
                }
                _menuVW.Add(_menu);
            }

            return _menuVW;
        }
        public List<MenuVW> BindNLevelMenu(List<MenuVW> _subMenu, List<MenuVW> _fullMenu)
        {
            var _sMenu = new List<MenuVW>();
            foreach (var _tm in _subMenu)
            {
                var thirdmenu = (from _fm in _fullMenu
                                 where _fm.ParentForm == _tm.Id
                                 select _fm).ToList();
                if (thirdmenu.Count > 0)
                {
                    _tm.SubMenu = BindNLevelMenu(thirdmenu, _fullMenu);
                }
                _sMenu.Add(_tm);
            }
            return _sMenu;
        }
       
    }
}
