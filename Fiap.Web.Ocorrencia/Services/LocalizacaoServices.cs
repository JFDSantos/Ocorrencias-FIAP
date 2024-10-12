using Fiap.Web.Ocorrencias.Data.Repository;
using Fiap.Web.Ocorrencias.Models;

namespace Fiap.Web.Ocorrencias.Services
{
    public class LocalizacaoServices : ILocalizacaoServices
    {
        private readonly ILocalizacaoRepository _repository;

        public LocalizacaoServices(ILocalizacaoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<LocalizacaoModel> ListarLocalizacao() => _repository.GetAll();

        public LocalizacaoModel ObterLocalizacaoPorId(int id) => _repository.GetById(id);

        public void CriarLocalizacao(LocalizacaoModel loc) => _repository.Add(loc);

        public void AtualizarLocalizacao(LocalizacaoModel loc) => _repository.Update(loc);

        public void DeletarLocalizacao(int id)
        {
            var loc = _repository.GetById(id);
            if (loc != null)
            {
                _repository.Delete(loc);
            }
        }
    }
}
