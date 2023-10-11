using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagement.DataAccess.Migrations
{
    public partial class StudentGroupUpda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubGroup",
                table: "StudentGroups");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "StudentGroups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "StudentGroups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "StudentGroups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "StudentGroups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "StudentGroups");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "StudentGroups");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "StudentGroups");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "StudentGroups");

            migrationBuilder.AddColumn<bool>(
                name: "IsSubGroup",
                table: "StudentGroups",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
