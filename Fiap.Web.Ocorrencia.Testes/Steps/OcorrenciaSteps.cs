using System.Collections.Generic;
using System.Linq;
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

namespace Fiap.Web.Ocorrencias.Tests
{
    [Binding]
    public class OcorrenciaSteps
    {
        private readonly Mock<IOcorrenciaServices> _mockOcorrenciaServices;
        private readonly Mock<IMapper> _mockMapper;
        private readonly OcorrenciaController _controller;
        private ActionResult<IEnumerable<OcorrenciaPaginacaoReferenciaViewModel>> _result;
        private readonly string _schemaPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"../../../Schemas");
        public OcorrenciaSteps()
        {
            _mockOcorrenciaServices = new Mock<IOcorrenciaServices>();
            _mockMapper = new Mock<IMapper>();
            _controller = new OcorrenciaController(_mockOcorrenciaServices.Object, _mockMapper.Object);
        }

        [Given(@"que existem ocorrências cadastradas")]
        public void GivenQueExistemOcorrenciasCadastradas()
        {
            var ocorrencias = new List<OcorrenciaModel>
            {
                new OcorrenciaModel { id_ocorrencia = 1, data_hora = DateTime.Now, descricao = "Ocorrência 1", id_loc = 1, id_gravidade = 1, id_atendimento = 1 },
                new OcorrenciaModel { id_ocorrencia = 2, data_hora = DateTime.Now, descricao = "Ocorrência 2", id_loc = 1, id_gravidade = 1, id_atendimento = 1 }
            };

            _mockOcorrenciaServices.Setup(s => s.ListarOcorrenciaUltimaReferencia(It.IsAny<int>(), It.IsAny<int>()))
                                   .Returns(ocorrencias);

            _mockMapper.Setup(m => m.Map<IEnumerable<OcorrenciaViewModel>>(It.IsAny<IEnumerable<OcorrenciaModel>>())).Returns((IEnumerable<OcorrenciaModel> source) =>
                source.Select(o => new OcorrenciaViewModel
                {
                    id_ocorrencia = o.id_ocorrencia,
                    data_hora = o.data_hora,
                    descricao = o.descricao,
                    id_loc = o.id_loc,
                    id_gravidade = o.id_gravidade,
                    id_atendimento = o.id_atendimento,
                    Localizacao = o.Localizacao != null ? new LocalizacaoViewModel() : null,
                    Gravidade = o.Gravidade != null ? new GravidadeViewModel() : null
                }).ToList());
        }

        [Given(@"que não existem ocorrências cadastradas")]
        public void GivenQueNaoExistemOcorrenciasCadastradas()
        {
            _mockOcorrenciaServices.Setup(s => s.ListarOcorrenciaUltimaReferencia(It.IsAny<int>(), It.IsAny<int>()))
                                   .Returns(new List<OcorrenciaModel>());
        }

        [When(@"eu faço uma solicitação de ocorrências GET para ""(.*)"" com referência (.*) e tamanho (.*)")]
        public void WhenEuFacoUmaSolicitacaoDeOcorrenciasGETParaComReferenciaETamanho(string endpoint, int referencia, int tamanho)
        {
            _result = _controller.Get(referencia, tamanho);
        }

        [Then(@"o status da resposta de ocorrências deve ser (.*)")]
        public void ThenOStatusDaRespostaDeOcorrenciasDeveSer(int statusCode)
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

        [Then(@"o corpo da resposta de ocorrências deve conter uma lista de ocorrências")]
        public void ThenOCorpoDaRespostaDeOcorrenciasDeveConterListaDeOcorrencias()
        {
            var okResult = Assert.IsType<OkObjectResult>(_result.Result);
            var viewModel = Assert.IsType<OcorrenciaPaginacaoReferenciaViewModel>(okResult.Value);
            Assert.True(viewModel.Ocorrencia.Any());
        }

        [Then(@"o corpo da resposta de ocorrências deve conter duas ocorrências")]
        public void ThenOCorpoDaRespostaDeOcorrenciasDeveConterDuasOcorrencias()
        {
            var okResult = Assert.IsType<OkObjectResult>(_result.Result);
            var viewModel = Assert.IsType<OcorrenciaPaginacaoReferenciaViewModel>(okResult.Value);
            Assert.Equal(2, viewModel.Ocorrencia.Count());
        }

        [Then(@"o corpo da resposta de ocorrências deve estar vazio")]
        public void ThenOCorpoDaRespostaDeOcorrenciasDeveEstarVazio()
        {
            var noContentResult = Assert.IsType<NoContentResult>(_result.Result);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Then(@"o corpo da resposta de ocorrências deve conter uma mensagem de erro")]
        public void ThenOCorpoDaRespostaDeOcorrenciasDeveConterMensagemDeErro()
        {
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(_result.Result);
            Assert.NotNull(badRequestResult.Value);
        }
        [Then(@"o contrato da resposta de ocorrências deve estar em conformidade com o JSON Schema ""(.*)""")]
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
