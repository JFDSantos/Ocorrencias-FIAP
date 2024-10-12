using Fiap.Web.Ocorrencias.Models;

namespace Fiap.Web.Ocorrencia.Data.Repository
{
    public interface IAtendimentoRepository
    {
        public IEnumerable<AtendimentoModel> GetAll();
        IEnumerable<AtendimentoModel> GetAll(int page, int size);
        IEnumerable<AtendimentoModel> GetAllReference(int lastReference, int size);
        AtendimentoModel GetById(int id);
        public void Add(AtendimentoModel ocorrencia);
        public void Update(AtendimentoModel ocorrencia);
        public void Delete(AtendimentoModel ocorrencia);
    }
}
