using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnEnglish.Migrations
{
    public partial class addBook_minor_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Users_OwnerId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Books",
                newName: "CreaterId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_OwnerId",
                table: "Books",
                newName: "IX_Books_CreaterId");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Users_CreaterId",
                table: "Books",
                column: "CreaterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Users_CreaterId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "CreaterId",
                table: "Books",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_CreaterId",
                table: "Books",
                newName: "IX_Books_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Users_OwnerId",
                table: "Books",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
