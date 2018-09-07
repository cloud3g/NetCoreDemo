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
    public class CourseController : Controller
    {
        ICourseService courseService;
        public CourseController(ICourseService _courseService)
        {
            courseService = _courseService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("get-all")]
        public List<Course> GetAllCourse()
        {
            return courseService.GetAllCourse();
        }
    }
}