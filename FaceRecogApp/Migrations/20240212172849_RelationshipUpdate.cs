using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaceRecogApp.Migrations
{
    public partial class RelationshipUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Pictures_EmployeeId",
                table: "Pictures",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_JobTitleId",
                table: "Employees",
                column: "JobTitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_JobTitles_JobTitleId",
                table: "Employees",
                column: "JobTitleId",
                principalTable: "JobTitles",
                principalColumn: "JobTitleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Employees_EmployeeId",
                table: "Pictures",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_JobTitles_JobTitleId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Employees_EmployeeId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_EmployeeId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Employees_JobTitleId",
                table: "Employees");
        }
    }
}
