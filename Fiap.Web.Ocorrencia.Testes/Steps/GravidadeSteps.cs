using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Fiap.Web.Ocorrencia.Controllers;
using Fiap.Web.Ocorrencia.ViewModel;
using Fiap.Web.Ocorrencias.Models;
using Fiap.Web.Ocorrencias.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using TechTalk.SpecFlow;
using Xunit;

namespace Fiap.Web.Ocorrencias.Tests
{
    [Binding]
    public class GravidadeSteps
    {
        private readonly Mock<IGravidadeServices> _mockGravidadeServices;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GravidadeController _controller;
        private ActionResult<IEnumerable<GravidadePaginacaoReferenciaViewModel>> _result;
        private readonly string _schemaPath = Path.Combine(Directory.GetCurrentDirectory(), "schemas");


    public GravidadeSteps()
        {
            _mockGravidadeServices = new Mock<IGravidadeServices>();
            _mockMapper = new Mock<IMapper>();
            _controller = new GravidadeController(_mockGravidadeServices.Object, _mockMapper.Object);
        }

        [Given(@"que existem gravidades cadastradas")]
        public void GivenQueExistemGravidadesCadastradas()
        {
            var gravidades = new List<GravidadeModel>
            {
                new GravidadeModel { id_gravidade = 1, descricao = "ABC1234" },
                new GravidadeModel { id_gravidade = 2, descricao = "XYZ5678" }
            };

            _mockGravidadeServices.Setup(s => s.ListarGravidadeUltimaReferencia(It.IsAny<int>(), It.IsAny<int>()))
                                  .Returns(gravidades);

            _mockMapper.Setup(m => m.Map<IEnumerable<GravidadeViewModel>>(It.IsAny<IEnumerable<GravidadeModel>>()))
           .Returns((IEnumerable<GravidadeModel> source) =>
                source.Select(g => new GravidadeViewModel
                {
                    id_gravidade = g.id_gravidade,
                    descricao = g.descricao
                }));
        }

        [Given(@"que não existem gravidades cadastradas")]
        public void GivenQueNaoExistemGravidadesCadastradas()
        {
            _mockGravidadeServices.Setup(s => s.ListarGravidadeUltimaReferencia(It.IsAny<int>(), It.IsAny<int>()))
                                  .Returns(new List<GravidadeModel>());
        }

        [When(@"eu faço uma solicitação GET para ""(.*)""")]
        public void WhenEuFacoUmaSolicitacaoGETPara(string endpoint)
        {
            _result = _controller.Get(referencia: 0, tamanho: 10);

        }

        [When(@"eu faço uma solicitação GET para ""(.*)"" com referência (.*) e tamanho (.*)")]
        public void WhenEuFacoUmaSolicitacaoGETParaComReferenciaETamanho(string endpoint, int referencia, int tamanho)
        {
            _result = _controller.Get(referencia, tamanho);
        }

        [Then(@"o status da resposta deve ser (.*)")]
        public void ThenOStatusDaRespostaParaGravidadeDeveSer(int statusCode)
        {
            Assert.NotNull(_result.Result);

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


        [Then(@"o corpo da resposta deve conter uma lista de gravidades")]
        public void ThenOCorpoDaRespostaDeveConterUmaListaDeGravidades()
        {
            var okResult = Assert.IsType<OkObjectResult>(_result.Result);
            var viewModel = Assert.IsType<GravidadePaginacaoReferenciaViewModel>(okResult.Value);
            Assert.True(viewModel.Gravidade.Any());
        }

        [Then(@"o corpo da resposta deve estar vazio")]
        public void ThenOCorpoDaRespostaDeveEstarVazio()
        {
            var noContentResult = Assert.IsType<NoContentResult>(_result.Result);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Then(@"o corpo da resposta deve conter uma mensagem de erro")]
        public void ThenOCorpoDaRespostaDeveConterUmaMensagemDeErro()
        {
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(_result.Result);
            Assert.NotNull(badRequestResult.Value);
        }

        [Then(@"o contrato da resposta deve estar em conformidade com o JSON Schema ""(.*)""")]
        public void ThenOContratoDaRespostaDeveEstarEmConformidadeComOJsonSchema(string schemaFileName)
        {
            var okResult = Assert.IsType<OkObjectResult>(_result.Result);
            var jsonResponse = JsonConvert.SerializeObject(okResult.Value);

            // Carregar o JSON Schema
            var schemaJson = System.IO.File.ReadAllText(System.IO.Path.Combine(_schemaPath, schemaFileName));
            var schema = JSchema.Parse(schemaJson);

            // Validar o JSON Response com o JSON Schema
            var json = JToken.Parse(jsonResponse);
            Assert.True(json.IsValid(schema), "O JSON de resposta não está em conformidade com o JSON Schema.");
        }
    }
}
