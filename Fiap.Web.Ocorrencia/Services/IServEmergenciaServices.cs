using Fiap.Web.Ocorrencias.Models;

namespace Fiap.Web.Ocorrencias.Services
{
    public interface IServEmergenciaServices
    {
        IEnumerable<ServicoEmergenciaModel> ListarServicoEmergencia();
        ServicoEmergenciaModel ObterListarServicoEmergenciaPorId(int id);
        void CriarServicoEmergencia(ServicoEmergenciaModel servicoEmergencia);
        void AtualizarServicoEmergencia(ServicoEmergenciaModel servicoEmergencia);
        void DeletarServicoEmergencia(int id);
    }
}
