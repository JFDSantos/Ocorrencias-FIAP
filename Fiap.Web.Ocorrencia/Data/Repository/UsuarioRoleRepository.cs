using Fiap.Web.Ocorrencias.Data;
using Fiap.Web.Ocorrencias.Models;

namespace Fiap.Web.Ocorrencia.Data.Repository
{
    public class UsuarioRoleRepository : IUsuarioRoleRepository
    {
        private readonly DatabaseContext _context;

        public UsuarioRoleRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<UsuarioRoleModel> GetAll() => _context.Role.ToList();

        public UsuarioRoleModel GetById(int id) => _context.Role.Find(id);

        public void Add(UsuarioRoleModel usuario)
        {
            _context.Role.Add(usuario);
            _context.SaveChanges();
        }
        public void Update(UsuarioRoleModel usuario)
        {
            _context.Update(usuario);
            _context.SaveChanges();
        }

        public void Delete(UsuarioRoleModel usuario)
        {
            _context.Role.Remove(usuario);
            _context.SaveChanges();
        }
    }
}
