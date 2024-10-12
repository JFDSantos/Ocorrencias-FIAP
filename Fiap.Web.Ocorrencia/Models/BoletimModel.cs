namespace Fiap.Web.Ocorrencias.Models
{
    public class BoletimModel
    {
        public int id_boletim { get; set; }
        public string? descricao { get; set; }
        public int id_email { get; set; }
        public int id_atendimento { get; set; }


        public EmailModel? Email { get; set; }
        public AtendimentoModel? Atendimento { get; set; }
    }
}
