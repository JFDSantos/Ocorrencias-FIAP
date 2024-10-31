using Fiap.Web.Ocorrencias.Models;

namespace Fiap.Web.Ocorrencia.Services
{
    public interface IAtendimentoServices
    {
        IEnumerable<AtendimentoModel> ListarAtendimento();
        IEnumerable<AtendimentoModel> ListarAtendimento(int pagina = 0, int tamanho = 10);
        IEnumerable<AtendimentoModel> ListarAtendimentoUltimaReferencia(int ultimoId = 0, int tamanho = 10);
        AtendimentoModel ObterAtendimentoPorId(int id);
        void CriarAtendimento(AtendimentoModel atendimento);
        void AtualizarAtendimento(AtendimentoModel atendimento);
        void DeletarAtendimento(int id);
    }
}
