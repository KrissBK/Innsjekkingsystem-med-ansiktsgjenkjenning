using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaceRecogApp.Migrations
{
    public partial class FaceServiceIdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Employees_OrganizerEmployeeId",
                table: "Activities");

            migrationBuilder.AddColumn<Guid>(
                name: "faceServicePersonId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "OrganizerEmployeeId",
                table: "Activities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Employees_OrganizerEmployeeId",
                table: "Activities",
                column: "OrganizerEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Employees_OrganizerEmployeeId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "faceServicePersonId",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizerEmployeeId",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Employees_OrganizerEmployeeId",
                table: "Activities",
                column: "OrganizerEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
