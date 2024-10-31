using Fiap.Web.Ocorrencias.Models;

namespace Fiap.Web.Ocorrencias.Data.Repository
{
    public interface IOcorrenciaRepository
    {
        public IEnumerable<OcorrenciaModel> GetAll();
         IEnumerable<OcorrenciaModel> GetAll(int page, int size);
        IEnumerable<OcorrenciaModel> GetAllReference(int lastReference, int size);
        OcorrenciaModel GetById(int id);
        public void Add(OcorrenciaModel ocorrencia);
        public void Update(OcorrenciaModel ocorrencia);
        public void Delete(OcorrenciaModel ocorrencia);
    }
}
