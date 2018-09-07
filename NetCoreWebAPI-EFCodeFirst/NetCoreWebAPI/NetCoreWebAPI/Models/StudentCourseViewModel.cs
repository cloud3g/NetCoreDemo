using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Models
{
    public class StudentCourseViewModel
    {
        public int studentID { get; set; }
        public int courseID { get; set; }
        public bool status { get; set; }
    }
}
