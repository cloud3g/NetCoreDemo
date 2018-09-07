using NetCoreWebAPI.Data.Interface;
using NetCoreWebAPI.Data.Repositories;
using NetCoreWebAPI.Model.Models;
using NetCoreWebAPI.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Service.Service
{
    public class StudentService : IStudentService
    {
        private IStudentRepository studentRepository;
        public StudentService(IStudentRepository _studentRepository)
        {
            studentRepository = _studentRepository;
        }
        public bool DeleteStudentByID(int studentID)
        {
            return studentRepository.DeleteByID(studentID);
        }

        public List<Student> GetAllStudent()
        {
            return studentRepository.GetALL(n => n.StudentID > 0);
        }

        public Student GetStudentByID(int id)
        {
            return studentRepository.GetSingleByID(id);
        }
    }
}
