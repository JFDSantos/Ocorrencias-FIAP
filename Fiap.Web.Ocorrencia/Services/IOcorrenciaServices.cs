using Fiap.Web.Ocorrencias.Models;

namespace Fiap.Web.Ocorrencias.Services
{
    public interface IOcorrenciaServices
    {
        IEnumerable<OcorrenciaModel> ListarOcorrencia();
        IEnumerable<OcorrenciaModel> ListarOcorrencia(int pagina = 0, int tamanho = 10);
        IEnumerable<OcorrenciaModel> ListarOcorrenciaUltimaReferencia(int ultimoId = 0, int tamanho = 10);
        OcorrenciaModel ObterOcorrenciaPorId(int id);
        void CriarOcorrencia(OcorrenciaModel ocorrencia);
        void AtualizarOcorrencia(OcorrenciaModel ocorrencia);
        void DeletarOcorrencia(int id);
    }
}
