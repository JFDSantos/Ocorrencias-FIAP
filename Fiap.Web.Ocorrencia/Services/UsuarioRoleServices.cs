using Fiap.Web.Ocorrencia.Data.Repository;
using Fiap.Web.Ocorrencias.Models;

namespace Fiap.Web.Ocorrencia.Services
{
    public class UsuarioRoleServices : IUsuarioRoleServices
    {
        private readonly IUsuarioRoleRepository _repository;

        public UsuarioRoleServices(IUsuarioRoleRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<UsuarioRoleModel> ListarRoles() => _repository.GetAll();

        public UsuarioRoleModel ObterRolesPorId(int id) => _repository.GetById(id);

        public void CriarRoles(UsuarioRoleModel usuario) => _repository.Add(usuario);

        public void AtualizarRoles(UsuarioRoleModel usuario) => _repository.Update(usuario);

        public void DeletarRoles(int id)
        {
            var usuario = _repository.GetById(id);
            if (usuario != null)
            {
                _repository.Delete(usuario);
            }
        }
    }
}
