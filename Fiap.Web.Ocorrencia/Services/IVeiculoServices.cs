using Fiap.Web.Ocorrencias.Models;

namespace Fiap.Web.Ocorrencias.Services
{
    public interface IVeiculoServices
    {
        IEnumerable<VeiculoModel> ListarVeiculos();
        VeiculoModel ObterVeiculosPorId(int id);
        void CriarVeiculos(VeiculoModel ocorrencia);
        void AtualizarVeiculos(VeiculoModel ocorrencia);
        void DeletarVeiculos(int id);
    }
}
