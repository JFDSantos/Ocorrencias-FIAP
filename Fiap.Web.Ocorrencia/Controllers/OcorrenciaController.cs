using AutoMapper;
using Fiap.Web.Ocorrencia.ViewModel;
using Fiap.Web.Ocorrencias.Models;
using Fiap.Web.Ocorrencias.Services;
using Fiap.Web.Ocorrencias.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Web.Ocorrencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OcorrenciaController : ControllerBase
    {
        private readonly IOcorrenciaServices _ocorrenciaServices;
        private readonly IMapper _mapper;

        public OcorrenciaController(IOcorrenciaServices ocorrenciaServices, IMapper mapper)
        {
            _ocorrenciaServices = ocorrenciaServices;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "gerente")]
        public ActionResult<IEnumerable<OcorrenciaPaginacaoReferenciaViewModel>> Get([FromQuery] int referencia = 0, [FromQuery] int tamanho = 10)
        {

            var lista = _ocorrenciaServices.ListarOcorrenciaUltimaReferencia(referencia,tamanho);
            var viewModelList = _mapper.Map<IEnumerable<OcorrenciaViewModel>>(lista);

            if (viewModelList.Count() > 0)
            {
                var viewModel = new OcorrenciaPaginacaoReferenciaViewModel
                {
                    Ocorrencia = viewModelList,
                    PageSize = tamanho,
                    Ref = referencia,
                    NextRef = viewModelList.Last().id_ocorrencia
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
        //public ActionResult<IEnumerable<OcorrenciaViewModel>> Get()
        //{

        //    var lista = _ocorrenciaServices.ListarOcorrencia();
        //    var viewModelList = _mapper.Map<IEnumerable<OcorrenciaViewModel>>(lista);

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
        public ActionResult<OcorrenciaViewModel> Get([FromRoute]int id)
        {

            var model = _ocorrenciaServices.ObterOcorrenciaPorId(id);

            if (model == null)
            {
                return NotFound();
            }
            else
            {
                var viewModel = _mapper.Map<OcorrenciaViewModel>(model);
                return Ok(viewModel);
            }

        }

        [HttpPost]
        public ActionResult Post([FromBody]OcorrenciaViewModel viewModel)
        {
            var model = _mapper.Map<OcorrenciaModel>(viewModel);
            _ocorrenciaServices.CriarOcorrencia(model); 
            return CreatedAtAction(nameof(Get),new {id = model.id_ocorrencia}, model);
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] int id, [FromBody] OcorrenciaViewModel viewModel)
        {

            if(viewModel.id_ocorrencia == id)
            {
                var model = _mapper.Map<OcorrenciaModel>(viewModel);
                _ocorrenciaServices.AtualizarOcorrencia(model);
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id) {
            _ocorrenciaServices.DeletarOcorrencia(id);
            return NoContent();
        }
    }
}
