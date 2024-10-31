using Fiap.Web.Ocorrencias.Data.Repository;
using Fiap.Web.Ocorrencias.Models;

namespace Fiap.Web.Ocorrencias.Services
{
    public class VeiculoServices  : IVeiculoServices
    {
        private readonly IVeiculoRepository _repository;

        public VeiculoServices(IVeiculoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<VeiculoModel> ListarVeiculos() => _repository.GetAll();

        public VeiculoModel ObterVeiculosPorId(int id) => _repository.GetById(id);

        public void CriarVeiculos(VeiculoModel veiculo) => _repository.Add(veiculo);

        public void AtualizarVeiculos(VeiculoModel veiculo) => _repository.Update(veiculo);

        public void DeletarVeiculos(int id)
        {
            var veiculo = _repository.GetById(id);
            if (veiculo != null)
            {
                _repository.Delete(veiculo);
            }
        }
    }
}
