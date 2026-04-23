using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaEPI.Migrations
{
    /// <inheritdoc />
    public partial class PadronizaSoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Usuarios",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Treinamentos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "EntregasEpi",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Treinamentos");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "EntregasEpi");
        }
    }
}
