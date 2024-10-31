using Fiap.Web.Ocorrencias.Models;

namespace Fiap.Web.Ocorrencias.Data.Repository
{
    public interface IServEmergenciaRepository
    {
        public IEnumerable<ServicoEmergenciaModel> GetAll();
        ServicoEmergenciaModel GetById(int id);
        public void Add(ServicoEmergenciaModel servicoEmergencia);
        public void Update(ServicoEmergenciaModel servicoEmergencia);
        public void Delete(ServicoEmergenciaModel servicoEmergencia);
    }
}
