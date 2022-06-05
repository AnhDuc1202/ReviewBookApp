using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReviewBook.API.Data.Migrations
{
    public partial class fixDbcontext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Propose_NewTag_Proposes_ID_Propose",
                table: "Propose_NewTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Propose_NewTag",
                table: "Propose_NewTag");

            migrationBuilder.RenameTable(
                name: "Propose_NewTag",
                newName: "propose_NewTags");

            migrationBuilder.RenameIndex(
                name: "IX_Propose_NewTag_ID_Propose",
                table: "propose_NewTags",
                newName: "IX_propose_NewTags_ID_Propose");

            migrationBuilder.AddPrimaryKey(
                name: "PK_propose_NewTags",
                table: "propose_NewTags",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_propose_NewTags_Proposes_ID_Propose",
                table: "propose_NewTags",
                column: "ID_Propose",
                principalTable: "Proposes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_propose_NewTags_Proposes_ID_Propose",
                table: "propose_NewTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_propose_NewTags",
                table: "propose_NewTags");

            migrationBuilder.RenameTable(
                name: "propose_NewTags",
                newName: "Propose_NewTag");

            migrationBuilder.RenameIndex(
                name: "IX_propose_NewTags_ID_Propose",
                table: "Propose_NewTag",
                newName: "IX_Propose_NewTag_ID_Propose");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Propose_NewTag",
                table: "Propose_NewTag",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Propose_NewTag_Proposes_ID_Propose",
                table: "Propose_NewTag",
                column: "ID_Propose",
                principalTable: "Proposes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
