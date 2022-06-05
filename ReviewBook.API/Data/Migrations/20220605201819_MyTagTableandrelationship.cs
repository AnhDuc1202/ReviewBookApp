using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ReviewBook.API.Data.Migrations
{
    public partial class MyTagTableandrelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "myTags",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ID_Acc = table.Column<int>(type: "integer", nullable: false),
                    ID_Tag = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_myTags", x => x.ID);
                    table.ForeignKey(
                        name: "FK_myTags_Accounts_ID_Acc",
                        column: x => x.ID_Acc,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_myTags_Tags_ID_Tag",
                        column: x => x.ID_Tag,
                        principalTable: "Tags",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_myTags_ID_Acc",
                table: "myTags",
                column: "ID_Acc");

            migrationBuilder.CreateIndex(
                name: "IX_myTags_ID_Tag",
                table: "myTags",
                column: "ID_Tag");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "myTags");
        }
    }
}
