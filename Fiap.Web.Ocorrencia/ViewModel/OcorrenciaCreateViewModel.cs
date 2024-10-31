using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Fiap.Web.Ocorrencias.ViewModel
{
    public class OcorrenciaCreateViewModel
    {
        public int id_ocorrencia { get; set; }

        [Required(ErrorMessage = "A Data/Hora é obrigatório!")]
        [Display(Name = "Data/Hora")]
        [DataType(DataType.Text, ErrorMessage ="Data/Hora (mm/dd/yyyy hh24:mi:ss) Inválida!")]
        public DateTime data_hora { get; set; }

        [Required(ErrorMessage = "A Descrição é obrigatório!")]
        [Display(Name = "Descrição")]
        [StringLength(100, ErrorMessage ="Descrição não pode exceder 100 caracteres.")]
        public string? descricao { get; set; }

        [Required(ErrorMessage = "A Localização é obrigatório!")]
        [Display(Name = "Localização")]
        [Range(1,int.MaxValue,ErrorMessage = "Localização inválida!.")]
        public int id_loc { get; set; }

        [Required(ErrorMessage = "A Gravidade é obrigatória!")]
        [Display(Name = "Gravidade")]
        [Range(1, int.MaxValue, ErrorMessage = "Gravidade inválida!")]
        public int id_gravidade { get; set; }


        [Display(Name = "Atendimento")]
        public int id_atendimento { get; set; }

        [Display(Name = "Localização")]
        public SelectList Localizacao { get; set; }

        [Display(Name = "Gravidade")]
        public SelectList Gravidade { get; set; }

        public OcorrenciaCreateViewModel()
        {
            Localizacao = new SelectList(Enumerable.Empty<SelectListItem>());
            Gravidade = new SelectList(Enumerable.Empty<SelectListItem>());
        }
    }
}
