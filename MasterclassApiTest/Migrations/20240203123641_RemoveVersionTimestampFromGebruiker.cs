using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterclassApiTest.Migrations
{
    /// <inheritdoc />
    public partial class RemoveVersionTimestampFromGebruiker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "Medewerkers");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Klanten");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "Medewerkers",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "Klanten",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
