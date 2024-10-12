using Fiap.Web.Ocorrencias.Data.Repository;
using Fiap.Web.Ocorrencias.Models;

namespace Fiap.Web.Ocorrencias.Services
{
    public class GravidadeServices : IGravidadeServices
    {
        private readonly IGravidadeRepository _repository;

        public GravidadeServices(IGravidadeRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<GravidadeModel> ListarGravidade() => _repository.GetAll();

        public IEnumerable<GravidadeModel> ListarGravidade(int pagina = 1, int tamanho = 10)
        {
            return _repository.GetAll(pagina, tamanho);
        }

        public IEnumerable<GravidadeModel> ListarGravidadeUltimaReferencia(int ultimoId = 0, int tamanho = 10)
        {
            return _repository.GetAllReference(ultimoId, tamanho);
        }

        public GravidadeModel ObterGravidadePorId(int id) => _repository.GetById(id);

        public void CriarGravidade(GravidadeModel cliente) => _repository.Add(cliente);

        public void AtualizarGravidade(GravidadeModel cliente) => _repository.Update(cliente);

        public void DeletarGravidade(int id)
        {
            var gravidade = _repository.GetById(id);
            if (gravidade != null)
            {
                _repository.Delete(gravidade);
            }
        }
    }
}
