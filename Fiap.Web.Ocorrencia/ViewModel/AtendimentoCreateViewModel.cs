using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fiap.Web.Ocorrencias.ViewModel
{
    public class AtendimentoCreateViewModel
    {
        public int id_atendimento { get; set; }
        public string? descricao { get; set; }
        public string? Status { get; set; }
        public int id_ocorrencia { get; set; }
        public int id_veic { get; set; }
        public int id_serv_emergencia { get; set; }
        public SelectList Veiculos { get; set; }
        public SelectList ServicoEmergencia { get; set; }

        public AtendimentoCreateViewModel()
        {
            Veiculos = new SelectList(Enumerable.Empty<SelectListItem>());
            ServicoEmergencia = new SelectList(Enumerable.Empty<SelectListItem>());
        }

    }
}
