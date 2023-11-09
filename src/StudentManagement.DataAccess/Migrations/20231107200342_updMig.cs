using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagement.DataAccess.Migrations
{
    public partial class updMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "AddressTeacher",
                table: "Teachers",
                sql: "Len(Address) >=3");

            migrationBuilder.AddCheckConstraint(
                name: "EmailTeacher",
                table: "Teachers",
                sql: "Len(Email) >=5");

            migrationBuilder.AddCheckConstraint(
                name: "MobileNumberTeacher",
                table: "Teachers",
                sql: "Len(MobileNumber) >=3");

            migrationBuilder.AddCheckConstraint(
                name: "EducationDegreeStudent",
                table: "Students",
                sql: "Len(EducationDegree) >=3");

            migrationBuilder.AddCheckConstraint(
                name: "EmailStudent",
                table: "Students",
                sql: "Len(Email) >=3");

            migrationBuilder.AddCheckConstraint(
                name: "FormOfEducationStudent",
                table: "Students",
                sql: "Len(FormOfEducation) >=3");

            migrationBuilder.AddCheckConstraint(
                name: "HomePhoneNumberStudent",
                table: "Students",
                sql: "Len(HomePhoneNumber) >=3");

            migrationBuilder.AddCheckConstraint(
                name: "PhoneNumberStudent",
                table: "Students",
                sql: "Len(PhoneNumber) >=3");

            migrationBuilder.AddCheckConstraint(
                name: "TypeOfPaymentStudent",
                table: "Students",
                sql: "Len(TypeOfPayment) >=3");

            migrationBuilder.AddCheckConstraint(
                name: "Semester",
                table: "GroupSubjects",
                sql: "Len(Semester) >= 3");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "AddressTeacher",
                table: "Teachers");

            migrationBuilder.DropCheckConstraint(
                name: "EmailTeacher",
                table: "Teachers");

            migrationBuilder.DropCheckConstraint(
                name: "MobileNumberTeacher",
                table: "Teachers");

            migrationBuilder.DropCheckConstraint(
                name: "EducationDegreeStudent",
                table: "Students");

            migrationBuilder.DropCheckConstraint(
                name: "EmailStudent",
                table: "Students");

            migrationBuilder.DropCheckConstraint(
                name: "FormOfEducationStudent",
                table: "Students");

            migrationBuilder.DropCheckConstraint(
                name: "HomePhoneNumberStudent",
                table: "Students");

            migrationBuilder.DropCheckConstraint(
                name: "PhoneNumberStudent",
                table: "Students");

            migrationBuilder.DropCheckConstraint(
                name: "TypeOfPaymentStudent",
                table: "Students");

            migrationBuilder.DropCheckConstraint(
                name: "Semester",
                table: "GroupSubjects");
        }
    }
}
