using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class UpdateEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Emails");

            migrationBuilder.AddColumn<long>(
                name: "fileLength",
                table: "Emails",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fileLength",
                table: "Emails");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Emails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Emails",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
