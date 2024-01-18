using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterclassApiTest.Migrations
{
    /// <inheritdoc />
    public partial class AddAdresAsComplexPropertyToKlant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Woonplaats",
                table: "Klanten",
                newName: "Adres_Woonplaats");

            migrationBuilder.RenameColumn(
                name: "Straat",
                table: "Klanten",
                newName: "Adres_Straat");

            migrationBuilder.RenameColumn(
                name: "Postcode",
                table: "Klanten",
                newName: "Adres_Postcode");

            migrationBuilder.RenameColumn(
                name: "HuisnummerToevoeging",
                table: "Klanten",
                newName: "Adres_HuisnummerToevoeging");

            migrationBuilder.RenameColumn(
                name: "Huisnummer",
                table: "Klanten",
                newName: "Adres_Huisnummer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Adres_Woonplaats",
                table: "Klanten",
                newName: "Woonplaats");

            migrationBuilder.RenameColumn(
                name: "Adres_Straat",
                table: "Klanten",
                newName: "Straat");

            migrationBuilder.RenameColumn(
                name: "Adres_Postcode",
                table: "Klanten",
                newName: "Postcode");

            migrationBuilder.RenameColumn(
                name: "Adres_HuisnummerToevoeging",
                table: "Klanten",
                newName: "HuisnummerToevoeging");

            migrationBuilder.RenameColumn(
                name: "Adres_Huisnummer",
                table: "Klanten",
                newName: "Huisnummer");
        }
    }
}
