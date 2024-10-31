using System.ComponentModel.DataAnnotations;

namespace Fiap.Web.Ocorrencias.ViewModel
{
    public class GravidadeCreateViewModel
    {
        public int id_gravidade { get; set; }

        [Required(ErrorMessage = "A Descrição é obrigatório!")]
        [Display(Name = "Descrição")]
        [StringLength(30, ErrorMessage = "Descrição não pode exceder 30 caracteres.")]
        public string? descricao { get; set; }
    }
}
