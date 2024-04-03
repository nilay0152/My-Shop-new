using SMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Data
{
    public class StudentProvider : BaseProvider
    {
        public StudentProvider()
        {

        }
        public Student GetStudentById(string StudentId)
        {
            return _db.students.Where(x=>x.StudentId.ToString() == StudentId).FirstOrDefault();
        }
        public Guid CreateStudent(StudentModel students)
        {
            var createdby = (from Student in _db.students
                             where students.Email == SessionHelper.EmailId
                             select students.StudentId).FirstOrDefault();

            Student obj = new Student()
            {
                StudentId = Guid.NewGuid(),
                Firstname = students.Firstname,
                Lastname = students.Lastname,
                Age = students.Age,
                Gender = students.Gender,
                Standard = students.Standard,
                Email = students.Email,
                ContactNumber = students.ContactNumber

            };
            _db.students.Add(obj);
            _db.SaveChanges();
            return students.StudentId;
        }
        public StudentModel UpdateStudent(StudentModel students)
        {

            StudentModel student = new StudentModel();
            var obj1 = GetStudentById(students.StudentId.ToString());
            obj1.Firstname = students.Firstname;
            obj1.Lastname = students.Lastname;
            obj1.Age = students.Age;
            obj1.Gender = students.Gender;
            obj1.Standard = students.Standard;
            obj1.Email = students.Email;
            obj1.ContactNumber = students.ContactNumber;
            _db.SaveChanges();
            return students;
        }
        public List<StudentModel> GetallStudent()
        {
           
            var squery = (from student in _db.students
                          where student.Status == true
                          select new StudentModel
                          {

                              StudentId = student.StudentId, 
                              Firstname = student.Firstname,
                              Lastname = student.Lastname,
                              Age = student.Age,
                              Gender = student.Gender,
                              Standard = student.Standard,
                              Email = student.Email,
                              ContactNumber = student.ContactNumber

                          }) ;
            return squery.ToList();
        }

        public void DeleteStudent(string StudentId)
        {
            var data = GetStudentById(StudentId);
            if (data != null)
            {
                _db.students.Remove(data);
                _db.SaveChanges();
            }
            
        }
        public int TotalStudent()
        {
            return _db.students.Where(x => x.Status == true).Count();
        }
    }
}

