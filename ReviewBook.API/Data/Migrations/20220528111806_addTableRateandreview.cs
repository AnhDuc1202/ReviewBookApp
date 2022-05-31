using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ReviewBook.API.Data.Migrations
{
    public partial class addTableRateandreview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Reviews");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Books",
                type: "text",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateTable(
                name: "reviewChildrens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ID_Acc = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Id_parent = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reviewChildrens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_reviewChildrens_Accounts_ID_Acc",
                        column: x => x.ID_Acc,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reviewChildrens_Reviews_Id_parent",
                        column: x => x.Id_parent,
                        principalTable: "Reviews",
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

            migrationBuilder.CreateIndex(
                name: "IX_reviewChildrens_ID_Acc",
                table: "reviewChildrens",
                column: "ID_Acc");

            migrationBuilder.CreateIndex(
                name: "IX_reviewChildrens_Id_parent",
                table: "reviewChildrens",
                column: "Id_parent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rateBooks");

            migrationBuilder.DropTable(
                name: "reviewChildrens");

            migrationBuilder.DropColumn(
                name: "description",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "Rate",
                table: "Reviews",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
