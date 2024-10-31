using Fiap.Web.Ocorrencias.Models;

namespace Fiap.Web.Ocorrencia.Data.Repository
{
    public interface IUsuarioRoleRepository
    {
        public IEnumerable<UsuarioRoleModel> GetAll();
        UsuarioRoleModel GetById(int id);
        public void Add(UsuarioRoleModel usuarioRole);
        public void Update(UsuarioRoleModel usuarioRole);
        public void Delete(UsuarioRoleModel usuarioRole);
    }
}
