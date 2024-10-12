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
    [Authorize(Roles = "gerente")]
    public class VeiculosController : ControllerBase
    {
        private readonly IVeiculoServices _veiculoServices;
        private readonly IMapper _mapper;

        public VeiculosController(IVeiculoServices veiculoServices, IMapper mapper)
        {
            _veiculoServices = veiculoServices;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VeiculoViewModel>> Get()
        {

            var lista = _veiculoServices.ListarVeiculos();
            var viewModelList = _mapper.Map<IEnumerable<VeiculoViewModel>>(lista);

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
        public ActionResult<VeiculoViewModel> Get([FromRoute] int id)
        {

            var model = _veiculoServices.ObterVeiculosPorId(id);

            if (model == null)
            {
                return NotFound();
            }
            else
            {
                var viewModel = _mapper.Map<VeiculoViewModel>(model);
                return Ok(viewModel);
            }

        }

        [HttpPost]
        public ActionResult Post([FromBody] VeiculoViewModel viewModel)
        {
            var model = _mapper.Map<VeiculoModel>(viewModel);
            _veiculoServices.CriarVeiculos(model);
            return CreatedAtAction(nameof(Get), new { id = model.id_veic }, model);
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] int id, [FromBody] VeiculoViewModel viewModel)
        {
            if (viewModel.id_veic != id)
            {
                return BadRequest();
            }

            var existingVeiculo = _veiculoServices.ObterVeiculosPorId(id);
            if (existingVeiculo == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<VeiculoModel>(viewModel);

            _veiculoServices.AtualizarVeiculos(model);

            return NoContent();

        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _veiculoServices.DeletarVeiculos(id);
            return NoContent();
        }
    }
}
