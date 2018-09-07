using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebAPI.Data.Repositories;
using NetCoreWebAPI.Model.Models;
using NetCoreWebAPI.Models;
using NetCoreWebAPI.Service.Interface;

namespace NetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        IStudentService studentService;
        public StudentController(IStudentService _studentService)
        {
            studentService = _studentService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("get-by-id")]
        public Student GetStudentByID(int id)
        {
            return studentService.GetStudentByID(id);
        }

        [HttpGet("get-all")]
        public List<Student> GetAllStudent()
        {
            return studentService.GetAllStudent();
        }

        [HttpDelete("delete-by-id")]
        public String DeleteStudentByID(int id)
        {
            if(studentService.DeleteStudentByID(id))
            {
                return "Xóa thành công Student có studentID = " + id + " và các Course mà Student đã đăng ký";
            }
            else
            {
                return "Xóa thất bại";
            }
        }
    }
}