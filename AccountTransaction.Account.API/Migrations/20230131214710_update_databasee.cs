using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountTransaction.Account.API.Migrations
{
    public partial class update_databasee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conta",
                columns: table => new
                {
                    Numero_Conta = table.Column<int>(type: "int", nullable: false),
                    Numero_Agencia = table.Column<int>(type: "int", nullable: false),
                    Nome_Titular = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo_Conta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Identificador_Titular = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativa = table.Column<int>(type: "int", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conta", x => new { x.Numero_Conta, x.Numero_Agencia });
                });

            migrationBuilder.CreateTable(
                name: "Cartao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero_Cartao = table.Column<long>(type: "bigint", nullable: false),
                    Data_Vencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CVC = table.Column<int>(type: "int", nullable: false),
                    Numero_Conta = table.Column<int>(type: "int", nullable: false),
                    Numero_Agencia = table.Column<int>(type: "int", nullable: false),
                    Limite_Saldo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Limite_Saldo_Disponivel = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Ativo = table.Column<int>(type: "int", nullable: true),
                    Bloqueado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cartao_Conta_Numero_Conta_Numero_Agencia",
                        columns: x => new { x.Numero_Conta, x.Numero_Agencia },
                        principalTable: "Conta",
                        principalColumns: new[] { "Numero_Conta", "Numero_Agencia" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cartao_Numero_Conta_Numero_Agencia",
                table: "Cartao",
                columns: new[] { "Numero_Conta", "Numero_Agencia" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cartao");

            migrationBuilder.DropTable(
                name: "Conta");
        }
    }
}
