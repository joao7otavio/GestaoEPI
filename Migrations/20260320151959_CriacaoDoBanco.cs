using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaEPI.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoDoBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Epis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    NumeroCA = table.Column<string>(type: "TEXT", nullable: false),
                    ValidadeCA = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PeriodicidadeTrocaDias = table.Column<int>(type: "INTEGER", nullable: false),
                    IsAtivo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Epis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Cpf = table.Column<string>(type: "TEXT", nullable: false),
                    Cargo = table.Column<string>(type: "TEXT", nullable: false),
                    Setor = table.Column<string>(type: "TEXT", nullable: false),
                    DataAdmissao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsAtivo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    SenhaHash = table.Column<string>(type: "TEXT", nullable: false),
                    NivelAcesso = table.Column<string>(type: "TEXT", nullable: false),
                    IsAtivo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntregasEpi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EpiId = table.Column<int>(type: "INTEGER", nullable: false),
                    FuncionarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataEntrega = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ProximaTroca = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AssinaturaDigitalHash = table.Column<string>(type: "TEXT", nullable: true),
                    ResponsavelRegistroId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsAtivo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntregasEpi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntregasEpi_Epis_EpiId",
                        column: x => x.EpiId,
                        principalTable: "Epis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntregasEpi_Funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntregasEpi_Usuarios_ResponsavelRegistroId",
                        column: x => x.ResponsavelRegistroId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Treinamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FuncionarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    NomeNR = table.Column<string>(type: "TEXT", nullable: false),
                    DataConclusao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ValidadeMeses = table.Column<int>(type: "INTEGER", nullable: false),
                    StatusVencimento = table.Column<string>(type: "TEXT", nullable: false),
                    ResponsavelRegistroId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsAtivo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treinamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Treinamentos_Funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Treinamentos_Usuarios_ResponsavelRegistroId",
                        column: x => x.ResponsavelRegistroId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntregasEpi_EpiId",
                table: "EntregasEpi",
                column: "EpiId");

            migrationBuilder.CreateIndex(
                name: "IX_EntregasEpi_FuncionarioId",
                table: "EntregasEpi",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_EntregasEpi_ResponsavelRegistroId",
                table: "EntregasEpi",
                column: "ResponsavelRegistroId");

            migrationBuilder.CreateIndex(
                name: "IX_Treinamentos_FuncionarioId",
                table: "Treinamentos",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Treinamentos_ResponsavelRegistroId",
                table: "Treinamentos",
                column: "ResponsavelRegistroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntregasEpi");

            migrationBuilder.DropTable(
                name: "Treinamentos");

            migrationBuilder.DropTable(
                name: "Epis");

            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
