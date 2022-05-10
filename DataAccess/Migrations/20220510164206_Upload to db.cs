using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Uploadtodb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Emails");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Emails",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "DataFiles",
                table: "Emails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "Emails",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Emails",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "DataFiles",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Emails");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Emails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
