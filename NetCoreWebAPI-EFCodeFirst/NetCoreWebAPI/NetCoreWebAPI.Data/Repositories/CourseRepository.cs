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
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(IDbFactory _dbFactory) : base(_dbFactory)
        {
        }
    }
}
