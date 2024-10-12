using Fiap.Web.Ocorrencias.Models;

namespace Fiap.Web.Ocorrencia.Services
{
    public interface IUsuarioServices
    {
        IEnumerable<UsuarioModel> ListarUsuarios();
        UsuarioModel ObterUsuarioPorId(int id);
        void CriarUsuario(UsuarioModel usuario);
        void AtualizarUsuario(UsuarioModel usuario);
        void DeletarUsuario(int id);
    }
}
