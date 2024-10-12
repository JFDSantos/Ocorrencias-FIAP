using Fiap.Web.Ocorrencias.Models;

namespace Fiap.Web.Ocorrencias.Services
{
    public interface ILocalizacaoServices
    {
        IEnumerable<LocalizacaoModel> ListarLocalizacao();
        LocalizacaoModel ObterLocalizacaoPorId(int id);
        void CriarLocalizacao(LocalizacaoModel loc);
        void AtualizarLocalizacao(LocalizacaoModel loc);
        void DeletarLocalizacao(int id);
    }
}
