using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BokAdventure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBokIdType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerBok_Boks_BokId1",
                table: "PlayerBok");

            migrationBuilder.DropIndex(
                name: "IX_PlayerBok_BokId1",
                table: "PlayerBok");

            migrationBuilder.DropColumn(
                name: "BokId1",
                table: "PlayerBok");

            migrationBuilder.AlterColumn<int>(
                name: "BokId",
                table: "PlayerBok",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerBok_BokId",
                table: "PlayerBok",
                column: "BokId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerBok_Boks_BokId",
                table: "PlayerBok",
                column: "BokId",
                principalTable: "Boks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerBok_Boks_BokId",
                table: "PlayerBok");

            migrationBuilder.DropIndex(
                name: "IX_PlayerBok_BokId",
                table: "PlayerBok");

            migrationBuilder.AlterColumn<Guid>(
                name: "BokId",
                table: "PlayerBok",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "BokId1",
                table: "PlayerBok",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerBok_BokId1",
                table: "PlayerBok",
                column: "BokId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerBok_Boks_BokId1",
                table: "PlayerBok",
                column: "BokId1",
                principalTable: "Boks",
                principalColumn: "Id");
        }
    }
}
