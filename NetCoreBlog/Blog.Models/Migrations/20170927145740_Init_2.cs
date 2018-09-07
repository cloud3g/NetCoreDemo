using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Blog.Models.Migrations
{
    public partial class Init_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Friendslinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreateTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Enable = table.Column<bool>(type: "bool", nullable: false),
                    Logo = table.Column<string>(type: "text", nullable: true),
                    Sort = table.Column<int>(type: "int4", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true),
                    WebName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendslinks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friendslinks");
        }
    }
}
