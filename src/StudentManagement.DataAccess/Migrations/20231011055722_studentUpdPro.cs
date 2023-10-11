using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagement.DataAccess.Migrations
{
    public partial class studentUpdPro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectHours_Subjects_SubjectId",
                table: "SubjectHours");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "SubjectHours",
                newName: "GroupSubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectHours_SubjectId",
                table: "SubjectHours",
                newName: "IX_SubjectHours_GroupSubjectId");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_GroupId",
                table: "Students",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Groups_GroupId",
                table: "Students",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectHours_GroupSubjects_GroupSubjectId",
                table: "SubjectHours",
                column: "GroupSubjectId",
                principalTable: "GroupSubjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Groups_GroupId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectHours_GroupSubjects_GroupSubjectId",
                table: "SubjectHours");

            migrationBuilder.DropIndex(
                name: "IX_Students_GroupId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "GroupSubjectId",
                table: "SubjectHours",
                newName: "SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectHours_GroupSubjectId",
                table: "SubjectHours",
                newName: "IX_SubjectHours_SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectHours_Subjects_SubjectId",
                table: "SubjectHours",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
