using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Blog.Models.Migrations
{
    public partial class addIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SysModule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AreaName = table.Column<string>(type: "text", nullable: true),
                    ControllerName = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    IsSysMenu = table.Column<bool>(type: "bool", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    PName = table.Column<string>(type: "text", nullable: true),
                    Pid = table.Column<int>(type: "int4", nullable: false),
                    Type = table.Column<int>(type: "int4", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysModule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreateTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Remark = table.Column<string>(type: "text", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreateTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    ErrorCount = table.Column<int>(type: "int4", nullable: false),
                    IP = table.Column<string>(type: "text", nullable: true),
                    Icon = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_SysUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysModuleAction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Code = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    Enable = table.Column<bool>(type: "bool", nullable: false),
                    ModuleId = table.Column<int>(type: "int4", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysModuleAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysModuleAction_SysModule_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "SysModule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int4", nullable: false),
                    ModuleId = table.Column<int>(type: "int4", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    Id = table.Column<int>(type: "int4", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => new { x.RoleId, x.ModuleId });
                    table.UniqueConstraint("AK_RolePermission_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermission_SysModule_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "SysModule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_SysRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "SysRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysUserRole",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int4", nullable: false),
                    UserId = table.Column<int>(type: "int4", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    Enable = table.Column<bool>(type: "bool", nullable: false),
                    Id = table.Column<int>(type: "int4", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp", nullable: true)
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
                        name: "FK_SysUserRole_SysUser_UserId",
                        column: x => x.UserId,
                        principalTable: "SysUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_ModuleId",
                table: "RolePermission",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_SysModuleAction_ModuleId",
                table: "SysModuleAction",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_SysUserRole_UserId",
                table: "SysUserRole",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "SysModuleAction");

            migrationBuilder.DropTable(
                name: "SysUserRole");

            migrationBuilder.DropTable(
                name: "SysModule");

            migrationBuilder.DropTable(
                name: "SysRole");

            migrationBuilder.DropTable(
                name: "SysUser");
        }
    }
}
