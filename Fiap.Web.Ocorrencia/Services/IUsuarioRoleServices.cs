using Fiap.Web.Ocorrencias.Models;

namespace Fiap.Web.Ocorrencia.Services
{
    public interface IUsuarioRoleServices
    {
        IEnumerable<UsuarioRoleModel> ListarRoles();
        UsuarioRoleModel ObterRolesPorId(int id);
        void CriarRoles(UsuarioRoleModel usuario);
        void AtualizarRoles(UsuarioRoleModel usuario);
        void DeletarRoles(int id);
    }
}
