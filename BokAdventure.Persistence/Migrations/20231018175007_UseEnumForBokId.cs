using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BokAdventure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UseEnumForBokId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerBok_Boks_BokId",
                table: "PlayerBok");

            migrationBuilder.DropIndex(
                name: "IX_PlayerBok_BokId",
                table: "PlayerBok");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Boks",
                newName: "ModifiedDate");

            migrationBuilder.AddColumn<int>(
                name: "BokId1",
                table: "PlayerBok",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Boks",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Boks",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Boks");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "Boks",
                newName: "Name");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Boks",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

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
    }
}
