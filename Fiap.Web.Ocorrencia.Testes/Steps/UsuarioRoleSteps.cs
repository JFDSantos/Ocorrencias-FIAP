using AutoMapper;
using Fiap.Web.Ocorrencia.Controllers;
using Fiap.Web.Ocorrencia.Services;
using Fiap.Web.Ocorrencia.ViewModel;
using Fiap.Web.Ocorrencias.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using Xunit;

namespace Fiap.Web.Ocorrencia.Testes
{
    [Binding]
    public class UsuarioRoleSteps
    {
        private readonly Mock<IUsuarioRoleServices> _mockUsuarioRoleServices;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UsuarioRoleController _controller;
        private ActionResult<IEnumerable<UsuarioRoleViewModel>> _result;
        private readonly string _schemaPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"../../../Schemas");
        public UsuarioRoleSteps()
        {
            _mockUsuarioRoleServices = new Mock<IUsuarioRoleServices>();
            _mockMapper = new Mock<IMapper>();
            _controller = new UsuarioRoleController(_mockUsuarioRoleServices.Object, _mockMapper.Object);
        }

        [Given(@"que existem roles cadastradas")]
        public void GivenQueExistemRolesCadastradas()
        {
            var roles = new List<UsuarioRoleModel>
            {
                new UsuarioRoleModel { id_role = 1, role = "Admin" },
                new UsuarioRoleModel { id_role = 2, role = "User" }
            };

            _mockUsuarioRoleServices.Setup(s => s.ListarRoles()).Returns(roles);
            _mockMapper.Setup(m => m.Map<IEnumerable<UsuarioRoleViewModel>>(It.IsAny<IEnumerable<UsuarioRoleModel>>()))
                       .Returns(roles.Select(r => new UsuarioRoleViewModel { id_role = r.id_role, role = r.role }));
        }

        [Given(@"que não existem roles cadastradas")]
        public void GivenQueNaoExistemRolesCadastradas()
        {
            _mockUsuarioRoleServices.Setup(s => s.ListarRoles()).Returns(new List<UsuarioRoleModel>());
        }

        [When(@"eu faço uma solicitação de roles GET para ""(.*)""")]
        public void WhenEuFacoUmaSolicitacaoDeRolesGETPara(string endpoint)
        {
            _result = _controller.Get();
        }

        [Then(@"o status da resposta de roles deve ser (.*)")]
        public void ThenOStatusDaRespostaDeRolesDeveSer(int statusCode)
        {
            if (_result != null && _result.Result != null)
            {
                switch (statusCode)
                {
                    case 200:
                        var okResult = Assert.IsType<OkObjectResult>(_result.Result);
                        Assert.Equal(statusCode, okResult.StatusCode);
                        break;

                    case 204:
                        var noContentResult = Assert.IsType<NoContentResult>(_result.Result);
                        Assert.Equal(statusCode, noContentResult.StatusCode);
                        break;

                    case 400:
                        var badRequestResult = Assert.IsType<BadRequestObjectResult>(_result.Result);
                        Assert.Equal(statusCode, badRequestResult.StatusCode);
                        Assert.NotNull(badRequestResult.Value);
                        break;

                    default:
                        Assert.Fail($"Tipo de status inesperado: {statusCode}");
                        break;
                }
            }
        }

        [Then(@"o corpo da resposta de roles deve conter uma lista de roles")]
        public void ThenOCorpoDaRespostaDeRolesDeveConterUmaListaDeRoles()
        {
            var okResult = Assert.IsType<OkObjectResult>(_result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<UsuarioRoleViewModel>>(okResult.Value);
            Assert.True(model.Any());
        }

        [Then(@"a lista de roles deve conter duas entradas")]
        public void ThenAListaDeRolesDeveConterDuasEntradas()
        {
            var okResult = Assert.IsType<OkObjectResult>(_result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<UsuarioRoleViewModel>>(okResult.Value);
            Assert.Equal(2, model.Count());
        }

        [Then(@"o corpo da resposta de roles deve estar vazio")]
        public void ThenOCorpoDaRespostaDeRolesDeveEstarVazio()
        {
            var noContentResult = Assert.IsType<NoContentResult>(_result.Result);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Then(@"o contrato da resposta de roles deve estar em conformidade com o JSON Schema ""(.*)""")]
        public void ThenOContratoDaRespostaDeveEstarEmConformidadeComOJsonSchema(string schemaFileName)
        {
            var okResult = Assert.IsType<OkObjectResult>(_result.Result);
            var jsonResponse = JsonConvert.SerializeObject(okResult.Value);

            // Definir o caminho completo do schema
            var schemaPath = Path.Combine(_schemaPath, schemaFileName);
            Console.WriteLine($"Schema Path: {schemaPath}");

            // Carregar o JSON Schema
            var schemaJson = System.IO.File.ReadAllText(schemaPath);
            var schema = JSchema.Parse(schemaJson);

            // Validar o JSON Response com o JSON Schema
            var json = JToken.Parse(jsonResponse);
            Assert.True(json.IsValid(schema), "O JSON de resposta não está em conformidade com o JSON Schema.");
        }
    }
}
