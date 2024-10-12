using Fiap.Web.Ocorrencia.Data.Repository;
using Fiap.Web.Ocorrencias.Data.Repository;
using Fiap.Web.Ocorrencias.Models;

namespace Fiap.Web.Ocorrencia.Services
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioServices(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<UsuarioModel> ListarUsuarios() => _repository.GetAll();

        public UsuarioModel ObterUsuarioPorId(int id) => _repository.GetById(id);

        public void CriarUsuario(UsuarioModel usuario) => _repository.Add(usuario);

        public void AtualizarUsuario(UsuarioModel usuario) => _repository.Update(usuario);

        public void DeletarUsuario(int id)
        {
            var usuario = _repository.GetById(id);
            if (usuario != null)
            {
                _repository.Delete(usuario);
            }
        }
    }
}
