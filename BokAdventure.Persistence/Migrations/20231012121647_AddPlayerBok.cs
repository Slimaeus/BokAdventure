using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BokAdventure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerBok : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boks_Players_PlayerId",
                table: "Boks");

            migrationBuilder.DropIndex(
                name: "IX_Boks_PlayerId",
                table: "Boks");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Boks");

            migrationBuilder.CreateTable(
                name: "PlayerBok",
                columns: table => new
                {
                    PlayerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    BokId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerBok", x => new { x.PlayerId, x.BokId });
                    table.ForeignKey(
                        name: "FK_PlayerBok_Boks_BokId",
                        column: x => x.BokId,
                        principalTable: "Boks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerBok_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerBok_BokId",
                table: "PlayerBok",
                column: "BokId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerBok");

            migrationBuilder.AddColumn<Guid>(
                name: "PlayerId",
                table: "Boks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boks_PlayerId",
                table: "Boks",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boks_Players_PlayerId",
                table: "Boks",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");
        }
    }
}
