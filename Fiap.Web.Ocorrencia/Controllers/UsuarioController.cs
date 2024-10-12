using AutoMapper;
using Fiap.Web.Ocorrencia.Services;
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
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServices _usuariosServices;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioServices usuariosServices, IMapper mapper)
        {
            _usuariosServices = usuariosServices;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "gerente")]
        public ActionResult<IEnumerable<UsuarioViewModel>> Get()
        {

            var lista = _usuariosServices.ListarUsuarios();
            var viewModelList = _mapper.Map<IEnumerable<UsuarioViewModel>>(lista);

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
        public ActionResult<UsuarioViewModel> Get([FromRoute] int id)
        {

            var model = _usuariosServices.ObterUsuarioPorId(id);

            if (model == null)
            {
                return NotFound();
            }
            else
            {
                var viewModel = _mapper.Map<UsuarioViewModel>(model);
                return Ok(viewModel);
            }

        }

        [HttpPost]
        public ActionResult Post([FromBody] UsuarioViewModel viewModel)
        {
            var model = _mapper.Map<UsuarioModel>(viewModel);
            _usuariosServices.CriarUsuario(model);
            return CreatedAtAction(nameof(Get), new { id = model.id_usuario }, model);
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] int id, [FromBody] UsuarioViewModel viewModel)
        {

            if (viewModel.id_usuario == id)
            {
                var model = _mapper.Map<UsuarioModel>(viewModel);
                _usuariosServices.AtualizarUsuario(model);
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
            _usuariosServices.DeletarUsuario(id);
            return NoContent();
        }
    }
}
