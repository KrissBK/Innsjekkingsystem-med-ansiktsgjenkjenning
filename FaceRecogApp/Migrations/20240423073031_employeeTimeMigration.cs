using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaceRecogApp.Migrations
{
    public partial class employeeTimeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "latestCheckIn",
                table: "Employees",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "latestCheckIn",
                table: "Employees");
        }
    }
}
