using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Blog.Models.Migrations
{
    public partial class upsysuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_SysUserRole_Id",
                table: "SysUserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SysUserRole",
                table: "SysUserRole");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_RolePermission_Id",
                table: "RolePermission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermission",
                table: "RolePermission");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "SysUserRole",
                type: "int4",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "RolePermission",
                type: "int4",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SysUserRole",
                table: "SysUserRole",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermission",
                table: "RolePermission",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SysUserRole_RoleId",
                table: "SysUserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_RoleId",
                table: "RolePermission",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SysUserRole",
                table: "SysUserRole");

            migrationBuilder.DropIndex(
                name: "IX_SysUserRole_RoleId",
                table: "SysUserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermission",
                table: "RolePermission");

            migrationBuilder.DropIndex(
                name: "IX_RolePermission_RoleId",
                table: "RolePermission");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "SysUserRole",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int4")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "RolePermission",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int4")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_SysUserRole_Id",
                table: "SysUserRole",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SysUserRole",
                table: "SysUserRole",
                columns: new[] { "RoleId", "UserId" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_RolePermission_Id",
                table: "RolePermission",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermission",
                table: "RolePermission",
                columns: new[] { "RoleId", "ModuleId" });
        }
    }
}
