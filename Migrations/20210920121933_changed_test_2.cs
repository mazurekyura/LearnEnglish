using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnEnglish.Migrations
{
    public partial class changed_test_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberCorrectAnswers",
                table: "Tests");

            migrationBuilder.AddColumn<int>(
                name: "NumberCorrectAnswers",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberCorrectAnswers",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "NumberCorrectAnswers",
                table: "Tests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
