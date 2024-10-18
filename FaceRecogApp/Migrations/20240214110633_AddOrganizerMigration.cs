using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaceRecogApp.Migrations
{
    public partial class AddOrganizerMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizerEmployeeId",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_OrganizerEmployeeId",
                table: "Activities",
                column: "OrganizerEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Employees_OrganizerEmployeeId",
                table: "Activities",
                column: "OrganizerEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Employees_OrganizerEmployeeId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_OrganizerEmployeeId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "OrganizerEmployeeId",
                table: "Activities");
        }
    }
}
