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
    public class StudentCourseService : IStudentCourseService
    {
        private IStudentCourseRepository studentCourseRepository;
        public StudentCourseService(IStudentCourseRepository _studentCourseRepository)
        {
            studentCourseRepository = _studentCourseRepository;
        }
        public bool CancelCourse(int studentID, int courseID, bool status)
        {
            return studentCourseRepository.UpdateCourse(studentID, courseID, status);
        }

        public async Task<List<StudentCourse>> GetCourseByStudentID(int studentID)
        {
            return await studentCourseRepository.GetCourseByStudentID(studentID);
        }

        public bool RegisterCourse(int studentID, int courseID, bool status)
        {
            return studentCourseRepository.AddCourse(studentID, courseID, status);
        }
    }
}
