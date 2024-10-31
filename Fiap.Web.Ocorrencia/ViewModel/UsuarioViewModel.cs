using Fiap.Web.Ocorrencias.Models;

namespace Fiap.Web.Ocorrencia.ViewModel
{
    public class UsuarioViewModel
    {
        public int id_usuario { get; set; }
        public string? nome { get; set; }
        public string? email { get; set; }
        public string? senha { get; set; }
        public int? id_role { get; set; }


        public UsuarioRoleModel? Role { get; set; }
    }
}
