using Fiap.Web.Ocorrencia.Data.Repository;
using Fiap.Web.Ocorrencias.Models;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Fiap.Web.Ocorrencia.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly IAuthRepository _auth;

        public AuthServices(IAuthRepository auth)
        {
            _auth = auth;
        }

        public IEnumerable<UsuarioModel> ListarUsuarios() => _auth.GetAll();

        public UsuarioModel Authenticate(string email, string password)
        {
            // Aqui você normalmente faria a verificação de senha de forma segura
            Expression<Func<UsuarioModel, bool>> predicate = u => u.email == email && u.senha == password;
            return _auth.FirstOrDefault(predicate);
        }
    }
}
