using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaEPI.Migrations
{
    /// <inheritdoc />
    public partial class CriaTabelaFuncionarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Funcionarios",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Funcionarios");
        }
    }
}
