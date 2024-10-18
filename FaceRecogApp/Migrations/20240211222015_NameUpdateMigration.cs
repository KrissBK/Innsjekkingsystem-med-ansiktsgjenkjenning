using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaceRecogApp.Migrations
{
    public partial class NameUpdateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Pictures",
                newName: "EmployeeId");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Activities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Attendance = table.Column<bool>(type: "bit", nullable: false),
                    JobTitleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Pictures",
                newName: "PersonId");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Attendance = table.Column<bool>(type: "bit", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobTitleId = table.Column<int>(type: "int", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonId);
                });
        }
    }
}
