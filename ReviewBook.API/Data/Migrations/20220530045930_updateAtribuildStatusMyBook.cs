using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReviewBook.API.Data.Migrations
{
    public partial class updateAtribuildStatusMyBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "myBooks");

            migrationBuilder.AddColumn<int>(
                name: "StatusBook",
                table: "myBooks",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusBook",
                table: "myBooks");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "myBooks",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
