using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ReviewBook.API.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Stage_Name = table.Column<string>(type: "text", nullable: true),
                    Birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Address = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Telephone = table.Column<int>(type: "integer", nullable: true),
                    Email = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Website = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameRole = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ID_Aut = table.Column<int>(type: "integer", nullable: false),
                    ID_Pub = table.Column<int>(type: "integer", nullable: false),
                    PublishedYear = table.Column<int>(type: "integer", nullable: false),
                    Picture = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_ID_Aut",
                        column: x => x.ID_Aut,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Publishers_ID_Pub",
                        column: x => x.ID_Pub,
                        principalTable: "Publishers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Password = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    FullName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Address = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Picture = table.Column<byte[]>(type: "bytea", nullable: true),
                    ID_Role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Accounts_Roles_ID_Role",
                        column: x => x.ID_Role,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookTags",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ID_Book = table.Column<int>(type: "integer", nullable: false),
                    ID_Tag = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTags", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookTags_Books_ID_Book",
                        column: x => x.ID_Book,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookTags_Tags_ID_Tag",
                        column: x => x.ID_Tag,
                        principalTable: "Tags",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Follows",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ID_Following = table.Column<int>(type: "integer", nullable: false),
                    ID_Follower = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Follows", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Follows_Accounts_ID_Follower",
                        column: x => x.ID_Follower,
                        principalTable: "Accounts",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Follows_Accounts_ID_Following",
                        column: x => x.ID_Following,
                        principalTable: "Accounts",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "myBooks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ID_Acc = table.Column<int>(type: "integer", nullable: false),
                    ID_Book = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_myBooks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_myBooks_Accounts_ID_Acc",
                        column: x => x.ID_Acc,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_myBooks_Books_ID_Book",
                        column: x => x.ID_Book,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proposes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BookName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ID_Aut = table.Column<int>(type: "integer", nullable: true),
                    ID_Pub = table.Column<int>(type: "integer", nullable: true),
                    ID_Acc_Request = table.Column<int>(type: "integer", nullable: false),
                    PublishedYear = table.Column<int>(type: "integer", nullable: false),
                    Picture = table.Column<byte[]>(type: "bytea", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Proposes_Accounts_ID_Acc_Request",
                        column: x => x.ID_Acc_Request,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Proposes_Authors_ID_Aut",
                        column: x => x.ID_Aut,
                        principalTable: "Authors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Proposes_Publishers_ID_Pub",
                        column: x => x.ID_Pub,
                        principalTable: "Publishers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ID_Acc = table.Column<int>(type: "integer", nullable: false),
                    ID_Book = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Rate = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Accounts_ID_Acc",
                        column: x => x.ID_Acc,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Books_ID_Book",
                        column: x => x.ID_Book,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProposeTags",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ID_Propose = table.Column<int>(type: "integer", nullable: false),
                    ID_Tag = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProposeTags", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProposeTags_Proposes_ID_Propose",
                        column: x => x.ID_Propose,
                        principalTable: "Proposes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProposeTags_Tags_ID_Tag",
                        column: x => x.ID_Tag,
                        principalTable: "Tags",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ID_Role",
                table: "Accounts",
                column: "ID_Role");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ID_Aut",
                table: "Books",
                column: "ID_Aut");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ID_Pub",
                table: "Books",
                column: "ID_Pub");

            migrationBuilder.CreateIndex(
                name: "IX_BookTags_ID_Book",
                table: "BookTags",
                column: "ID_Book");

            migrationBuilder.CreateIndex(
                name: "IX_BookTags_ID_Tag",
                table: "BookTags",
                column: "ID_Tag");

            migrationBuilder.CreateIndex(
                name: "IX_Follows_ID_Follower",
                table: "Follows",
                column: "ID_Follower");

            migrationBuilder.CreateIndex(
                name: "IX_Follows_ID_Following",
                table: "Follows",
                column: "ID_Following");

            migrationBuilder.CreateIndex(
                name: "IX_myBooks_ID_Acc",
                table: "myBooks",
                column: "ID_Acc");

            migrationBuilder.CreateIndex(
                name: "IX_myBooks_ID_Book",
                table: "myBooks",
                column: "ID_Book");

            migrationBuilder.CreateIndex(
                name: "IX_Proposes_ID_Acc_Request",
                table: "Proposes",
                column: "ID_Acc_Request");

            migrationBuilder.CreateIndex(
                name: "IX_Proposes_ID_Aut",
                table: "Proposes",
                column: "ID_Aut");

            migrationBuilder.CreateIndex(
                name: "IX_Proposes_ID_Pub",
                table: "Proposes",
                column: "ID_Pub");

            migrationBuilder.CreateIndex(
                name: "IX_ProposeTags_ID_Propose",
                table: "ProposeTags",
                column: "ID_Propose");

            migrationBuilder.CreateIndex(
                name: "IX_ProposeTags_ID_Tag",
                table: "ProposeTags",
                column: "ID_Tag");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ID_Acc",
                table: "Reviews",
                column: "ID_Acc");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ID_Book",
                table: "Reviews",
                column: "ID_Book");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookTags");

            migrationBuilder.DropTable(
                name: "Follows");

            migrationBuilder.DropTable(
                name: "myBooks");

            migrationBuilder.DropTable(
                name: "ProposeTags");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Proposes");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
