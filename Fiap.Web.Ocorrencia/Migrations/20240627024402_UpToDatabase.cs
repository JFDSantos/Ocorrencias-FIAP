using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.Web.Ocorrencias.Migrations
{
    /// <inheritdoc />
    public partial class UpToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_EMAIL",
                columns: table => new
                {
                    id_email = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    cpf = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_EMAIL", x => x.id_email);
                });

            migrationBuilder.CreateTable(
                name: "TBL_GRAVIDADE",
                columns: table => new
                {
                    id_gravidade = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_GRAVIDADE", x => x.id_gravidade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_LOCALIZACAO",
                columns: table => new
                {
                    id_loc = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    endereco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    cidade = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    cep = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_LOCALIZACAO", x => x.id_loc);
                });

            migrationBuilder.CreateTable(
                name: "TBL_ROLES",
                columns: table => new
                {
                    id_role = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    role = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ROLES", x => x.id_role);
                });

            migrationBuilder.CreateTable(
                name: "TBL_SERVICO_EMERGENCIA",
                columns: table => new
                {
                    id_serv_emergencia = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    telefone = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    gravidade_atendida = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_SERVICO_EMERGENCIA", x => x.id_serv_emergencia);
                });

            migrationBuilder.CreateTable(
                name: "TBL_USUARIO",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    senha = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    role = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_USUARIO", x => x.id_usuario);
                    table.ForeignKey(
                        name: "FK_TBL_USUARIO_TBL_ROLES_role",
                        column: x => x.role,
                        principalTable: "TBL_ROLES",
                        principalColumn: "id_role",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_ATENDIMENTO",
                columns: table => new
                {
                    id_atendimento = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    id_ocorrencia = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    id_veic = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    id_serv_emergencia = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ATENDIMENTO", x => x.id_atendimento);
                    table.ForeignKey(
                        name: "FK_TBL_ATENDIMENTO_TBL_SERVICO_EMERGENCIA_id_serv_emergencia",
                        column: x => x.id_serv_emergencia,
                        principalTable: "TBL_SERVICO_EMERGENCIA",
                        principalColumn: "id_serv_emergencia",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_BOLETIM",
                columns: table => new
                {
                    id_boletim = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    id_email = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    id_atendimento = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_BOLETIM", x => x.id_boletim);
                    table.ForeignKey(
                        name: "FK_TBL_BOLETIM_TBL_ATENDIMENTO_id_atendimento",
                        column: x => x.id_atendimento,
                        principalTable: "TBL_ATENDIMENTO",
                        principalColumn: "id_atendimento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_BOLETIM_TBL_EMAIL_id_email",
                        column: x => x.id_email,
                        principalTable: "TBL_EMAIL",
                        principalColumn: "id_email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_OCORRENCIA",
                columns: table => new
                {
                    id_ocorrencia = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    data_hora = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    id_loc = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    id_gravidade = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    id_atendimento = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_OCORRENCIA", x => x.id_ocorrencia);
                    table.ForeignKey(
                        name: "FK_TBL_OCORRENCIA_TBL_ATENDIMENTO_id_atendimento",
                        column: x => x.id_atendimento,
                        principalTable: "TBL_ATENDIMENTO",
                        principalColumn: "id_atendimento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_OCORRENCIA_TBL_GRAVIDADE_id_gravidade",
                        column: x => x.id_gravidade,
                        principalTable: "TBL_GRAVIDADE",
                        principalColumn: "id_gravidade",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_OCORRENCIA_TBL_LOCALIZACAO_id_loc",
                        column: x => x.id_loc,
                        principalTable: "TBL_LOCALIZACAO",
                        principalColumn: "id_loc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_VEICULOS_DISPONIVEIS",
                columns: table => new
                {
                    id_veic = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    placa = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    disponivel = table.Column<string>(type: "NVARCHAR2(1)", nullable: false),
                    id_serv_emergencia = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    id_atendimento = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_VEICULOS_DISPONIVEIS", x => x.id_veic);
                    table.ForeignKey(
                        name: "FK_TBL_VEICULOS_DISPONIVEIS_TBL_ATENDIMENTO_id_atendimento",
                        column: x => x.id_atendimento,
                        principalTable: "TBL_ATENDIMENTO",
                        principalColumn: "id_atendimento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_VEICULOS_DISPONIVEIS_TBL_SERVICO_EMERGENCIA_id_serv_emergencia",
                        column: x => x.id_serv_emergencia,
                        principalTable: "TBL_SERVICO_EMERGENCIA",
                        principalColumn: "id_serv_emergencia",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ATENDIMENTO_id_ocorrencia",
                table: "TBL_ATENDIMENTO",
                column: "id_ocorrencia");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ATENDIMENTO_id_serv_emergencia",
                table: "TBL_ATENDIMENTO",
                column: "id_serv_emergencia");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ATENDIMENTO_id_veic",
                table: "TBL_ATENDIMENTO",
                column: "id_veic");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_BOLETIM_id_atendimento",
                table: "TBL_BOLETIM",
                column: "id_atendimento");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_BOLETIM_id_email",
                table: "TBL_BOLETIM",
                column: "id_email");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_OCORRENCIA_id_atendimento",
                table: "TBL_OCORRENCIA",
                column: "id_atendimento");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_OCORRENCIA_id_gravidade",
                table: "TBL_OCORRENCIA",
                column: "id_gravidade");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_OCORRENCIA_id_loc",
                table: "TBL_OCORRENCIA",
                column: "id_loc");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_USUARIO_role",
                table: "TBL_USUARIO",
                column: "role");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_VEICULOS_DISPONIVEIS_id_atendimento",
                table: "TBL_VEICULOS_DISPONIVEIS",
                column: "id_atendimento");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_VEICULOS_DISPONIVEIS_id_serv_emergencia",
                table: "TBL_VEICULOS_DISPONIVEIS",
                column: "id_serv_emergencia");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_ATENDIMENTO_TBL_OCORRENCIA_id_ocorrencia",
                table: "TBL_ATENDIMENTO",
                column: "id_ocorrencia",
                principalTable: "TBL_OCORRENCIA",
                principalColumn: "id_ocorrencia",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_ATENDIMENTO_TBL_VEICULOS_DISPONIVEIS_id_veic",
                table: "TBL_ATENDIMENTO",
                column: "id_veic",
                principalTable: "TBL_VEICULOS_DISPONIVEIS",
                principalColumn: "id_veic",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_ATENDIMENTO_TBL_OCORRENCIA_id_ocorrencia",
                table: "TBL_ATENDIMENTO");

            migrationBuilder.DropForeignKey(
                name: "FK_TBL_ATENDIMENTO_TBL_SERVICO_EMERGENCIA_id_serv_emergencia",
                table: "TBL_ATENDIMENTO");

            migrationBuilder.DropForeignKey(
                name: "FK_TBL_VEICULOS_DISPONIVEIS_TBL_SERVICO_EMERGENCIA_id_serv_emergencia",
                table: "TBL_VEICULOS_DISPONIVEIS");

            migrationBuilder.DropForeignKey(
                name: "FK_TBL_ATENDIMENTO_TBL_VEICULOS_DISPONIVEIS_id_veic",
                table: "TBL_ATENDIMENTO");

            migrationBuilder.DropTable(
                name: "TBL_BOLETIM");

            migrationBuilder.DropTable(
                name: "TBL_USUARIO");

            migrationBuilder.DropTable(
                name: "TBL_EMAIL");

            migrationBuilder.DropTable(
                name: "TBL_ROLES");

            migrationBuilder.DropTable(
                name: "TBL_OCORRENCIA");

            migrationBuilder.DropTable(
                name: "TBL_GRAVIDADE");

            migrationBuilder.DropTable(
                name: "TBL_LOCALIZACAO");

            migrationBuilder.DropTable(
                name: "TBL_SERVICO_EMERGENCIA");

            migrationBuilder.DropTable(
                name: "TBL_VEICULOS_DISPONIVEIS");

            migrationBuilder.DropTable(
                name: "TBL_ATENDIMENTO");
        }
    }
}
