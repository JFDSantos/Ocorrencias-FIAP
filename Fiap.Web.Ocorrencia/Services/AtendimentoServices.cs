using Fiap.Web.Ocorrencia.Data.Repository;
using Fiap.Web.Ocorrencias.Data.Repository;
using Fiap.Web.Ocorrencias.Models;

namespace Fiap.Web.Ocorrencia.Services
{
    public class AtendimentoServices : IAtendimentoServices
    {
        private readonly IAtendimentoRepository _repository;

        public AtendimentoServices(IAtendimentoRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<AtendimentoModel> ListarAtendimento() => _repository.GetAll();
        public IEnumerable<AtendimentoModel> ListarAtendimento(int pagina = 1, int tamanho = 10)
        {
            return _repository.GetAll(pagina, tamanho);
        }

        public IEnumerable<AtendimentoModel> ListarAtendimentoUltimaReferencia(int ultimoId = 0, int tamanho = 10)
        {
            return _repository.GetAllReference(ultimoId, tamanho);
        }


        public AtendimentoModel ObterAtendimentoPorId(int id) => _repository.GetById(id);

        public void CriarAtendimento(AtendimentoModel atendimento) => _repository.Add(atendimento);

        public void AtualizarAtendimento(AtendimentoModel atendimento) => _repository.Update(atendimento);

        public void DeletarAtendimento(int id)
        {
            var atendimento = _repository.GetById(id);
            if (atendimento != null)
            {
                _repository.Delete(atendimento);
            }
        }
    }
}
