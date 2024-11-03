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
    public class GravidadeController : ControllerBase
    {
        private readonly IGravidadeServices _gravidadeServices;
        private readonly IMapper _mapper;

        public GravidadeController(IGravidadeServices gravidadeServices, IMapper mapper)
        {
            _gravidadeServices = gravidadeServices;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<GravidadePaginacaoReferenciaViewModel>> Get([FromQuery] int referencia = 0, [FromQuery] int tamanho = 10)
        {

            var lista = _gravidadeServices.ListarGravidadeUltimaReferencia(referencia, tamanho);

            if (lista == null || !lista.Any())
            {
                return NoContent();
            }

            if (referencia < 0 || tamanho < 0)
            {
                return BadRequest("Parâmetros inválidos.");
            }

            var viewModelList = _mapper.Map<IEnumerable<GravidadeViewModel>>(lista);

            var viewModel = new GravidadePaginacaoReferenciaViewModel
            {
                Gravidade = viewModelList,
                PageSize = tamanho,
                Ref = referencia,
                NextRef = viewModelList.Last().id_gravidade
            };

            return Ok(viewModel);

        }

        //[HttpGet]
        //public ActionResult<IEnumerable<GravidadeViewModel>> Get()
        //{

        //    var lista = _gravidadeServices.ListarGravidade();
        //    var viewModelList = _mapper.Map<IEnumerable<GravidadeViewModel>>(lista);

        //    if (viewModelList == null)
        //    {
        //        return NoContent();
        //    }
        //    else
        //    {
        //        return Ok(viewModelList);
        //    }

        //}

        [HttpGet("{id}")]
        public ActionResult<GravidadeViewModel> Get([FromRoute] int id)
        {

            var model = _gravidadeServices.ObterGravidadePorId(id);

            if (model == null)
            {
                return NotFound();
            }
            else
            {
                var viewModel = _mapper.Map<GravidadeViewModel>(model);
                return Ok(viewModel);
            }

        }

        [HttpPost]
        [Authorize(Roles = "gerente")]
        public ActionResult Post([FromBody] GravidadeViewModel viewModel)
        {
            var model = _mapper.Map<GravidadeModel>(viewModel);
            _gravidadeServices.CriarGravidade(model);
            return CreatedAtAction(nameof(Get), new { id = model.id_gravidade }, model);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "gerente")]
        public ActionResult Put([FromRoute] int id, [FromBody] GravidadeViewModel viewModel)
        {

            if (viewModel.id_gravidade == id)
            {
                var model = _mapper.Map<GravidadeModel>(viewModel);
                _gravidadeServices.AtualizarGravidade(model);
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
            _gravidadeServices.DeletarGravidade(id);
            return NoContent();
        }
    }
}
