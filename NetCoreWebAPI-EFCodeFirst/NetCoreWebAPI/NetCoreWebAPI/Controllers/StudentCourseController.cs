using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebAPI.Model.Models;
using NetCoreWebAPI.Models;
using NetCoreWebAPI.Service.Interface;

namespace NetCoreWebAPI.Controllers
{
    [Route("api/student-course")]
    [ApiController]
    public class StudentCourseController : Controller
    {
        IStudentCourseService studentCourseService;
        public StudentCourseController(IStudentCourseService _studentCourseService)
        {
            studentCourseService = _studentCourseService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("get-by-student-id")]
        public async Task<List<StudentCourse>> GetCourseByStudentID(int id)
        {
            return await studentCourseService.GetCourseByStudentID(id);
        }

        [HttpPut("cancel-course")]
        public string CancelCourse(StudentCourseViewModel studentCourseViewModel)
        {
            if (studentCourseService.CancelCourse(studentCourseViewModel.studentID, studentCourseViewModel.courseID, studentCourseViewModel.status))
            {
                return "Đã hủy khóa học có StudentID = " + studentCourseViewModel.studentID + " và courseID = " + studentCourseViewModel.courseID;
            }
            else
            {
                return "Hủy khóa học thất bại";
            }
        }

        [HttpPost("register-course")]
        public String RegisterCourse(StudentCourseViewModel studentCourseViewModel)
        {
            if (studentCourseService.RegisterCourse(studentCourseViewModel.studentID, studentCourseViewModel.courseID, studentCourseViewModel.status))
            {
                return "Đăng ký khóa học thành công với StudentID = " + studentCourseViewModel.studentID + " và courseID = " + studentCourseViewModel.courseID;
            }
            else
            {
                return "Đăng ký khóa học thất bại";
            }
        }
    }
}