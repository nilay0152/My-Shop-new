using SMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Data
{
    public class TeacherProvider:BaseProvider
    {
        public TeacherProvider()
        {

        }
     
        public List<TeacherModel> GetAllTeacher()
        {
            var squery = (from teacher in _db.teachers
                          where teacher.Status == true
                          select new TeacherModel
                          {  
                              Id = teacher.Id,
                              FirstName = teacher.FirstName,
                              LastName = teacher.LastName,
                              Email = teacher.Email,
                              MobileNumber = teacher.MobileNumber,
                              IsActive = teacher.IsActive
                          });
            return squery.ToList();
        }
        public Teacher GetTeacherById(int id)
        {
            return _db.teachers.Find(id);
        }


        public int CreateTeacher(TeacherModel teachers)
        {
           
            var CreatedBy = (from Teacher in _db.teachers
                             where teachers.Email == SessionHelper.EmailId
                             select teachers.Id).FirstOrDefault();
            Teacher obj = new Teacher()
            {
                Id = teachers.Id,
                FirstName = teachers.FirstName,
                LastName = teachers.LastName,
                Email = teachers.Email,
                MobileNumber = teachers.MobileNumber,
                IsActive = teachers.IsActive,
                CreatedOn = DateTime.UtcNow
            };
            _db.teachers.Add(obj);
            _db.SaveChanges();
            return teachers.Id;
        }
        public TeacherModel UpdateTeacher(TeacherModel teacherModel)
        {
            var objtea = GetTeacherById(teacherModel.Id);
            objtea.FirstName = teacherModel.FirstName;
            objtea.LastName = teacherModel.LastName;
            objtea.Email = teacherModel.Email;
            objtea.MobileNumber = teacherModel.MobileNumber;
            objtea.IsActive = teacherModel.IsActive;
            objtea.CreatedOn = teacherModel.CreatedOn;

            _db.SaveChanges();
            return teacherModel;
        }
        public void DeleteTeacher(int Id)
        {
            var data = GetTeacherById(Id);
            if (data != null)
            {
                _db.teachers.Remove(data);
                _db.SaveChanges();
            }
        }
        public int TotalTeacher()
        {
            return _db.teachers.Where(a => a.Status == true).Count();

        }
    }
}
