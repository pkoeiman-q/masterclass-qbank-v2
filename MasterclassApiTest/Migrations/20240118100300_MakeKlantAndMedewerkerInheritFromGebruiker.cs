using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterclassApiTest.Migrations
{
    /// <inheritdoc />
    public partial class MakeKlantAndMedewerkerInheritFromGebruiker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gebruikers");

            migrationBuilder.RenameColumn(
                name: "KlantNummer",
                table: "Klanten",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "LoginNaam",
                table: "Klanten",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Klanten",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Wachtwoord",
                table: "Klanten",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Medewerkers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoginNaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wachtwoord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LaatstIngelogd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayNaam = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medewerkers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Klanten_Id",
                table: "Klanten",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Medewerkers_Id",
                table: "Medewerkers",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medewerkers");

            migrationBuilder.DropIndex(
                name: "IX_Klanten_Id",
                table: "Klanten");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Klanten");

            migrationBuilder.DropColumn(
                name: "Wachtwoord",
                table: "Klanten");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Klanten",
                newName: "KlantNummer");

            migrationBuilder.AlterColumn<string>(
                name: "LoginNaam",
                table: "Klanten",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Gebruikers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GebruikersNaam = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LaatstIngelogd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wachtwoord = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gebruikers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gebruikers_GebruikersNaam",
                table: "Gebruikers",
                column: "GebruikersNaam",
                unique: true);
        }
    }
}
