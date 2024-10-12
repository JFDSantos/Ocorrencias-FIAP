using Fiap.Web.Ocorrencias.Models;
using System.Linq.Expressions;

namespace Fiap.Web.Ocorrencia.Data.Repository
{
    public interface IAuthRepository
    {
        public IEnumerable<UsuarioModel> GetAll();
        UsuarioModel FirstOrDefault(Expression<Func<UsuarioModel, bool>> predicate);
    }
}
