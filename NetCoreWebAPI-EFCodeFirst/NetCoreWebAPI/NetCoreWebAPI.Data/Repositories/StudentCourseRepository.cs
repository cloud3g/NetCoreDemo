using Microsoft.EntityFrameworkCore;
using NetCoreWebAPI.Data.Infrastructure;
using NetCoreWebAPI.Data.Interface;
using NetCoreWebAPI.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Data.Repositories
{
    public class StudentCourseRepository : Repository<StudentCourse>, IStudentCourseRepository
    {
        public StudentCourseRepository(IDbFactory _dbFactory) : base(_dbFactory)
        {
        }

        public bool AddCourse(int studentID, int courseID, bool status)
        {
            Student checkStudent = DbContext.Students.SingleOrDefault(n => n.StudentID == studentID);
            Course checkCourse = DbContext.Courses.SingleOrDefault(n => n.CourseID == courseID);
            StudentCourse checkStudentCourse = DbContext.StudentCourses.SingleOrDefault(n => n.CourseID == courseID && n.StudentID == studentID);

            if (checkStudent == null)
            {
                return false;
            }
            else
            {
                if (checkCourse == null)
                {
                    return false;
                }
                else
                {
                    if (checkStudentCourse != null)
                    {
                        return false;
                    }
                }
            }

            DateTime now = DateTime.Now;
            StudentCourse studentCourse = new StudentCourse() { StudentID = studentID, CourseID = courseID, Description = now.ToString(), Status = status };
            try
            {
                DbContext.StudentCourses.Add(studentCourse);
                int bSave = DbContext.SaveChanges();
                if (bSave > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<StudentCourse>> GetCourseByStudentID(int studentID)
        {
            //return await DbContext.StudentCourses.Where(n => n.StudentID == studentID && n.Status == true).Include(n => n.Student).ToListAsync();
            return await DbContext.StudentCourses.Where(n => n.StudentID == studentID && n.Status == true).ToListAsync();
        }

        public bool UpdateCourse(int studentID, int courseID, bool status)
        {
            Student checkStudent = DbContext.Students.SingleOrDefault(n => n.StudentID == studentID);
            Course checkCourse = DbContext.Courses.SingleOrDefault(n => n.CourseID == courseID);
            StudentCourse studentCourse = DbContext.StudentCourses.FirstOrDefault(n => n.CourseID == courseID && n.StudentID == studentID);
            
            if (checkStudent == null)
            {
                return false;
            }
            else
            {
                if (checkCourse == null)
                {
                    return false;
                }
                else
                {
                    if (studentCourse == null)
                    {
                        return false;
                    }
                }
            }

            try
            {
                studentCourse.Status = status;
                DbContext.StudentCourses.Update(studentCourse);
                int checkUpdate = DbContext.SaveChanges();

                if (checkUpdate > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
