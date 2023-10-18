using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BokAdventure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddBokName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Name",
                table: "Boks");
        }
    }
}
