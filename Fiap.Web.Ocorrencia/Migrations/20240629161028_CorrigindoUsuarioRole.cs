using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.Web.Ocorrencias.Migrations
{
    /// <inheritdoc />
    public partial class CorrigindoUsuarioRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_USUARIO_TBL_ROLES_role",
                table: "TBL_USUARIO");

            migrationBuilder.RenameColumn(
                name: "role",
                table: "TBL_USUARIO",
                newName: "id_role");

            migrationBuilder.RenameIndex(
                name: "IX_TBL_USUARIO_role",
                table: "TBL_USUARIO",
                newName: "IX_TBL_USUARIO_id_role");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_USUARIO_TBL_ROLES_id_role",
                table: "TBL_USUARIO",
                column: "id_role",
                principalTable: "TBL_ROLES",
                principalColumn: "id_role",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_USUARIO_TBL_ROLES_id_role",
                table: "TBL_USUARIO");

            migrationBuilder.RenameColumn(
                name: "id_role",
                table: "TBL_USUARIO",
                newName: "role");

            migrationBuilder.RenameIndex(
                name: "IX_TBL_USUARIO_id_role",
                table: "TBL_USUARIO",
                newName: "IX_TBL_USUARIO_role");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_USUARIO_TBL_ROLES_role",
                table: "TBL_USUARIO",
                column: "role",
                principalTable: "TBL_ROLES",
                principalColumn: "id_role",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
