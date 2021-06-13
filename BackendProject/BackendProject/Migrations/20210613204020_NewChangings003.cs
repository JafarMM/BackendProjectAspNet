using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendProject.Migrations
{
    public partial class NewChangings003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SkillPercentCommunication",
                table: "TeacherDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SkillPercentDesign",
                table: "TeacherDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SkillPercentDevolopment",
                table: "TeacherDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SkillPercentInnovation",
                table: "TeacherDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SkillPercentTeamLeader",
                table: "TeacherDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SkillPercentCommunication",
                table: "TeacherDetails");

            migrationBuilder.DropColumn(
                name: "SkillPercentDesign",
                table: "TeacherDetails");

            migrationBuilder.DropColumn(
                name: "SkillPercentDevolopment",
                table: "TeacherDetails");

            migrationBuilder.DropColumn(
                name: "SkillPercentInnovation",
                table: "TeacherDetails");

            migrationBuilder.DropColumn(
                name: "SkillPercentTeamLeader",
                table: "TeacherDetails");
        }
    }
}
