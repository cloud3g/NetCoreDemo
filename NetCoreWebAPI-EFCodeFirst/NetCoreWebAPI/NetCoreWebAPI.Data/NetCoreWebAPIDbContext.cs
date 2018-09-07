using Microsoft.EntityFrameworkCore;
using NetCoreWebAPI.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Data
{
    public class NetCoreWebAPIDbContext : DbContext
    {
        //public NetCoreWebAPIDbContext(DbContextOptions<NetCoreWebAPIDbContext> options) : base(options)
        //{

        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=BIN-PC\\BIN;Database=NetCoreWebAPI;Trusted_Connection=True;");
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(t => new { t.CourseID, t.StudentID });
                entity.HasOne(n => n.Student);
                entity.HasOne(n => n.Course);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                //entity.HasMany(n => n.StudentCourses);

                entity.HasData(
                    new Student { StudentID = 1, StudentName = "Nguyễn Văn A", StudentGender = true, StudentBirthDay = Convert.ToDateTime("1996/11/29"), StudentPhone = "01672983683" },
                    new Student { StudentID = 2, StudentName = "Nguyễn Văn B", StudentGender = true, StudentBirthDay = Convert.ToDateTime("1996/11/29"), StudentPhone = "01672983683" },
                    new Student { StudentID = 3, StudentName = "Nguyễn Văn C", StudentGender = true, StudentBirthDay = Convert.ToDateTime("1996/11/29"), StudentPhone = "01672983683" },
                    new Student { StudentID = 4, StudentName = "Nguyễn Văn D", StudentGender = true, StudentBirthDay = Convert.ToDateTime("1996/11/29"), StudentPhone = "01672983683" },
                    new Student { StudentID = 5, StudentName = "Nguyễn Văn E", StudentGender = true, StudentBirthDay = Convert.ToDateTime("1996/11/29"), StudentPhone = "01672983683" },
                    new Student { StudentID = 6, StudentName = "Nguyễn Văn F", StudentGender = true, StudentBirthDay = Convert.ToDateTime("1996/11/29"), StudentPhone = "01672983683" },
                    new Student { StudentID = 7, StudentName = "Nguyễn Văn G", StudentGender = true, StudentBirthDay = Convert.ToDateTime("1996/11/29"), StudentPhone = "01672983683" },
                    new Student { StudentID = 8, StudentName = "Nguyễn Văn H", StudentGender = true, StudentBirthDay = Convert.ToDateTime("1996/11/29"), StudentPhone = "01672983683" },
                    new Student { StudentID = 9, StudentName = "Nguyễn Văn I", StudentGender = true, StudentBirthDay = Convert.ToDateTime("1996/11/29"), StudentPhone = "01672983683" },
                    new Student { StudentID = 10, StudentName = "Nguyễn Văn J", StudentGender = true, StudentBirthDay = Convert.ToDateTime("1996/11/29"), StudentPhone = "01672983683" });
            });

            modelBuilder.Entity<Course>(entity =>
            {
                //entity.HasMany(n => n.StudentCourses);

                entity.HasData(
                    new Course { CourseID = 1, CourseName = "Khóa học Javascript chuyên sâu", CoursePrice = 1000000 },
                    new Course { CourseID = 2, CourseName = "Khóa học React Native chuyên sâu", CoursePrice = 1500000 },
                    new Course { CourseID = 3, CourseName = "Khóa học ASP.NET Core chuyên sâu", CoursePrice = 2000000 },
                    new Course { CourseID = 4, CourseName = "Khóa học AngularJS chuyên sâu", CoursePrice = 1300000 },
                    new Course { CourseID = 5, CourseName = "Khóa học ReactJS chuyên sâu", CoursePrice = 1400000 }
                );
            });

        }
    }
}
