using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Model.Models
{
    [Table("Student")]
    public class Student
    {
        //public Student()
        //{
        //    this.StudentCourses = new HashSet<StudentCourse>();
        //}
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentID { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string StudentName { get; set; }
        [Required]
        public Boolean StudentGender { get; set; }
        [Required]
        public DateTime StudentBirthDay { get; set; }
        [Required]
        [MaxLength(12)]
        public string StudentPhone { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
