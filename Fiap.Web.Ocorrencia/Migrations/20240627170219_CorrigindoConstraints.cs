using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.Web.Ocorrencias.Migrations
{
    /// <inheritdoc />
    public partial class CorrigindoConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_VEICULOS_DISPONIVEIS_TBL_ATENDIMENTO_id_atendimento",
                table: "TBL_VEICULOS_DISPONIVEIS");

            migrationBuilder.DropIndex(
                name: "IX_TBL_VEICULOS_DISPONIVEIS_id_atendimento",
                table: "TBL_VEICULOS_DISPONIVEIS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TBL_VEICULOS_DISPONIVEIS_id_atendimento",
                table: "TBL_VEICULOS_DISPONIVEIS",
                column: "id_atendimento");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_VEICULOS_DISPONIVEIS_TBL_ATENDIMENTO_id_atendimento",
                table: "TBL_VEICULOS_DISPONIVEIS",
                column: "id_atendimento",
                principalTable: "TBL_ATENDIMENTO",
                principalColumn: "id_atendimento",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
