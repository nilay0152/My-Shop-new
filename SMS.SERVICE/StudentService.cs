using SMS.Data;
using SMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Service
{
   public class StudentService
    {
        public readonly StudentProvider _studentProvider;
        public StudentService()
        {
            _studentProvider = new StudentProvider();
        }
        public StudentModel GetStudentById(string StudentId)
        {
            var data = _studentProvider.GetStudentById(StudentId);
           
            StudentModel students = new StudentModel()
            {
                StudentId = data.StudentId,
                Firstname = data.Firstname,
                Lastname =  data.Lastname,
                 Age = data.Age,
                Gender = data.Gender,
                Standard = data.Standard,
                Email = data.Email,
                ContactNumber = data.ContactNumber,
                Status = data.Status

            };
            return students;
        }
        public Guid CreateStudent(StudentModel student)
        {
            return _studentProvider.CreateStudent(student);
        }
        public StudentModel UpdateStudent(StudentModel students)
        {
           return _studentProvider.UpdateStudent(students);
        }
        public List<StudentModel> GetallStudent()
        {
            var studentlist = _studentProvider.GetallStudent();
            return studentlist;
        }
        public void DeleteStudent(string StudentId)
        {
           
            _studentProvider.DeleteStudent(StudentId);
        }
        public int TotalStudent()
        {
            return _studentProvider.TotalStudent();
        }
    }
}
