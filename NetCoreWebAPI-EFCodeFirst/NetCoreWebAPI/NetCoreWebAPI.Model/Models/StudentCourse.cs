using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Model.Models
{
    [Table("StudentCourse")]
    public class StudentCourse
    {
        [Key]
        [Column(Order = 1)]
        public int CourseID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int StudentID { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Description { get; set; }
        public Boolean Status { get; set; }
        [ForeignKey("StudentID")]
        public virtual Student Student { get; set; }
        [ForeignKey("CourseID")]
        public virtual Course Course { get; set; }
    }
}
