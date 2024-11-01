using AutoMapper;
using Fiap.Web.Ocorrencia.Services;
using Fiap.Web.Ocorrencia.ViewModel;
using Fiap.Web.Ocorrencias.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Web.Ocorrencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "gerente")]
    public class AtendimentoController : ControllerBase
    {
        private readonly IAtendimentoServices _atendimentoServices;
        private readonly IMapper _mapper;

        public AtendimentoController(IAtendimentoServices atendimentoServices, IMapper mapper)
        {
            _atendimentoServices = atendimentoServices;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<AtendimentoPaginacaoReferenciaViewModel> Get([FromQuery] int referencia = 0, [FromQuery] int tamanho = 10)
        {
            var lista = _atendimentoServices.ListarAtendimentoUltimaReferencia(referencia, tamanho);
            var viewModelList = _mapper.Map<IEnumerable<AtendimentoViewModel>>(lista);

            if (!viewModelList.Any())
            {
                return NoContent(); // Retorna 204 No Content se não houver atendimentos
            }

            if (referencia < 0 || tamanho < 0) {
                return BadRequest();
            }

            var viewModel = new AtendimentoPaginacaoReferenciaViewModel
            {
                Atendimento = viewModelList,
                PageSize = tamanho,
                Ref = referencia,
                NextRef = viewModelList.Last().id_atendimento
            };

            return Ok(viewModel); // Retorna 200 OK com a lista de atendimentos
        }

        [HttpGet("{id}")]
        public ActionResult<AtendimentoViewModel> Get([FromRoute] int id)
        {
            var model = _atendimentoServices.ObterAtendimentoPorId(id);

            if (model == null)
            {
                return NotFound(); // Retorna 404 Not Found se o atendimento não existir
            }

            var viewModel = _mapper.Map<AtendimentoViewModel>(model);
            return Ok(viewModel); // Retorna 200 OK com o atendimento encontrado
        }

        [HttpPost]
        public ActionResult<AtendimentoViewModel> Post([FromBody] AtendimentoViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest("Dados inválidos."); // Retorna 400 Bad Request se o modelo estiver nulo
            }

            var model = _mapper.Map<AtendimentoModel>(viewModel);
            _atendimentoServices.CriarAtendimento(model);

            return CreatedAtAction(nameof(Get), new { id = model.id_atendimento }, model); // Retorna 201 Created
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] int id, [FromBody] AtendimentoViewModel viewModel)
        {
            if (viewModel == null || viewModel.id_atendimento != id)
            {
                return BadRequest("Dados inválidos."); // Retorna 400 Bad Request se o modelo estiver nulo ou o ID não corresponder
            }

            var existingAtendimento = _atendimentoServices.ObterAtendimentoPorId(id);
            if (existingAtendimento == null)
            {
                return NotFound(); // Retorna 404 Not Found se o atendimento não existir
            }

            var model = _mapper.Map<AtendimentoModel>(viewModel);
            _atendimentoServices.AtualizarAtendimento(model);
            return NoContent(); // Retorna 204 No Content se a atualização for bem-sucedida
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var existingAtendimento = _atendimentoServices.ObterAtendimentoPorId(id);
            if (existingAtendimento == null)
            {
                return NotFound(); // Retorna 404 Not Found se o atendimento não existir
            }

            _atendimentoServices.DeletarAtendimento(id);
            return NoContent(); // Retorna 204 No Content se a exclusão for bem-sucedida
        }
    }
}
