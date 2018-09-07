using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewAPI.Migrations
{
    public partial class updateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "grupoid",
                table: "Pessoas",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Grupos",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    nome = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupos", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_grupoid",
                table: "Pessoas",
                column: "grupoid");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoas_Grupos_grupoid",
                table: "Pessoas",
                column: "grupoid",
                principalTable: "Grupos",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_Grupos_grupoid",
                table: "Pessoas");

            migrationBuilder.DropTable(
                name: "Grupos");

            migrationBuilder.DropIndex(
                name: "IX_Pessoas_grupoid",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "grupoid",
                table: "Pessoas");
        }
    }
}
