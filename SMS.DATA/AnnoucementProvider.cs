using SMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Data
{
    public class AnnoucementProvider:BaseProvider
    {
        public AnnoucementProvider()
        {

        }
        public List<AnnoucementModel> GetAllAnnoucement()
        {
            var sadmin = SessionHelper.UserId;
            var all = _db.webpages_UsersInRoles.Where(x=>x.UserId == sadmin).FirstOrDefault().RoleId;
            if (sadmin==1)
            {
                return _db.annoucements.Select(x => new AnnoucementModel
                {
                    Id = x.Id,
                    Subject = x.Subject,
                    AnnoucementDetail = x.AnnoucementDetail,
                    CreatedOn = x.CreatedOn,
                    RoleId=x.RoleId
                }).ToList();
            }
            else 
            {
                var annoucement = (from role in _db.annoucements
                                   join anmct in _db.webpages_UsersInRoles on role.RoleId equals anmct.RoleId 
                                   into list from announcement in list.DefaultIfEmpty()
                                   where role.RoleId == all || role.RoleId == 0
                                   select
                       new AnnoucementModel()
                       {
                           Id = role.Id,
                           CreatedOn = role.CreatedOn,
                           Subject = role.Subject,
                           AnnoucementDetail = role.AnnoucementDetail,
                           RoleId = role.RoleId
                       }).ToList();
                return annoucement;
            }
        }
        public Annoucement GetAnnoucementById(int Id)
        {
            return _db.annoucements.Find(Id);
        }
        public int  CreateAnnoucement(AnnoucementModel annoucementModel)
        {
            
            Annoucement _annocement = new Annoucement()
            {
                Id=annoucementModel.Id,
                Subject = annoucementModel.Subject,
                AnnoucementDetail= annoucementModel.AnnoucementDetail,
                RoleId=annoucementModel.RoleId,
                CreatedOn = DateTime.UtcNow
                
            };

            _db.annoucements.Add(_annocement);
            _db.SaveChanges();
            return annoucementModel.Id;
        }

        public AnnoucementModel UpdateAnnoucement(AnnoucementModel annoucementModel)
        {
            var objannocement = GetAnnoucementById(annoucementModel.Id);
            objannocement.Subject = annoucementModel.Subject;
            objannocement.AnnoucementDetail = annoucementModel.AnnoucementDetail;
            objannocement.RoleId = annoucementModel.RoleId;
            objannocement.CreatedOn = DateTime.UtcNow;
          _db.SaveChanges();
            return annoucementModel;
        }
        public void DeleteAnnoucement(int Id)
        {
            var data = GetAnnoucementById(Id);
            if (data != null)
            {
                _db.annoucements.Remove(data);
                _db.SaveChanges();
            }
        }
        public List<DropDownList> BindRole()
        {
            return _db.webpages_Roles.Where(s => s.IsActive == true && s.RoleCode != "SADMIN").Select(x => new DropDownList { Key = x.RoleName, Value = x.RoleId }).ToList();
        }
        public int TotalAnnouncement()
        {
            return _db.annoucements.Where(q => q.Status == true).Count();
        }

    }
}
