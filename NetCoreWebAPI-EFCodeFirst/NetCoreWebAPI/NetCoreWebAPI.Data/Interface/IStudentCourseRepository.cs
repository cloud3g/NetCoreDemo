using NetCoreWebAPI.Data.Infrastructure;
using NetCoreWebAPI.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Data.Interface
{
    public interface IStudentCourseRepository : IRepository<StudentCourse>
    {
        /// <summary>
        /// Register New Course
        /// </summary>
        /// <param name="studentID">ID Of Student</param>
        /// <param name="courseID">ID Of Course</param>
        /// <returns>true if register successfull, false if register fail</returns>
        bool AddCourse(int studentID, int courseID, bool status);
        /// <summary>
        /// Update Information Course
        /// </summary>
        /// <param name="studentID">ID Of Student</param>
        /// <param name="courseID">ID Of Course</param>
        /// <returns>true if update successfull, false if update fail</returns>
        bool UpdateCourse(int studentID, int courseID, bool status);
        /// <summary>
        /// Get Infomation Of Course With StudentID
        /// </summary>
        /// <param name="studentID">ID Of Student</param>
        /// <returns>List StudentCourse</returns>
        Task<List<StudentCourse>> GetCourseByStudentID(int studentID);
    }
}
