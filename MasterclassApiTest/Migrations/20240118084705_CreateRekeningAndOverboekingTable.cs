using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterclassApiTest.Migrations
{
    /// <inheritdoc />
    public partial class CreateRekeningAndOverboekingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Overboekingen",
                columns: table => new
                {
                    VolgNummer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoekDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Bedrag = table.Column<int>(type: "int", nullable: false),
                    VanRekening = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TegenRekening = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Overboekingen", x => x.VolgNummer);
                });

            migrationBuilder.CreateTable(
                name: "Rekeningen",
                columns: table => new
                {
                    RekeningNummer = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    KlantNummer = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Saldo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BeginDatum = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rekeningen", x => x.RekeningNummer);
                    table.ForeignKey(
                        name: "FK_Rekeningen_Klanten_KlantNummer",
                        column: x => x.KlantNummer,
                        principalTable: "Klanten",
                        principalColumn: "KlantNummer",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rekeningen_KlantNummer",
                table: "Rekeningen",
                column: "KlantNummer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Overboekingen");

            migrationBuilder.DropTable(
                name: "Rekeningen");
        }
    }
}
