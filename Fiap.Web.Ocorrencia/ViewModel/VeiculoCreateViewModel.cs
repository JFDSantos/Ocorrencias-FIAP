using Fiap.Web.Ocorrencias.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Fiap.Web.Ocorrencias.ViewModel
{
    public class VeiculoCreateViewModel
    {
        public int id_veic { get; set; }

        [Required(ErrorMessage = "A Placa é obrigatório!")]
        [Display(Name = "Placa")]
        [StringLength(8, ErrorMessage = "Placa não pode exceder 8 caracteres.")]
        public string? placa { get; set; }
        public char disponivel { get; set; }

        [Required(ErrorMessage = "O Serviço é obrigatório!")]
        [Display(Name = "Serviço")]
        [Range(1, int.MaxValue, ErrorMessage = "Serviço inválido!.")]
        public int id_serv_emergencia { get; set; }

        [Display(Name = "Atendimento")]
        public int id_atendimento { get; set; }

        [Display(Name = "Serviço")]
        public SelectList ServEmergencia { get; set; }

        public VeiculoCreateViewModel()
        {
            ServEmergencia = new SelectList(Enumerable.Empty<SelectListItem>());
        }

    }

}
