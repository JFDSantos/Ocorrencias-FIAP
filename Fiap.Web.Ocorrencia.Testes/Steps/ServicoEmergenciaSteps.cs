using AutoMapper;
using Fiap.Web.Ocorrencia.Controllers;
using Fiap.Web.Ocorrencia.ViewModel;
using Fiap.Web.Ocorrencias.Models;
using Fiap.Web.Ocorrencias.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using Xunit;

namespace Fiap.Web.Ocorrencia.Testes
{
    [Binding]
    public class ServicoEmergenciaSteps
    {
        private readonly Mock<IServEmergenciaServices> _mockServicoEmergenciaServices;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ServicoEmergenciaController _controller;
        private ActionResult<IEnumerable<ServicoEmergenciaViewModel>> _result;
        private ActionResult<ServicoEmergenciaViewModel> _singleResult;

        public ServicoEmergenciaSteps()
        {
            _mockServicoEmergenciaServices = new Mock<IServEmergenciaServices>();
            _mockMapper = new Mock<IMapper>();
            _controller = new ServicoEmergenciaController(_mockServicoEmergenciaServices.Object, _mockMapper.Object);
        }

        [Given(@"que existem serviços de emergência cadastrados")]
        public void GivenQueExistemServicosDeEmergenciaCadastrados()
        {
            var servicos = new List<ServicoEmergenciaModel>
            {
                new ServicoEmergenciaModel { id_serv_emergencia = 1, descricao = "ABC1234" },
                new ServicoEmergenciaModel { id_serv_emergencia = 2, descricao = "XYZ5678" }
            };

            _mockServicoEmergenciaServices.Setup(s => s.ListarServicoEmergencia()).Returns(servicos);
            _mockMapper.Setup(m => m.Map<IEnumerable<ServicoEmergenciaViewModel>>(It.IsAny<IEnumerable<ServicoEmergenciaModel>>()))
                       .Returns(servicos.Select(s => new ServicoEmergenciaViewModel
                       {
                           id_serv_emergencia = s.id_serv_emergencia,
                           descricao = s.descricao
                       }));
        }

        [Given(@"que não existem serviços de emergência cadastrados")]
        public void GivenQueNaoExistemServicosDeEmergenciaCadastrados()
        {
            _mockServicoEmergenciaServices.Setup(s => s.ListarServicoEmergencia()).Returns(new List<ServicoEmergenciaModel>());
        }

        [Given(@"que o ID do serviço de emergência é inválido")]
        public void GivenQueOIdDoServicoDeEmergenciaEInvalido()
        {
            _mockServicoEmergenciaServices.Setup(s => s.ObterListarServicoEmergenciaPorId(It.IsAny<int>())).Returns((ServicoEmergenciaModel)null);
        }

        [When(@"eu faço uma solicitação de serviços de emergência GET para ""(.*)""")]
        public void WhenEuFacoUmaSolicitacaoDeServicosDeEmergenciaGETPara(string endpoint)
        {
            _result = _controller.Get();
        }

        [When(@"eu faço uma solicitação de serviços de emergência GET para ""(.*)"" com o ID inválido")]
        public void WhenEuFacoUmaSolicitacaoDeServicosDeEmergenciaGETParaComIdInvalido(string endpoint)
        {
            _singleResult = _controller.Get(-1); // ID inválido
        }

        [Then(@"o status da resposta de serviços de emergência deve ser (.*)")]
        public void ThenOStatusDaRespostaDeServicosDeEmergenciaDeveSer(int statusCode)
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
            else if (_singleResult != null && _singleResult.Result != null)
            {
                switch (statusCode)
                {
                    case 200:
                        var okResult = Assert.IsType<OkObjectResult>(_singleResult.Result);
                        Assert.Equal(statusCode, okResult.StatusCode);
                        break;

                    case 204:
                        var noContentResult = Assert.IsType<NoContentResult>(_singleResult.Result);
                        Assert.Equal(statusCode, noContentResult.StatusCode);
                        break;

                    case 400:
                        var badRequestResult = Assert.IsType<BadRequestObjectResult>(_singleResult.Result);
                        Assert.Equal(statusCode, badRequestResult.StatusCode);
                        Assert.NotNull(badRequestResult.Value);
                        break;

                    default:
                        Assert.Fail($"Tipo de status inesperado: {statusCode}");
                        break;
                }
            }
        }

        [Then(@"o corpo da resposta de serviços de emergência deve conter uma lista de serviços de emergência")]
        public void ThenOCorpoDaRespostaDeServicosDeEmergenciaDeveConterUmaListaDeServicosDeEmergencia()
        {
            var okResult = Assert.IsType<OkObjectResult>(_result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<ServicoEmergenciaViewModel>>(okResult.Value);
            Assert.True(model.Any());
        }

        [Then(@"a lista de serviços de emergência deve conter duas entradas")]
        public void ThenAListaDeServicosDeEmergenciaDeveConterDuasEntradas()
        {
            var okResult = Assert.IsType<OkObjectResult>(_result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<ServicoEmergenciaViewModel>>(okResult.Value);
            Assert.Equal(2, model.Count());
        }

        [Then(@"o corpo da resposta de serviços de emergência deve estar vazio")]
        public void ThenOCorpoDaRespostaDeServicosDeEmergenciaDeveEstarVazio()
        {
            Assert.IsType<NoContentResult>(_result.Result);
        }

        [Then(@"o corpo da resposta de serviços de emergência deve conter uma mensagem de erro")]
        public void ThenOCorpoDaRespostaDeServicosDeEmergenciaDeveConterUmaMensagemDeErro()
        {
            var BadFoundResult = Assert.IsType<BadRequestObjectResult>(_singleResult.Result);
            Assert.NotNull(BadFoundResult.Value);
        }
        [Then(@"o contrato da resposta de serviços de emergência deve estar em conformidade com o JSON Schema ""(.*)""")]
        public void ThenOContratoDaRespostaDeveEstarEmConformidadeComOJsonSchema(string schemaFileName)
        {
            var okResult = Assert.IsType<OkObjectResult>(_result.Result);
            var jsonResponse = JsonConvert.SerializeObject(okResult.Value);

            // Definir o caminho completo do schema
            var schemaPath = Path.Combine(@"C:\Users\jeferson.ferreira\Documents\Projects\Ocorrencias-FIAP\Fiap.Web.Ocorrencia.Testes\Schemas\gravidade-schema.json", schemaFileName);
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
