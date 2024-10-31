using Fiap.Web.Ocorrencias.Data.Repository;
using Fiap.Web.Ocorrencias.Models;

namespace Fiap.Web.Ocorrencias.Services
{
    public class ServEmergenciaServices : IServEmergenciaServices
    {
        private readonly IServEmergenciaRepository _repository;

        public ServEmergenciaServices(IServEmergenciaRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ServicoEmergenciaModel> ListarServicoEmergencia() => _repository.GetAll();

        public ServicoEmergenciaModel ObterListarServicoEmergenciaPorId(int id) => _repository.GetById(id);

        public void CriarServicoEmergencia(ServicoEmergenciaModel servicoEmergencia) => _repository.Add(servicoEmergencia);

        public void AtualizarServicoEmergencia(ServicoEmergenciaModel servicoEmergencia) => _repository.Update(servicoEmergencia);

        public void DeletarServicoEmergencia(int id)
        {
            var servicoEmergencia = _repository.GetById(id);
            if (servicoEmergencia != null)
            {
                _repository.Delete(servicoEmergencia);
            }
        }
    }
}
