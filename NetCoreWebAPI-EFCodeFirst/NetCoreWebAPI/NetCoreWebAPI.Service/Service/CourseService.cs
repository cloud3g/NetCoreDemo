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
    public class CourseService : ICourseService
    {
        private ICourseRepository courseRepository;
        public CourseService(ICourseRepository _courseRepository)
        {
            courseRepository = _courseRepository;
        }

        public List<Course> GetAllCourse()
        {
            return courseRepository.GetALL(n => n.CourseID > 0);
        }
    }
}
