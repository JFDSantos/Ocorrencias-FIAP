using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.Web.Ocorrencias.Migrations
{
    /// <inheritdoc />
    public partial class CorrigindoConstrins2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TBL_SERVICO_EMERGENCIA_gravidade_atendida",
                table: "TBL_SERVICO_EMERGENCIA",
                column: "gravidade_atendida");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_SERVICO_EMERGENCIA_TBL_GRAVIDADE_gravidade_atendida",
                table: "TBL_SERVICO_EMERGENCIA",
                column: "gravidade_atendida",
                principalTable: "TBL_GRAVIDADE",
                principalColumn: "id_gravidade",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_SERVICO_EMERGENCIA_TBL_GRAVIDADE_gravidade_atendida",
                table: "TBL_SERVICO_EMERGENCIA");

            migrationBuilder.DropIndex(
                name: "IX_TBL_SERVICO_EMERGENCIA_gravidade_atendida",
                table: "TBL_SERVICO_EMERGENCIA");
        }
    }
}
