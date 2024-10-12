using Fiap.Web.Ocorrencias.Models;

namespace Fiap.Web.Ocorrencias.Data.Repository
{
    public interface IVeiculoRepository
    {
        public IEnumerable<VeiculoModel> GetAll();
        VeiculoModel GetById(int id);
        public void Add(VeiculoModel veiculo);
        public void Update(VeiculoModel veiculo);
        public void Delete(VeiculoModel veiculo);
    }
}
