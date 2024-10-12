using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.Web.Ocorrencias.Migrations
{
    /// <inheritdoc />
    public partial class CorrigindoConstrains3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_OCORRENCIA_TBL_ATENDIMENTO_id_atendimento",
                table: "TBL_OCORRENCIA");

            migrationBuilder.DropIndex(
                name: "IX_TBL_OCORRENCIA_id_atendimento",
                table: "TBL_OCORRENCIA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TBL_OCORRENCIA_id_atendimento",
                table: "TBL_OCORRENCIA",
                column: "id_atendimento");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_OCORRENCIA_TBL_ATENDIMENTO_id_atendimento",
                table: "TBL_OCORRENCIA",
                column: "id_atendimento",
                principalTable: "TBL_ATENDIMENTO",
                principalColumn: "id_atendimento",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
