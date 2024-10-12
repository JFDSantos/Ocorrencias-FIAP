using Fiap.Web.Ocorrencias.Models;

namespace Fiap.Web.Ocorrencias.Data.Repository
{
    public interface ILocalizacaoRepository
    {
        public IEnumerable<LocalizacaoModel> GetAll();
        LocalizacaoModel GetById(int id);
        public void Add(LocalizacaoModel loc);
        public void Update(LocalizacaoModel loc);
        public void Delete(LocalizacaoModel loc);
    }
}
