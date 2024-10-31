using Fiap.Web.Ocorrencias.Data.Repository;
using Fiap.Web.Ocorrencias.Models;

namespace Fiap.Web.Ocorrencias.Services
{
    public class OcorrenciaServices : IOcorrenciaServices
    {
        private readonly IOcorrenciaRepository _repository;

        public OcorrenciaServices(IOcorrenciaRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<OcorrenciaModel> ListarOcorrencia() => _repository.GetAll();
        public IEnumerable<OcorrenciaModel> ListarOcorrencia(int pagina = 1, int tamanho = 10)
        {
            return _repository.GetAll(pagina, tamanho);
        }

        public IEnumerable<OcorrenciaModel> ListarOcorrenciaUltimaReferencia(int ultimoId = 0, int tamanho = 10)
        {
            return _repository.GetAllReference(ultimoId, tamanho);
        }

        public OcorrenciaModel ObterOcorrenciaPorId(int id) => _repository.GetById(id);

        public void CriarOcorrencia(OcorrenciaModel cliente) => _repository.Add(cliente);

        public void AtualizarOcorrencia(OcorrenciaModel cliente) => _repository.Update(cliente);

        public void DeletarOcorrencia(int id)
        {
            var cliente = _repository.GetById(id);
            if (cliente != null)
            {
                _repository.Delete(cliente);
            }
        }
    }
}
