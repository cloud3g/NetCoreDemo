using NetCoreWebAPI.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Service.Interface
{
    public interface ICourseService
    {
        List<Course> GetAllCourse();
    }
}
