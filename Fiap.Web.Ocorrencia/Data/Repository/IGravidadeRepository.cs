using Fiap.Web.Ocorrencias.Models;

namespace Fiap.Web.Ocorrencias.Data.Repository
{
    public interface IGravidadeRepository
    {
        public IEnumerable<GravidadeModel> GetAll();
        IEnumerable<GravidadeModel> GetAll(int page, int size);
        IEnumerable<GravidadeModel> GetAllReference(int lastReference, int size);
        GravidadeModel GetById(int id);
        public void Add(GravidadeModel gravidade);
        public void Update(GravidadeModel gravidade);
        public void Delete(GravidadeModel gravidade);
    }
}
