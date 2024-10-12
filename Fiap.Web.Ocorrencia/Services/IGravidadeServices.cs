using Fiap.Web.Ocorrencias.Models;

namespace Fiap.Web.Ocorrencias.Services
{
    public interface IGravidadeServices
    {
        IEnumerable<GravidadeModel> ListarGravidade();
        IEnumerable<GravidadeModel> ListarGravidade(int pagina = 0, int tamanho = 10);
        IEnumerable<GravidadeModel> ListarGravidadeUltimaReferencia(int ultimoId = 0, int tamanho = 10);
        GravidadeModel ObterGravidadePorId(int id);
        void CriarGravidade(GravidadeModel gravidade);
        void AtualizarGravidade(GravidadeModel gravidade);
        void DeletarGravidade(int id);
    }
}
