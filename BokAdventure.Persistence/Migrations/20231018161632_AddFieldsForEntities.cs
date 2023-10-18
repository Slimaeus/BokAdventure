using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BokAdventure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsForEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Attack",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "BokBank",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Defence",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<ulong>(
                name: "HitPoints",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<long>(
                name: "StatPoints",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Attack",
                table: "Boks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Defence",
                table: "Boks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<ulong>(
                name: "HitPoints",
                table: "Boks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Boks",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attack",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "BokBank",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Defence",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "HitPoints",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "StatPoints",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Attack",
                table: "Boks");

            migrationBuilder.DropColumn(
                name: "Defence",
                table: "Boks");

            migrationBuilder.DropColumn(
                name: "HitPoints",
                table: "Boks");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Boks");
        }
    }
}
