using AutoMapper;
using Fiap.Web.Ocorrencia.Services;
using Fiap.Web.Ocorrencia.ViewModel;
using Fiap.Web.Ocorrencias.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Web.Ocorrencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioRoleController : ControllerBase
    {
        private readonly IUsuarioRoleServices _usuariosServices;
        private readonly IMapper _mapper;

        public UsuarioRoleController(IUsuarioRoleServices usuariosServices, IMapper mapper)
        {
            _usuariosServices = usuariosServices;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UsuarioRoleViewModel>> Get()
        {

            var lista = _usuariosServices.ListarRoles();
            var viewModelList = _mapper.Map<IEnumerable<UsuarioRoleViewModel>>(lista);

            if (viewModelList == null || !viewModelList.Any())
            {
                return NoContent();
            }
            else
            {
                return Ok(viewModelList);
            }

        }

        [HttpGet("{id}")]
        public ActionResult<UsuarioRoleViewModel> Get([FromRoute] int id)
        {

            var model = _usuariosServices.ObterRolesPorId(id);

            if (model == null)
            {
                return NotFound();
            }
            else
            {
                var viewModel = _mapper.Map<UsuarioRoleViewModel>(model);
                return Ok(viewModel);
            }

        }

        [HttpPost]
        [Authorize(Roles = "gerente")]
        public ActionResult Post([FromBody] UsuarioRoleViewModel viewModel)
        {
            var model = _mapper.Map<UsuarioRoleModel>(viewModel);
            _usuariosServices.CriarRoles(model);
            return CreatedAtAction(nameof(Get), new { id = model.id_role }, model);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "gerente")]
        public ActionResult Put([FromRoute] int id, [FromBody] UsuarioRoleViewModel viewModel)
        {

            if (viewModel.id_role == id)
            {
                var model = _mapper.Map<UsuarioRoleModel>(viewModel);
                _usuariosServices.AtualizarRoles(model);
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
            _usuariosServices.DeletarRoles(id);
            return NoContent();
        }

    }
}