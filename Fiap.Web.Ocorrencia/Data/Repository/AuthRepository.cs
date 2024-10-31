using Fiap.Web.Ocorrencias.Data;
using Fiap.Web.Ocorrencias.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Fiap.Web.Ocorrencia.Data.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DatabaseContext _context;

        public AuthRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<UsuarioModel> GetAll() => _context.Usuarios.Include(c => c.Role).ToList();

        public UsuarioModel FirstOrDefault(Expression<Func<UsuarioModel, bool>> predicate)
        {
            return _context.Usuarios.Include(a => a.Role).FirstOrDefault(predicate);
        }
    }
}
