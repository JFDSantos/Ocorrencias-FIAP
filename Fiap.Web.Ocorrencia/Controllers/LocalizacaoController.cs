using AutoMapper;
using Fiap.Web.Ocorrencia.ViewModel;
using Fiap.Web.Ocorrencias.Models;
using Fiap.Web.Ocorrencias.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Fiap.Web.Ocorrencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalizacaoController : ControllerBase
    {
        private readonly ILocalizacaoServices _localizacaoServices;
        private readonly IMapper _mapper;

        public LocalizacaoController(ILocalizacaoServices localizacaoServices, IMapper mapper)
        {
            _localizacaoServices = localizacaoServices;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<LocalizacaoViewModel>> Get()
        {
            var lista = _localizacaoServices.ListarLocalizacao();
            var viewModelList = _mapper.Map<IEnumerable<LocalizacaoViewModel>>(lista);

            if (viewModelList == null || !viewModelList.Any())
            {
                return NoContent(); // Retorna 204 se a lista estiver vazia ou nula
            }

            return Ok(viewModelList); // Retorna 200 com a lista
        }

        [HttpGet("{id}")]
        public ActionResult<LocalizacaoViewModel> Get([FromRoute] int id)
        {
            var model = _localizacaoServices.ObterLocalizacaoPorId(id);

            if (model == null)
            {
                return NotFound(); // Retorna 404 se a localização não for encontrada
            }

            var viewModel = _mapper.Map<LocalizacaoViewModel>(model);
            return Ok(viewModel); // Retorna 200 com o objeto encontrado
        }

        [HttpPost]
        public IActionResult Post([FromBody] LocalizacaoViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest("LocalizacaoViewModel não pode ser nulo.");
            }

            // Validar as propriedades essenciais
            if (string.IsNullOrWhiteSpace(viewModel.endereco) ||
                string.IsNullOrWhiteSpace(viewModel.cidade) ||
                string.IsNullOrWhiteSpace(viewModel.cep))
            {
                return BadRequest("Endereço, cidade e CEP são obrigatórios.");
            }

            var model = _mapper.Map<LocalizacaoModel>(viewModel);

            if (model == null)
            {
                return BadRequest("Falha ao mapear o LocalizacaoViewModel.");
            }

            try
            {
                _localizacaoServices.CriarLocalizacao(model);

                // Valida o ID após a criação
                if (model.id_loc <= 0) // Certifique-se de que o serviço retorna um ID válido
                {
                    return BadRequest("ID da localização não é válido."); // Se o ID não for gerado corretamente
                }

                return CreatedAtAction(nameof(Get), new { id = model.id_loc }, model); // Retorna 201 com a nova localização
            }
            catch (Exception ex)
            {
                // Log de erro pode ser adicionado aqui
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao criar a localização: {ex.Message}");
            }
        }



        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] int id, [FromBody] LocalizacaoViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest("LocalizacaoViewModel não pode ser nulo.");
            }

            if (viewModel.id_loc != id)
            {
                return BadRequest("ID na URL não corresponde ao ID no modelo."); // Verifica se os IDs correspondem
            }

            var model = _mapper.Map<LocalizacaoModel>(viewModel);
            try
            {
                _localizacaoServices.AtualizarLocalizacao(model); // Atualiza a localização
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar a localização: {ex.Message}"); // Tratamento de erro
            }

            return NoContent(); // Retorna 204 em caso de sucesso
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "gerente")]
        public ActionResult Delete([FromRoute] int id)
        {
            try
            {
                _localizacaoServices.DeletarLocalizacao(id); // Tenta deletar a localização
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar a localização: {ex.Message}"); // Tratamento de erro
            }

            return NoContent(); // Retorna 204 em caso de sucesso
        }
    }
}