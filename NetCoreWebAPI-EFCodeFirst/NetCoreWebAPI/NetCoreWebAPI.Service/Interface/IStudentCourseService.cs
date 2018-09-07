using NetCoreWebAPI.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Service.Interface
{
    public interface IStudentCourseService
    {
        bool RegisterCourse(int studentID, int courseID, bool status);
        bool CancelCourse(int studentID, int courseID, bool status);
        Task<List<StudentCourse>> GetCourseByStudentID(int studentID);
    }
}
