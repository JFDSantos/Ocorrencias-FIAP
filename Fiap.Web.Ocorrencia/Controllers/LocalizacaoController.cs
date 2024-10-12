using AutoMapper;
using Fiap.Web.Ocorrencia.ViewModel;
using Fiap.Web.Ocorrencias.Models;
using Fiap.Web.Ocorrencias.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

            if (viewModelList == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(viewModelList);
            }

        }

        [HttpGet("{id}")]
        public ActionResult<LocalizacaoViewModel> Get([FromRoute] int id)
        {

            var model = _localizacaoServices.ObterLocalizacaoPorId(id);

            if (model == null)
            {
                return NotFound();
            }
            else
            {
                var viewModel = _mapper.Map<LocalizacaoViewModel>(model);
                return Ok(viewModel);
            }

        }

        [HttpPost]
        public ActionResult Post([FromBody] LocalizacaoViewModel viewModel)
        {
            var model = _mapper.Map<LocalizacaoModel>(viewModel);
            _localizacaoServices.CriarLocalizacao(model);
            return CreatedAtAction(nameof(Get), new { id = model.id_loc }, model);
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] int id, [FromBody] LocalizacaoViewModel viewModel)
        {

            if (viewModel.id_loc == id)
            {
                var model = _mapper.Map<LocalizacaoModel>(viewModel);
                _localizacaoServices.AtualizarLocalizacao(model);
                return NoContent();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "gerente")]
        public ActionResult Delete([FromRoute] int id)
        {
            _localizacaoServices.DeletarLocalizacao(id);
            return NoContent();
        }
    }
}
