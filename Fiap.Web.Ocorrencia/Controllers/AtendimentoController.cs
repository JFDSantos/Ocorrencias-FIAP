using AutoMapper;
using Fiap.Web.Ocorrencia.Services;
using Fiap.Web.Ocorrencia.ViewModel;
using Fiap.Web.Ocorrencias.Models;
using Fiap.Web.Ocorrencias.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
        public ActionResult<IEnumerable<AtendimentoPaginacaoReferenciaViewModel>> Get([FromQuery] int referencia = 0, [FromQuery] int tamanho = 10)
        {

            var lista = _atendimentoServices.ListarAtendimentoUltimaReferencia(referencia, tamanho);
            var viewModelList = _mapper.Map<IEnumerable<AtendimentoViewModel>>(lista);

            if(viewModelList.Count() > 0)
            {
                var viewModel = new AtendimentoPaginacaoReferenciaViewModel
                {
                    Atendimento = viewModelList,
                    PageSize = tamanho,
                    Ref = referencia,
                    NextRef = viewModelList.Last().id_atendimento
                };

                return Ok(viewModel);
            }
            else
            {
                return NoContent();
            }

        }

        //[HttpGet]
        //[Authorize(Roles = "gerente")]
        //public ActionResult<IEnumerable<AtendimentoViewModel>> Get()
        //{

        //    var lista = _atendimentoServices.ListarAtendimento();
        //    var viewModelList = _mapper.Map<IEnumerable<AtendimentoViewModel>>(lista);

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
        public ActionResult<AtendimentoViewModel> Get([FromRoute] int id)
        {

            var model = _atendimentoServices.ObterAtendimentoPorId(id);

            if (model == null)
            {
                return NotFound();
            }
            else
            {
                var viewModel = _mapper.Map<AtendimentoViewModel>(model);
                return Ok(viewModel);
            }

        }

        [HttpPost]
        public ActionResult Post([FromBody] AtendimentoViewModel viewModel)
        {
            var model = _mapper.Map<AtendimentoModel>(viewModel);
            _atendimentoServices.CriarAtendimento(model);
            return CreatedAtAction(nameof(Get), new { id = model.id_atendimento }, model);
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] int id, [FromBody] AtendimentoViewModel viewModel)
        {
            var existingAtendimento = _atendimentoServices.ObterAtendimentoPorId(id);

            if (existingAtendimento == null)
            {
                return NotFound();
            }

            if (viewModel.id_atendimento == id)
            {
                var model = _mapper.Map<AtendimentoModel>(viewModel);
                _atendimentoServices.AtualizarAtendimento(model);
                return NoContent();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _atendimentoServices.DeletarAtendimento(id);
            return NoContent();
        }
    }
}
