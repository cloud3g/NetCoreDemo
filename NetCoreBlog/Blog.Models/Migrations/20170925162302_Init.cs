using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Blog.Models.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogAnnouncement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Content = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    Enable = table.Column<bool>(type: "bool", nullable: false),
                    Level = table.Column<int>(type: "int4", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogAnnouncement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false),
                    BodyContent = table.Column<string>(type: "varchar(8000)", maxLength: 8000, nullable: true),
                    CategoryType = table.Column<int>(type: "int4", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    Enable = table.Column<bool>(type: "bool", nullable: false),
                    ImgUrl = table.Column<string>(type: "text", nullable: true),
                    IsNav = table.Column<bool>(type: "bool", nullable: false),
                    LinkUrl = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    Meta_Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Meta_Keywords = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Pid = table.Column<int>(type: "int4", nullable: true),
                    Remark = table.Column<string>(type: "text", nullable: true),
                    Sort = table.Column<int>(type: "int4", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysUserInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreateTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    ErrorCount = table.Column<int>(type: "int4", nullable: false),
                    LastErrTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    LoginName = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Password = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    RealName = table.Column<string>(type: "text", nullable: true),
                    Remark = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "int4", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysUserInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogArticle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CategoryId = table.Column<int>(type: "int4", nullable: false),
                    CategoryName = table.Column<string>(type: "text", nullable: true),
                    CommentNum = table.Column<int>(type: "int4", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    Recommend = table.Column<bool>(type: "bool", nullable: true),
                    Remark = table.Column<string>(type: "text", nullable: true),
                    Stick = table.Column<bool>(type: "bool", nullable: true),
                    Submitter = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Traffic = table.Column<int>(type: "int4", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogArticle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogArticle_BlogCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "BlogCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogArticle_CategoryId",
                table: "BlogArticle",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogAnnouncement");

            migrationBuilder.DropTable(
                name: "BlogArticle");

            migrationBuilder.DropTable(
                name: "SysUserInfo");

            migrationBuilder.DropTable(
                name: "BlogCategory");
        }
    }
}
