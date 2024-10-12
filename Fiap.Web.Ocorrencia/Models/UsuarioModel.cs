namespace Fiap.Web.Ocorrencias.Models
{
    public class UsuarioModel
    {
        public int id_usuario { get; set; }
        public string? nome { get; set; }
        public string? email { get; set; }
        public string? senha { get; set; }
        public int? id_role { get; set; }


        public UsuarioRoleModel? Role { get; set; }
    }
}
