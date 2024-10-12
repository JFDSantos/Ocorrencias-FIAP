using Fiap.Web.Ocorrencias.Models;

namespace Fiap.Web.Ocorrencia.Data.Repository
{
    public interface IUsuarioRepository
    {
        public IEnumerable<UsuarioModel> GetAll();
        UsuarioModel GetById(int id);
        public void Add(UsuarioModel usuario);
        public void Update(UsuarioModel usuario);
        public void Delete(UsuarioModel usuario);
    }
}
