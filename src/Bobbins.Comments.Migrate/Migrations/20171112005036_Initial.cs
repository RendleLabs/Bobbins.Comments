using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Bobbins.Comments.Migrate.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                    DownVoteCount = table.Column<int>(type: "int4", nullable: false),
                    LinkId = table.Column<int>(type: "int4", nullable: false),
                    ReplyCount = table.Column<int>(type: "int4", nullable: false),
                    ReplyToId = table.Column<int>(type: "int4", nullable: true),
                    Text = table.Column<string>(type: "text", nullable: true),
                    UpVoteCount = table.Column<int>(type: "int4", nullable: false),
                    User = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CommentId = table.Column<int>(type: "int4", nullable: false),
                    User = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true),
                    Value = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Votes_CommentId_User",
                table: "Votes",
                columns: new[] { "CommentId", "User" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Comments");
        }
    }
}
