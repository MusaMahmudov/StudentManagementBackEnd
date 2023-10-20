using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagement.DataAccess.Migrations
{
    public partial class GroupChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Semester",
                table: "GroupSubjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "GroupSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddCheckConstraint(
                name: "YearOfGraduation",
                table: "Students",
                sql: "YearOfGraduation Between 1900 and 2023");

            migrationBuilder.AddCheckConstraint(
                name: "MaxScore",
                table: "Exams",
                sql: "MaxScore Between 0 AND 100");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "YearOfGraduation",
                table: "Students");

            migrationBuilder.DropCheckConstraint(
                name: "MaxScore",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "Semester",
                table: "GroupSubjects");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "GroupSubjects");
        }
    }
}
