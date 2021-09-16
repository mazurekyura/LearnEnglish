using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnEnglish.Migrations
{
    public partial class add_test_minor_change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnswerFasleSecond",
                table: "Tests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnswerFasleThird",
                table: "Tests",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerFasleSecond",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "AnswerFasleThird",
                table: "Tests");
        }
    }
}
