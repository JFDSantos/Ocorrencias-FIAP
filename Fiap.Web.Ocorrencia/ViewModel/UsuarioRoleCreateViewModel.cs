using System.ComponentModel.DataAnnotations;

namespace Fiap.Web.Ocorrencia.ViewModel
{
    public class UsuarioRoleCreateViewModel
    {
        public int id_role { get; set; }
        
        [Required(ErrorMessage = "A Descrição é obrigatório!")]
        [Display(Name = "Descrição")]
        [StringLength(100, ErrorMessage = "Descrição não pode exceder 100 caracteres.")]
        public string? role { get; set; }
    }
}
