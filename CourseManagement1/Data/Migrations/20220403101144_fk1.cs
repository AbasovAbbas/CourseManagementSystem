using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagement.Data.Migrations
{
    public partial class fk1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubject_AspNetUsers_StudentId1",
                table: "StudentSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubject_AspNetUsers_TeacherId1",
                table: "TeacherSubject");

            migrationBuilder.DropIndex(
                name: "IX_TeacherSubject_TeacherId1",
                table: "TeacherSubject");

            migrationBuilder.DropIndex(
                name: "IX_StudentSubject_StudentId1",
                table: "StudentSubject");

            migrationBuilder.DropColumn(
                name: "TeacherId1",
                table: "TeacherSubject");

            migrationBuilder.DropColumn(
                name: "StudentId1",
                table: "StudentSubject");

            migrationBuilder.AlterColumn<string>(
                name: "TeacherId",
                table: "TeacherSubject",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "StudentSubject",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubject_TeacherId",
                table: "TeacherSubject",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubject_StudentId",
                table: "StudentSubject",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubject_AspNetUsers_StudentId",
                table: "StudentSubject",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubject_AspNetUsers_TeacherId",
                table: "TeacherSubject",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubject_AspNetUsers_StudentId",
                table: "StudentSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubject_AspNetUsers_TeacherId",
                table: "TeacherSubject");

            migrationBuilder.DropIndex(
                name: "IX_TeacherSubject_TeacherId",
                table: "TeacherSubject");

            migrationBuilder.DropIndex(
                name: "IX_StudentSubject_StudentId",
                table: "StudentSubject");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "TeacherSubject",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeacherId1",
                table: "TeacherSubject",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "StudentSubject",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentId1",
                table: "StudentSubject",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubject_TeacherId1",
                table: "TeacherSubject",
                column: "TeacherId1");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubject_StudentId1",
                table: "StudentSubject",
                column: "StudentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubject_AspNetUsers_StudentId1",
                table: "StudentSubject",
                column: "StudentId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubject_AspNetUsers_TeacherId1",
                table: "TeacherSubject",
                column: "TeacherId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
