using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Fiap.Web.Ocorrencias.ViewModel
{
    public class ServicoEmergenciaCreateViewModel
    {
        public int id_serv_emergencia { get; set; }

        [Required(ErrorMessage = "A Descrição é obrigatório!")]
        [Display(Name = "Descrição")]
        [StringLength(30, ErrorMessage = "Descrição não pode exceder 30 caracteres.")]
        public string? descricao { get; set; }

        [Required(ErrorMessage = "O Telefone é obrigatório!")]
        [Display(Name = "Telefone")]
        [StringLength(9, ErrorMessage = "Telefone não pode exceder 9 caracteres.")]
        public string? telefone { get; set; }

        [Required(ErrorMessage = "A Gravidade é obrigatório!")]
        [Display(Name = "Gravidade")]
        [Range(1, int.MaxValue, ErrorMessage = "Gravidade inválida!.")]
        public int gravidade_atendida { get; set; }

        [Display(Name = "Gravidade")]
        public SelectList Gravidade { get; set; }

        public ServicoEmergenciaCreateViewModel()
        {
            Gravidade = new SelectList(Enumerable.Empty<SelectListItem>());
        }
    }
}
