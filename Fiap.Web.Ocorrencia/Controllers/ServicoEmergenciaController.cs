using AutoMapper;
using Fiap.Web.Ocorrencia.ViewModel;
using Fiap.Web.Ocorrencias.Models;
using Fiap.Web.Ocorrencias.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Web.Ocorrencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "gerente")]
    public class ServicoEmergenciaController : ControllerBase
    {
        private readonly IServEmergenciaServices _servEmergenciaServices;
        private readonly IMapper _mapper;

        public ServicoEmergenciaController(IServEmergenciaServices servEmergenciaServices, IMapper mapper)
        {
            _servEmergenciaServices = servEmergenciaServices;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ServicoEmergenciaViewModel>> Get()
        {

            var lista = _servEmergenciaServices.ListarServicoEmergencia();
            var viewModelList = _mapper.Map<IEnumerable<ServicoEmergenciaViewModel>>(lista);

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
        public ActionResult<ServicoEmergenciaViewModel> Get([FromRoute] int id)
        {

            var model = _servEmergenciaServices.ObterListarServicoEmergenciaPorId(id);

            if (model == null)
            {
                return NotFound();
            }
            else
            {
                var viewModel = _mapper.Map<ServicoEmergenciaViewModel>(model);
                return Ok(viewModel);
            }

        }

        [HttpPost]
        public ActionResult Post([FromBody] ServicoEmergenciaViewModel viewModel)
        {
            var model = _mapper.Map<ServicoEmergenciaModel>(viewModel);
            _servEmergenciaServices.CriarServicoEmergencia(model);
            return CreatedAtAction(nameof(Get), new { id = model.id_serv_emergencia }, model);
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] int id, [FromBody] ServicoEmergenciaViewModel viewModel)
        {
            var exstisServ = _servEmergenciaServices.ObterListarServicoEmergenciaPorId(id);

            try
            {
                if (exstisServ == null)
                {
                    return NotFound();
                }

                if (viewModel.id_serv_emergencia == id)
                {
                    var model = _mapper.Map<ServicoEmergenciaModel>(viewModel);
                    _servEmergenciaServices.AtualizarServicoEmergencia(model);
                    return NoContent();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                return new JsonResult(innerException);
            }


        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _servEmergenciaServices.DeletarServicoEmergencia(id);
            return NoContent();
        }
    }
}
