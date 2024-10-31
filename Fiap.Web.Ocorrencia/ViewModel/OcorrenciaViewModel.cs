using Fiap.Web.Ocorrencias.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Web.Ocorrencia.ViewModel
{
    public class OcorrenciaViewModel
    {
        public int id_ocorrencia { get; set; }
        public DateTime data_hora { get; set; }
        public string? descricao { get; set; }
        public int id_loc { get; set; }
        public int id_gravidade { get; set; }
        public int id_atendimento { get; set; }
        public LocalizacaoViewModel? Localizacao { get; set; }
        public GravidadeViewModel? Gravidade { get; set; }
    }
}
