using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.Web.Ocorrencias.Migrations
{
    /// <inheritdoc />
    public partial class CorrigindoTypeData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "cep",
                table: "TBL_LOCALIZACAO",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "NUMBER(19)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "cep",
                table: "TBL_LOCALIZACAO",
                type: "NUMBER(19)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");
        }
    }
}
