using Fiap.Web.Ocorrencias.Models;

namespace Fiap.Web.Ocorrencia.ViewModel
{
    public class VeiculoViewModel
    {
        public int id_veic { get; set; }
        public string? placa { get; set; }
        public char disponivel { get; set; }
        public int id_serv_emergencia { get; set; }
        public int id_atendimento { get; set; }


        public ServicoEmergenciaModel? ServicoEmergencia { get; set; }
    }
}
