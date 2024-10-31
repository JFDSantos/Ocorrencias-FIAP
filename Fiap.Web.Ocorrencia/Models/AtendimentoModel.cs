namespace Fiap.Web.Ocorrencias.Models
{
    public class AtendimentoModel
    {
        public int id_atendimento { get; set; }
        public string? descricao { get; set; }
        public string? Status { get; set; }
        public int id_ocorrencia { get; set; }
        public int id_veic { get; set; }
        public int id_serv_emergencia { get; set; }


        public OcorrenciaModel? Ocorrencia { get; set; }
        public VeiculoModel? Veiculo { get; set; }
        public ServicoEmergenciaModel? ServicoEmergencia { get; set; }
    }
}
