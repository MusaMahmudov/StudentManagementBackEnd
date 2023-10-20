using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagement.DataAccess.Migrations
{
    public partial class studentTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamTypes_ExamTypes_ExamTypeId",
                table: "ExamTypes");

            migrationBuilder.DropIndex(
                name: "IX_ExamTypes_ExamTypeId",
                table: "ExamTypes");

            migrationBuilder.DropColumn(
                name: "ExamTypeId",
                table: "ExamTypes");

            migrationBuilder.AddCheckConstraint(
                name: "FullName",
                table: "Students",
                sql: "Len(FullName) >=3");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "FullName",
                table: "Students");

            migrationBuilder.AddColumn<Guid>(
                name: "ExamTypeId",
                table: "ExamTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamTypes_ExamTypeId",
                table: "ExamTypes",
                column: "ExamTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamTypes_ExamTypes_ExamTypeId",
                table: "ExamTypes",
                column: "ExamTypeId",
                principalTable: "ExamTypes",
                principalColumn: "Id");
        }
    }
}
