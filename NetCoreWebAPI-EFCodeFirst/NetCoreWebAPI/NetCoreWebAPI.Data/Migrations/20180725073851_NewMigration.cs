using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreWebAPI.Data.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CoursePrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.CourseID);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    StudentGender = table.Column<bool>(nullable: false),
                    StudentBirthDay = table.Column<DateTime>(nullable: false),
                    StudentPhone = table.Column<string>(maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentID);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourse",
                columns: table => new
                {
                    CourseID = table.Column<int>(nullable: false),
                    StudentID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourse", x => new { x.CourseID, x.StudentID });
                    table.ForeignKey(
                        name: "FK_StudentCourse_Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourse_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Course",
                columns: new[] { "CourseID", "CourseName", "CoursePrice" },
                values: new object[,]
                {
                    { 1, "Khóa học Javascript chuyên sâu", 1000000.0 },
                    { 2, "Khóa học React Native chuyên sâu", 1500000.0 },
                    { 3, "Khóa học ASP.NET Core chuyên sâu", 2000000.0 },
                    { 4, "Khóa học AngularJS chuyên sâu", 1300000.0 },
                    { 5, "Khóa học ReactJS chuyên sâu", 1400000.0 }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "StudentID", "StudentBirthDay", "StudentGender", "StudentName", "StudentPhone" },
                values: new object[,]
                {
                    { 1, new DateTime(1996, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Nguyễn Văn A", "01672983683" },
                    { 2, new DateTime(1996, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Nguyễn Văn B", "01672983683" },
                    { 3, new DateTime(1996, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Nguyễn Văn C", "01672983683" },
                    { 4, new DateTime(1996, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Nguyễn Văn D", "01672983683" },
                    { 5, new DateTime(1996, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Nguyễn Văn E", "01672983683" },
                    { 6, new DateTime(1996, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Nguyễn Văn F", "01672983683" },
                    { 7, new DateTime(1996, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Nguyễn Văn G", "01672983683" },
                    { 8, new DateTime(1996, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Nguyễn Văn H", "01672983683" },
                    { 9, new DateTime(1996, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Nguyễn Văn I", "01672983683" },
                    { 10, new DateTime(1996, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Nguyễn Văn J", "01672983683" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourse_StudentID",
                table: "StudentCourse",
                column: "StudentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentCourse");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Student");
        }
    }
}
