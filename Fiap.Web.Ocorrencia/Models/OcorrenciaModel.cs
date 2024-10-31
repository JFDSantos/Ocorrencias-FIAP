namespace Fiap.Web.Ocorrencias.Models
{
    public class OcorrenciaModel
    {
        public int id_ocorrencia { get; set; }
        public DateTime data_hora { get; set; }
        public string? descricao { get; set; }
        public int id_loc { get; set; }
        public int id_gravidade { get; set; }
        public int id_atendimento { get; set; }


        public LocalizacaoModel? Localizacao { get; set; }
        public GravidadeModel? Gravidade { get; set; }
    }
}
