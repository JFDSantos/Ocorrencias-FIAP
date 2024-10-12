using Fiap.Web.Ocorrencias.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Fiap.Web.Ocorrencia.ViewModel
{
    public class UsuarioCreateViewModel
    {
        public int id_usuario { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório!")]
        [Display(Name = "Nome")]
        [StringLength(100, ErrorMessage = "Nome não pode exceder 100 caracteres.")]
        public string? nome { get; set; }

        [Required(ErrorMessage = "O Email é obrigatório!")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string? email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatório!")]
        [StringLength(10,MinimumLength = 6, ErrorMessage = "Senha não pode exceder 10 caracteres ou ter menos que 6 caracteres")]
        [PasswordPropertyText]
        public string? senha { get; set; }

        [Required(ErrorMessage = "O Cargo é obrigatória!")]
        [Display(Name = "Cargo")]
        [JsonPropertyName("userRole")]
        [Range(1, int.MaxValue, ErrorMessage = "Cargo inválido!")]
        public int? role { get; set; }

        [Display(Name = "Cargo")]
        public SelectList Role { get; set; }

        public UsuarioCreateViewModel()
        {
            Role = new SelectList(Enumerable.Empty<SelectListItem>());
        }
    }
}
