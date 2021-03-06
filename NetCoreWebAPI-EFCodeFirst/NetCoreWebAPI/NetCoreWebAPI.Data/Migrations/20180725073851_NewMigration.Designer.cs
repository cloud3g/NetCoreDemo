﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetCoreWebAPI.Data;

namespace NetCoreWebAPI.Data.Migrations
{
    [DbContext(typeof(NetCoreWebAPIDbContext))]
    [Migration("20180725073851_NewMigration")]
    partial class NewMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NetCoreWebAPI.Model.Models.Course", b =>
                {
                    b.Property<int>("CourseID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("CoursePrice");

                    b.HasKey("CourseID");

                    b.ToTable("Course");

                    b.HasData(
                        new { CourseID = 1, CourseName = "Khóa học Javascript chuyên sâu", CoursePrice = 1000000.0 },
                        new { CourseID = 2, CourseName = "Khóa học React Native chuyên sâu", CoursePrice = 1500000.0 },
                        new { CourseID = 3, CourseName = "Khóa học ASP.NET Core chuyên sâu", CoursePrice = 2000000.0 },
                        new { CourseID = 4, CourseName = "Khóa học AngularJS chuyên sâu", CoursePrice = 1300000.0 },
                        new { CourseID = 5, CourseName = "Khóa học ReactJS chuyên sâu", CoursePrice = 1400000.0 }
                    );
                });

            modelBuilder.Entity("NetCoreWebAPI.Model.Models.Student", b =>
                {
                    b.Property<int>("StudentID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("StudentBirthDay");

                    b.Property<bool>("StudentGender");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("StudentPhone")
                        .IsRequired()
                        .HasMaxLength(12);

                    b.HasKey("StudentID");

                    b.ToTable("Student");

                    b.HasData(
                        new { StudentID = 1, StudentBirthDay = new DateTime(1996, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), StudentGender = true, StudentName = "Nguyễn Văn A", StudentPhone = "01672983683" },
                        new { StudentID = 2, StudentBirthDay = new DateTime(1996, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), StudentGender = true, StudentName = "Nguyễn Văn B", StudentPhone = "01672983683" },
                        new { StudentID = 3, StudentBirthDay = new DateTime(1996, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), StudentGender = true, StudentName = "Nguyễn Văn C", StudentPhone = "01672983683" },
                        new { StudentID = 4, StudentBirthDay = new DateTime(1996, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), StudentGender = true, StudentName = "Nguyễn Văn D", StudentPhone = "01672983683" },
                        new { StudentID = 5, StudentBirthDay = new DateTime(1996, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), StudentGender = true, StudentName = "Nguyễn Văn E", StudentPhone = "01672983683" },
                        new { StudentID = 6, StudentBirthDay = new DateTime(1996, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), StudentGender = true, StudentName = "Nguyễn Văn F", StudentPhone = "01672983683" },
                        new { StudentID = 7, StudentBirthDay = new DateTime(1996, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), StudentGender = true, StudentName = "Nguyễn Văn G", StudentPhone = "01672983683" },
                        new { StudentID = 8, StudentBirthDay = new DateTime(1996, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), StudentGender = true, StudentName = "Nguyễn Văn H", StudentPhone = "01672983683" },
                        new { StudentID = 9, StudentBirthDay = new DateTime(1996, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), StudentGender = true, StudentName = "Nguyễn Văn I", StudentPhone = "01672983683" },
                        new { StudentID = 10, StudentBirthDay = new DateTime(1996, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), StudentGender = true, StudentName = "Nguyễn Văn J", StudentPhone = "01672983683" }
                    );
                });

            modelBuilder.Entity("NetCoreWebAPI.Model.Models.StudentCourse", b =>
                {
                    b.Property<int>("CourseID");

                    b.Property<int>("StudentID");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Status");

                    b.HasKey("CourseID", "StudentID");

                    b.HasIndex("StudentID");

                    b.ToTable("StudentCourse");
                });

            modelBuilder.Entity("NetCoreWebAPI.Model.Models.StudentCourse", b =>
                {
                    b.HasOne("NetCoreWebAPI.Model.Models.Course", "Course")
                        .WithMany("StudentCourses")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NetCoreWebAPI.Model.Models.Student", "Student")
                        .WithMany("StudentCourses")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
