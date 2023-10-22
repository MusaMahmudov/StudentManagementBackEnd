using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagement.DataAccess.Migrations
{
    public partial class groupSubjectUpd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "Credits",
                table: "GroupSubjects",
                sql: "Credits BETWEEN 1 AND 30");

            migrationBuilder.AddCheckConstraint(
                name: "HoursOfSubject",
                table: "GroupSubjects",
                sql: "Hours BETWEEN 1 AND 200");

            migrationBuilder.AddCheckConstraint(
                name: "TotalWeeksDuration",
                table: "GroupSubjects",
                sql: "TotalWeeks BETWEEN 1 AND 50");

            migrationBuilder.AddCheckConstraint(
                name: "YearOfSubject",
                table: "GroupSubjects",
                sql: "Year BETWEEN 2010 AND 2023");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "Credits",
                table: "GroupSubjects");

            migrationBuilder.DropCheckConstraint(
                name: "HoursOfSubject",
                table: "GroupSubjects");

            migrationBuilder.DropCheckConstraint(
                name: "TotalWeeksDuration",
                table: "GroupSubjects");

            migrationBuilder.DropCheckConstraint(
                name: "YearOfSubject",
                table: "GroupSubjects");
        }
    }
}
