namespace Fiap.Web.Ocorrencias.Models
{
    public class VeiculoModel
    {
        public int id_veic { get; set; }
        public string? placa { get; set; }
        public char disponivel { get; set; }
        public int id_serv_emergencia { get; set; }
        public int id_atendimento { get; set; }


        public ServicoEmergenciaModel? ServicoEmergencia { get; set; }
    }
}
