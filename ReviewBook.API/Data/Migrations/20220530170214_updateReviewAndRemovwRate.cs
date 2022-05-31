using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ReviewBook.API.Data.Migrations
{
    public partial class updateReviewAndRemovwRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rateBooks");

            migrationBuilder.AddColumn<int>(
                name: "Rate",
                table: "Reviews",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Reviews");

            migrationBuilder.CreateTable(
                name: "rateBooks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ID_Acc = table.Column<int>(type: "integer", nullable: false),
                    ID_Book = table.Column<int>(type: "integer", nullable: false),
                    Rate = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rateBooks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_rateBooks_Accounts_ID_Acc",
                        column: x => x.ID_Acc,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rateBooks_Books_ID_Book",
                        column: x => x.ID_Book,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_rateBooks_ID_Acc",
                table: "rateBooks",
                column: "ID_Acc");

            migrationBuilder.CreateIndex(
                name: "IX_rateBooks_ID_Book",
                table: "rateBooks",
                column: "ID_Book");
        }
    }
}
