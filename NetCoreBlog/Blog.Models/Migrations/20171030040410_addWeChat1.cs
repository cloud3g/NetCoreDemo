using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Blog.Models.Migrations
{
    public partial class addWeChat1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WC_OfficalAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AccessToken = table.Column<string>(type: "text", nullable: true),
                    ApiUrl = table.Column<string>(type: "text", nullable: true),
                    AppId = table.Column<string>(type: "text", nullable: true),
                    AppSecret = table.Column<string>(type: "text", nullable: true),
                    Category = table.Column<int>(type: "int4", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    Enable = table.Column<bool>(type: "bool", nullable: false),
                    IsDefault = table.Column<bool>(type: "bool", nullable: false),
                    OfficalCode = table.Column<string>(type: "text", nullable: true),
                    OfficalId = table.Column<string>(type: "text", nullable: true),
                    OfficalKey = table.Column<string>(type: "text", nullable: true),
                    OfficalName = table.Column<string>(type: "text", nullable: true),
                    Remark = table.Column<string>(type: "text", nullable: true),
                    Token = table.Column<string>(type: "text", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WC_OfficalAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WC_MessageResponse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Category = table.Column<int>(type: "int4", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    Enable = table.Column<bool>(type: "bool", nullable: false),
                    ImgTextContext = table.Column<string>(type: "text", nullable: true),
                    ImgTextLink = table.Column<string>(type: "text", nullable: true),
                    ImgTextUrl = table.Column<string>(type: "text", nullable: true),
                    IsDefault = table.Column<bool>(type: "bool", nullable: false),
                    MatchKey = table.Column<string>(type: "text", nullable: true),
                    MeidaLink = table.Column<string>(type: "text", nullable: true),
                    MeidaUrl = table.Column<string>(type: "text", nullable: true),
                    MessageRule = table.Column<int>(type: "int4", nullable: false),
                    OfficalAccountId = table.Column<int>(type: "int4", nullable: false),
                    Remark = table.Column<string>(type: "text", nullable: true),
                    Sort = table.Column<int>(type: "int4", nullable: false),
                    TextContent = table.Column<string>(type: "text", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WC_MessageResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WC_MessageResponse_WC_OfficalAccounts_OfficalAccountId",
                        column: x => x.OfficalAccountId,
                        principalTable: "WC_OfficalAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WC_MessageResponse_OfficalAccountId",
                table: "WC_MessageResponse",
                column: "OfficalAccountId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WC_MessageResponse");

            migrationBuilder.DropTable(
                name: "WC_OfficalAccounts");
        }
    }
}
