using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Blog.Models.Migrations
{
    public partial class Init_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SysRoleModule");

            migrationBuilder.DropTable(
                name: "SysUserRole");

            migrationBuilder.DropTable(
                name: "SysModule");

            migrationBuilder.DropTable(
                name: "SysRole");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SysModule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ActionName = table.Column<string>(nullable: true),
                    AreaName = table.Column<string>(nullable: true),
                    Code = table.Column<int>(nullable: false),
                    ControllerName = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Pid = table.Column<int>(nullable: false),
                    SubmitterId = table.Column<int>(nullable: false),
                    SubmitterName = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysModule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    SubmitterId = table.Column<int>(nullable: false),
                    SubmitterName = table.Column<string>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysRoleModule",
                columns: table => new
                {
                    SysModuleId = table.Column<int>(nullable: false),
                    SysRoleId = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    Id = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRoleModule", x => new { x.SysModuleId, x.SysRoleId });
                    table.UniqueConstraint("AK_SysRoleModule_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysRoleModule_SysModule_SysModuleId",
                        column: x => x.SysModuleId,
                        principalTable: "SysModule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SysRoleModule_SysRole_SysRoleId",
                        column: x => x.SysRoleId,
                        principalTable: "SysRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysUserRole",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    Id = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysUserRole", x => new { x.RoleId, x.UserId });
                    table.UniqueConstraint("AK_SysUserRole_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysUserRole_SysRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "SysRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SysUserRole_SysUserInfo_UserId",
                        column: x => x.UserId,
                        principalTable: "SysUserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SysRoleModule_SysRoleId",
                table: "SysRoleModule",
                column: "SysRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SysUserRole_UserId",
                table: "SysUserRole",
                column: "UserId");
        }
    }
}
