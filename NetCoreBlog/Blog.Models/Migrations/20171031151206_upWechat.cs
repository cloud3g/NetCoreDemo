using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Blog.Models.Migrations
{
    public partial class upWechat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WC_MessageResponse_OfficalAccountId",
                table: "WC_MessageResponse");

            migrationBuilder.CreateIndex(
                name: "IX_WC_MessageResponse_OfficalAccountId",
                table: "WC_MessageResponse",
                column: "OfficalAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WC_MessageResponse_OfficalAccountId",
                table: "WC_MessageResponse");

            migrationBuilder.CreateIndex(
                name: "IX_WC_MessageResponse_OfficalAccountId",
                table: "WC_MessageResponse",
                column: "OfficalAccountId",
                unique: true);
        }
    }
}
