using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Model.Models
{
    [Table("Course")]
    public class Course
    {
        //public Course()
        //{
        //    this.StudentCourses = new HashSet<StudentCourse>();
        //}
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseID { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string CourseName { get; set; }
        [Required]
        public double CoursePrice { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
