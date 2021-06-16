using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendProject.Migrations
{
    public partial class Newchanging00012 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SocialMedia_Teachers_TeacherId",
                table: "SocialMedia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SocialMedia",
                table: "SocialMedia");

            migrationBuilder.RenameTable(
                name: "SocialMedia",
                newName: "SocialMedias");

            migrationBuilder.RenameIndex(
                name: "IX_SocialMedia_TeacherId",
                table: "SocialMedias",
                newName: "IX_SocialMedias_TeacherId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TeacherDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SocialMedias",
                table: "SocialMedias",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SocialMedias_Teachers_TeacherId",
                table: "SocialMedias",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SocialMedias_Teachers_TeacherId",
                table: "SocialMedias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SocialMedias",
                table: "SocialMedias");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TeacherDetails");

            migrationBuilder.RenameTable(
                name: "SocialMedias",
                newName: "SocialMedia");

            migrationBuilder.RenameIndex(
                name: "IX_SocialMedias_TeacherId",
                table: "SocialMedia",
                newName: "IX_SocialMedia_TeacherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SocialMedia",
                table: "SocialMedia",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SocialMedia_Teachers_TeacherId",
                table: "SocialMedia",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
