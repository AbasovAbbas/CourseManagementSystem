using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagement.Data.Migrations
{
    public partial class initilazionq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubject_Subjects_SubjectId1",
                table: "StudentSubject");

            migrationBuilder.DropIndex(
                name: "IX_StudentSubject_SubjectId1",
                table: "StudentSubject");

            migrationBuilder.DropColumn(
                name: "SubjectId1",
                table: "StudentSubject");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "SubjectId1",
                table: "StudentSubject",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubject_SubjectId1",
                table: "StudentSubject",
                column: "SubjectId1");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubject_Subjects_SubjectId1",
                table: "StudentSubject",
                column: "SubjectId1",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
