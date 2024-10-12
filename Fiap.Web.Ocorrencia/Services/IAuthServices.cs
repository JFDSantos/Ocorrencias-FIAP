using Fiap.Web.Ocorrencias.Models;
using System.Collections.Generic;

namespace Fiap.Web.Ocorrencia.Services
{
    public interface IAuthServices
    {
        UsuarioModel Authenticate(string email, string password);
        IEnumerable<UsuarioModel> ListarUsuarios();
    }
}
