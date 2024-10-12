using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.Web.Ocorrencias.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoTriggers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE OR REPLACE TRIGGER RM552388.TRG_status_ATENDIMENTO
AFTER UPDATE OR INSERT ON TBL_ATENDIMENTO
FOR EACH ROW
BEGIN
   
        IF :NEW.""Status"" = 'F' THEN
            -- Atualize a tabela TBL_VEICULOS_DISPONIVEIS
            UPDATE TBL_VEICULOS_DISPONIVEIS 
            SET ""disponivel"" = 'S', 
                ""id_atendimento"" = 0 
            WHERE ""id_veic"" = :OLD.""id_veic"";

            -- Insira na tabela TBL_BOLETIM
            INSERT INTO TBL_BOLETIM (""descricao"" , ""id_atendimento"")
            VALUES ( 
                    :OLD.""descricao"", 
                    :OLD.""id_atendimento"");
        END IF;
  
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro: ' || SQLERRM);
END;

            ");

            // Trigger para enviar email
            migrationBuilder.Sql(@"
            CREATE OR REPLACE TRIGGER trigger_enviar_email
            AFTER INSERT OR UPDATE ON tbl_boletim
            FOR EACH ROW
            DECLARE
                email_destinatario VARCHAR2(100);
                assunto_email VARCHAR2(100);
                corpo_email VARCHAR2(4000);

            BEGIN
                -- Variáveis para o pacote UTL_MAIL
                mail_conn utl_smtp.connection;

                SELECT 'BOLETIM - ' || :NEW.ID_BOLETIM AS BOLETIM, :NEW.""descricao"", B.EMAIL
                INTO assunto_email, corpo_email, email_destinatario
                FROM TBL_BOLETIM A
                JOIN TBL_EMAIL B
                ON B.ID_EMAIL = :NEW.ID_EMAIL;

                -- Iniciar a conexão com o servidor SMTP
                mail_conn := utl_smtp.open_connection('smtp.gmail.com', 25);

                -- Informações do e-mail
                utl_smtp.mail(mail_conn, 'jaco1v9@gmail.com');
                utl_smtp.rcpt(mail_conn, email_destinatario);
                utl_smtp.open_data(mail_conn);
                utl_smtp.write_data(mail_conn, 'Subject: ' || assunto_email || utl_tcp.crlf || utl_tcp.crlf);
                utl_smtp.write_data(mail_conn, corpo_email || utl_tcp.crlf);
                utl_smtp.close_data(mail_conn);

                -- Fechar a conexão com o servidor SMTP
                utl_smtp.quit(mail_conn);
            EXCEPTION
                WHEN OTHERS THEN
                    -- Lidar com erros
                    DBMS_OUTPUT.PUT_LINE('Erro ao enviar e-mail: ' || SQLERRM);
            END;
            ");

            // Trigger para atendimento
            migrationBuilder.Sql(@"
            CREATE OR REPLACE TRIGGER TRG_ATENDIMENTO
            AFTER INSERT ON TBL_OCORRENCIA
            FOR EACH ROW
            DECLARE
               nID_SERV INTEGER := 0;
               nID_VEIC INTEGER := 0;
            BEGIN
                SELECT B.""id_serv_emergencia"", C.""id_veic""
                INTO nID_SERV, nID_VEIC
                FROM TBL_SERVICO_EMERGENCIA B
                INNER JOIN TBL_VEICULOS_DISPONIVEIS C
                    ON C.""id_serv_emergencia"" = B.""id_serv_emergencia""
                    AND C.""disponivel"" = 'S'
                    AND C.""id_veic"" = (SELECT MAX(""id_veic"") FROM TBL_VEICULOS_DISPONIVEIS WHERE ""id_serv_emergencia"" = B.""id_serv_emergencia"" AND ""disponivel"" = 'S')
                WHERE B.""gravidade_atendida"" >= :NEW.""id_gravidade""
                AND ROWNUM = 1; -- Use :NEW para obter o valor do ""id_ocorrencia""

                IF INSERTING THEN
                    INSERT INTO tbl_atendimento (""descricao"", ""Status"", ""id_ocorrencia"", ""id_veic"", ""id_serv_emergencia"")
                    VALUES (:NEW.""descricao"", 'A', :NEW.""id_ocorrencia"", nID_VEIC, nID_SERV); -- Corrigido para especificar as colunas de destino

                    UPDATE TBL_VEICULOS_DISPONIVEIS SET ""disponivel"" = 'N', ""id_atendimento"" = (SELECT MAX(""id_atendimento"") from tbl_atendimento) WHERE ""id_veic"" = nID_VEIC;

                    UPDATE TBL_OCORRENCIA SET ""id_atendimento"" = (SELECT MAX(""id_atendimento"") from tbl_atendimento) WHERE ""id_ocorrencia"" = :NEW.""id_ocorrencia"";
                END IF;

               EXCEPTION
                WHEN OTHERS THEN
                    -- Lidar com erros
                    DBMS_OUTPUT.PUT_LINE('Erro ' || SQLERRM);
            END;
            ");

            migrationBuilder.Sql(@"
            ALTER TRIGGER trigger_enviar_email DISABLE;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remover triggers na migração de rollback
            migrationBuilder.Sql(@"
            DROP TRIGGER trigger_enviar_email;
            ");

            migrationBuilder.Sql(@"
            DROP TRIGGER TRG_ATENDIMENTO;
            ");

            migrationBuilder.Sql(@"
            DROP TRIGGER TRG_""status""_ATENDIMENTO
            ");
        }
    }
}
