using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagement.Migrations
{
    public partial class studentsubj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnrollmentDate",
                table: "StudentSubject");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "StudentSubject");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EnrollmentDate",
                table: "StudentSubject",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "StudentSubject",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
