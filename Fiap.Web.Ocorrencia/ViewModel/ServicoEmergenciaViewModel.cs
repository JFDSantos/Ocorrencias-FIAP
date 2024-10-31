using Fiap.Web.Ocorrencias.Models;

namespace Fiap.Web.Ocorrencia.ViewModel
{
    public class ServicoEmergenciaViewModel
    {
        public int id_serv_emergencia { get; set; }
        public string? descricao { get; set; }
        public string? telefone { get; set; }
        public int gravidade_atendida { get; set; }

        public GravidadeModel? Gravidade { get; set; }
    }
}
