using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ReviewBook.API.Data.Migrations
{
    public partial class addAttibuteAndProposeNewwTagTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NewAut",
                table: "Proposes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NewPub",
                table: "Proposes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Propose_NewTag",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nameNewTag = table.Column<string>(type: "text", nullable: false),
                    ID_Propose = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propose_NewTag", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Propose_NewTag_Proposes_ID_Propose",
                        column: x => x.ID_Propose,
                        principalTable: "Proposes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Propose_NewTag_ID_Propose",
                table: "Propose_NewTag",
                column: "ID_Propose");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Propose_NewTag");

            migrationBuilder.DropColumn(
                name: "NewAut",
                table: "Proposes");

            migrationBuilder.DropColumn(
                name: "NewPub",
                table: "Proposes");
        }
    }
}
