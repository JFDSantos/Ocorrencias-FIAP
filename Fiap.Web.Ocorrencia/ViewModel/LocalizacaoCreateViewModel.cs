using System.ComponentModel.DataAnnotations;

namespace Fiap.Web.Ocorrencias.ViewModel
{
    public class LocalizacaoCreateViewModel
    {
        public int id_loc { get; set; }

        [Required(ErrorMessage = "O Endereço é obrigatório!")]
        [Display(Name = "Endereço")]
        [StringLength(30, ErrorMessage = "Endereço não pode exceder 30 caracteres.")]
        public string? endereco { get; set; }

        [Required(ErrorMessage = "A Cidade é obrigatório!")]
        [Display(Name = "Cidade")]
        [StringLength(30, ErrorMessage = "Cidade não pode exceder 30 caracteres.")]
        public string? cidade { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório!")]
        [Display(Name = "CEP")]
        [StringLength(30, ErrorMessage = "CEP não pode exceder 30 caracteres.")]
        public String cep { get; set; }
    }
}
