using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReviewBook.API.Data.Migrations
{
    public partial class columDescriptinInProposeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Proposes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "Proposes");
        }
    }
}
