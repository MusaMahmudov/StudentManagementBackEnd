using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagement.DataAccess.Migrations
{
    public partial class updatesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "FullName",
                table: "Students");

            migrationBuilder.AddCheckConstraint(
                name: "FullNameTeacher",
                table: "Teachers",
                sql: "Len(FullName) >=3");

            migrationBuilder.AddCheckConstraint(
                name: "NameTeacherRole",
                table: "TeacherRoles",
                sql: "Len(Name) >=3");

            migrationBuilder.AddCheckConstraint(
                name: "NameSubject",
                table: "Subjects",
                sql: "Len(Name) >=3");

            migrationBuilder.AddCheckConstraint(
                name: "FullNameStudent",
                table: "Students",
                sql: "Len(FullName) >=3");

            migrationBuilder.AddCheckConstraint(
                name: "NameLessonType",
                table: "LessonType",
                sql: "Len(Name) >=3");

            migrationBuilder.AddCheckConstraint(
                name: "NameGroup",
                table: "Groups",
                sql: "Len(Name) >=3");

            migrationBuilder.AddCheckConstraint(
                name: "NameFaculty",
                table: "Faculties",
                sql: "Len(Name) >=3");

            migrationBuilder.AddCheckConstraint(
                name: "NameExamType",
                table: "ExamTypes",
                sql: "Len(Name) >=3");

            migrationBuilder.AddCheckConstraint(
                name: "Score",
                table: "ExamsResults",
                sql: "Score BETWEEN 1 AND 100");

            migrationBuilder.AddCheckConstraint(
                name: "NameExam",
                table: "Exams",
                sql: "Len(Name) >=3");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "FullNameTeacher",
                table: "Teachers");

            migrationBuilder.DropCheckConstraint(
                name: "NameTeacherRole",
                table: "TeacherRoles");

            migrationBuilder.DropCheckConstraint(
                name: "NameSubject",
                table: "Subjects");

            migrationBuilder.DropCheckConstraint(
                name: "FullNameStudent",
                table: "Students");

            migrationBuilder.DropCheckConstraint(
                name: "NameLessonType",
                table: "LessonType");

            migrationBuilder.DropCheckConstraint(
                name: "NameGroup",
                table: "Groups");

            migrationBuilder.DropCheckConstraint(
                name: "NameFaculty",
                table: "Faculties");

            migrationBuilder.DropCheckConstraint(
                name: "NameExamType",
                table: "ExamTypes");

            migrationBuilder.DropCheckConstraint(
                name: "Score",
                table: "ExamsResults");

            migrationBuilder.DropCheckConstraint(
                name: "NameExam",
                table: "Exams");

            migrationBuilder.AddCheckConstraint(
                name: "FullName",
                table: "Students",
                sql: "Len(FullName) >=3");
        }
    }
}
