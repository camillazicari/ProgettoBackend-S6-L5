using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgettoBackend_S6_L5.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prenotazione_Cliente_ClienteId",
                table: "Prenotazione");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente");

            migrationBuilder.RenameTable(
                name: "Cliente",
                newName: "Clienti");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clienti",
                table: "Clienti",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prenotazione_Clienti_ClienteId",
                table: "Prenotazione",
                column: "ClienteId",
                principalTable: "Clienti",
                principalColumn: "ClienteId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prenotazione_Clienti_ClienteId",
                table: "Prenotazione");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clienti",
                table: "Clienti");

            migrationBuilder.RenameTable(
                name: "Clienti",
                newName: "Cliente");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prenotazione_Cliente_ClienteId",
                table: "Prenotazione",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "ClienteId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
