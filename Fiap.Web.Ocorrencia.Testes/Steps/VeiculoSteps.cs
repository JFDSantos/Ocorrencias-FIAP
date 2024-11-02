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
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using Xunit;

namespace Fiap.Web.Ocorrencia.Testes
{
    [Binding]
    public class VeiculosSteps
    {
        private readonly Mock<IVeiculoServices> _mockVeiculoServices;
        private readonly Mock<IMapper> _mockMapper;
        private readonly VeiculosController _controller;
        private ActionResult<IEnumerable<VeiculoViewModel>> _result;
        private ActionResult<VeiculoViewModel> _singleResult;
        private readonly string _schemaPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"../../../Schemas");
        public VeiculosSteps()
        {
            _mockVeiculoServices = new Mock<IVeiculoServices>();
            _mockMapper = new Mock<IMapper>();
            _controller = new VeiculosController(_mockVeiculoServices.Object, _mockMapper.Object);
        }

        [Given(@"que existem veículos cadastrados")]
        public void GivenQueExistemVeiculosCadastrados()
        {
            var veiculos = new List<VeiculoModel>
            {
                new VeiculoModel { id_veic = 1, placa = "ABC1234" },
                new VeiculoModel { id_veic = 2, placa = "XYZ5678" }
            };

            _mockVeiculoServices.Setup(repo => repo.ListarVeiculos()).Returns(veiculos);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<VeiculoViewModel>>(veiculos))
                       .Returns(veiculos.Select(v => new VeiculoViewModel
                       {
                           id_veic = v.id_veic,
                           placa = v.placa
                       }));
        }

        [Given(@"que não existem veículos cadastrados")]
        public void GivenQueNaoExistemVeiculosCadastrados()
        {
            _mockVeiculoServices.Setup(repo => repo.ListarVeiculos()).Returns(new List<VeiculoModel>());
        }

        [Given(@"que o ID do veículo é inválido")]
        public void GivenQueOIdDoVeiculoEInvalido()
        {
            // Configuração não necessária para esse passo, pois ele será tratado no cenário.
        }

        [When(@"eu faço uma solicitação de veículos GET para ""(.*)""")]
        public void WhenEuFacoUmaSolicitacaoDeVeiculosGETPara(string endpoint)
        {
            _result = _controller.Get();
        }

        [When(@"eu faço uma solicitação de veículos GET para ""(.*)"" com o ID inválido")]
        public void WhenEuFacoUmaSolicitacaoDeVeiculosGETParaComIdInvalido(string endpoint)
        {
            _singleResult = _controller.Get(-1); // ID inválido
        }

        [Then(@"o status da resposta de veículos deve ser (.*)")]
        public void ThenOStatusDaRespostaDeVeiculosDeveSer(int statusCode)
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

        [Then(@"o corpo da resposta de veículos deve conter uma lista de veículos")]
        public void ThenOCorpoDaRespostaDeVeiculosDeveConterUmaListaDeVeiculos()
        {
            var okResult = Assert.IsType<OkObjectResult>(_result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<VeiculoViewModel>>(okResult.Value);
            Assert.True(model.Any());
        }

        [Then(@"a lista de veículos deve conter duas entradas")]
        public void ThenAListaDeVeiculosDeveConterDuasEntradas()
        {
            var okResult = Assert.IsType<OkObjectResult>(_result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<VeiculoViewModel>>(okResult.Value);
            Assert.Equal(2, model.Count());
        }

        [Then(@"o corpo da resposta de veículos deve estar vazio")]
        public void ThenOCorpoDaRespostaDeVeiculosDeveEstarVazio()
        {
            var noContentResult = Assert.IsType<NoContentResult>(_result.Result);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Then(@"o corpo da resposta de veículos deve conter uma mensagem de erro")]
        public void ThenOCorpoDaRespostaDeVeiculosDeveConterUmaMensagemDeErro()
        {
            var BadResult = Assert.IsType<BadRequestObjectResult>(_singleResult.Result);
            Assert.NotNull(BadResult.Value);
            Assert.Equal(400, BadResult.StatusCode);
        }

        [Then(@"o contrato da resposta de veículos deve estar em conformidade com o JSON Schema ""(.*)""")]
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
