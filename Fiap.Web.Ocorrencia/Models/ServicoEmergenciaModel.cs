using System.ComponentModel.DataAnnotations.Schema;

namespace Fiap.Web.Ocorrencias.Models
{
    public class ServicoEmergenciaModel
    {
        public int id_serv_emergencia { get; set; }
        public string? descricao { get; set; }
        public string? telefone { get; set; }
        public int gravidade_atendida { get; set; }

        public GravidadeModel? Gravidade { get; set; }
    }
}
