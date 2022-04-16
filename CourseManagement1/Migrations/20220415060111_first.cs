using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagement.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeTables_AspNetUsers_StudentId",
                table: "TimeTables");

            migrationBuilder.DropIndex(
                name: "IX_TimeTables_StudentId",
                table: "TimeTables");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "TimeTables");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "TimeTables",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "TimeTables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TimeTables_GroupId",
                table: "TimeTables",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTables_Group_GroupId",
                table: "TimeTables",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeTables_Group_GroupId",
                table: "TimeTables");

            migrationBuilder.DropIndex(
                name: "IX_TimeTables_GroupId",
                table: "TimeTables");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "TimeTables");

            migrationBuilder.AlterColumn<string>(
                name: "Time",
                table: "TimeTables",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "TimeTables",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeTables_StudentId",
                table: "TimeTables",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTables_AspNetUsers_StudentId",
                table: "TimeTables",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
