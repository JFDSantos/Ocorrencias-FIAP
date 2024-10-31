using Fiap.Web.Ocorrencias.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Fiap.Web.Ocorrencias.Data
{
    public class DatabaseContext : DbContext
    {

        public virtual DbSet<AtendimentoModel> Atendimentos { get; set; }
        public virtual DbSet<BoletimModel> Boletims { get; set; }
        public virtual DbSet<EmailModel> Emails { get; set; }
        public virtual DbSet<GravidadeModel> Gravidades { get; set; }
        public virtual DbSet<LocalizacaoModel> Localizacaos { get; set; }
        public virtual DbSet<OcorrenciaModel> Ocorrencias { get; set; }
        public virtual DbSet<ServicoEmergenciaModel> ServicoEmergencias { get; set; }
        public virtual DbSet<UsuarioModel> Usuarios { get; set; }
        public virtual DbSet<UsuarioRoleModel> Role { get; set; }
        public virtual DbSet<VeiculoModel> Veiculos { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected DatabaseContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<AtendimentoModel>(
                entity =>
                {
                    entity.ToTable("TBL_ATENDIMENTO");
                    entity.HasKey(e => e.id_atendimento);
                    entity.Property(e => e.descricao).IsRequired();
                    entity.Property(e => e.Status).IsRequired();

                    entity.HasOne(e => e.ServicoEmergencia).WithMany().HasForeignKey(e => e.id_serv_emergencia).IsRequired();
                    entity.HasOne(e => e.Veiculo).WithMany().HasForeignKey(e => e.id_veic).IsRequired();
                    entity.HasOne(e => e.Ocorrencia).WithMany().HasForeignKey(e => e.id_ocorrencia).IsRequired();
                }
            );

            modelBuilder.Entity<BoletimModel>(
                entity =>
                {
                    entity.ToTable("TBL_BOLETIM");
                    entity.HasKey(e => e.id_boletim);
                    entity.Property(e => e.descricao).IsRequired();

                    entity.HasOne(e => e.Email).WithMany().HasForeignKey(e => e.id_email).IsRequired();
                    entity.HasOne(e => e.Atendimento).WithMany().HasForeignKey(e => e.id_atendimento).IsRequired();
                }
            );

            modelBuilder.Entity<EmailModel>(
                entity =>
                {
                    entity.ToTable("TBL_EMAIL");
                    entity.HasKey(e => e.id_email);
                    entity.Property(e => e.email).IsRequired();
                    entity.Property(e => e.cpf).IsRequired();

                }
            );

            modelBuilder.Entity<GravidadeModel>(
                entity =>
                {
                    entity.ToTable("TBL_GRAVIDADE");
                    entity.HasKey(e => e.id_gravidade);
                    entity.Property(e => e.descricao).IsRequired();
                }
            );

            modelBuilder.Entity<LocalizacaoModel>(
                entity =>
                {
                    entity.ToTable("TBL_LOCALIZACAO");
                    entity.HasKey(e => e.id_loc);
                    entity.Property(e => e.endereco).IsRequired();
                    entity.Property(e => e.cidade).IsRequired();
                    entity.Property(e => e.cep).IsRequired();
                }
            );

            modelBuilder.Entity<OcorrenciaModel>(
                entity =>
                {
                    entity.ToTable("TBL_OCORRENCIA");
                    entity.HasKey(e => e.id_ocorrencia);
                    entity.Property(e => e.descricao).IsRequired();

                    entity.HasOne(e => e.Localizacao).WithMany().HasForeignKey(e => e.id_loc).IsRequired();
                    entity.HasOne(e => e.Gravidade).WithMany().HasForeignKey(e => e.id_gravidade).IsRequired();

                }
            );

            modelBuilder.Entity<ServicoEmergenciaModel>(
                entity =>
                {
                    entity.ToTable("TBL_SERVICO_EMERGENCIA");
                    entity.HasKey(e => e.id_serv_emergencia);
                    entity.Property(e => e.telefone).IsRequired();
                    entity.Property(e => e.descricao).IsRequired();

                    entity.HasOne(e => e.Gravidade).WithMany().HasForeignKey(e => e.gravidade_atendida).IsRequired();
                }
            );

            modelBuilder.Entity<UsuarioModel>(
                entity =>
                {
                    entity.ToTable("TBL_USUARIO");
                    entity.HasKey(e => e.id_usuario);
                    entity.Property(e => e.nome).IsRequired();
                    entity.Property(e => e.email).IsRequired();
                    entity.Property(e => e.senha).IsRequired();
                    entity.Property(e => e.id_role).IsRequired();

                    entity.HasOne(e => e.Role).WithMany().HasForeignKey(e => e.id_role).IsRequired();
                }
            );

            modelBuilder.Entity<VeiculoModel>(
                entity =>
                {
                    entity.ToTable("TBL_VEICULOS_DISPONIVEIS");
                    entity.HasKey(e => e.id_veic);
                    entity.Property(e => e.placa).IsRequired();
                    entity.Property(e => e.disponivel).IsRequired();

                    entity.HasOne(e => e.ServicoEmergencia).WithMany().HasForeignKey(e => e.id_serv_emergencia).IsRequired();
                }
            );

            modelBuilder.Entity<UsuarioRoleModel>(
                entity =>
                {
                    entity.ToTable("TBL_ROLES");
                    entity.HasKey(e => e.id_role);
                    entity.Property(e => e.role).IsRequired();
                }
            );

          

        }
    }

}
