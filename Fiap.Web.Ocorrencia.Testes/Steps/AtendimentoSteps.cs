using AutoMapper;
using Fiap.Web.Ocorrencia.Controllers;
using Fiap.Web.Ocorrencia.Services;
using Fiap.Web.Ocorrencia.ViewModel;
using Fiap.Web.Ocorrencias.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using Xunit;

namespace Fiap.Web.Ocorrencias.Tests
{
    [Binding]
    public class AtendimentoSteps
    {
        private readonly Mock<IAtendimentoServices> _mockAtendimentoService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AtendimentoController _atendimentoController;
        private ActionResult<AtendimentoPaginacaoReferenciaViewModel> _result; // Corrigido para o tipo correto

        public AtendimentoSteps()
        {
            _mockAtendimentoService = new Mock<IAtendimentoServices>();
            _mockMapper = new Mock<IMapper>();
            _atendimentoController = new AtendimentoController(_mockAtendimentoService.Object, _mockMapper.Object);
        }

        [Given(@"que existem atendimentos cadastrados")]
        public void GivenQueExistemAtendimentosCadastrados()
        {
            var atendimentos = new List<AtendimentoModel>
            {
                new AtendimentoModel { id_atendimento = 1, descricao = "Atendimento 1" },
                new AtendimentoModel { id_atendimento = 2, descricao = "Atendimento 2" }
            };

            // Configura o mock para retornar os atendimentos simulados
            _mockAtendimentoService.Setup(s => s.ListarAtendimentoUltimaReferencia(It.IsAny<int>(), It.IsAny<int>()))
                                   .Returns(atendimentos);

            // Configura o mapeamento do AutoMapper
            _mockMapper.Setup(m => m.Map<IEnumerable<AtendimentoViewModel>>(It.IsAny<IEnumerable<AtendimentoModel>>()))
                       .Returns(atendimentos.Select(a => new AtendimentoViewModel { id_atendimento = a.id_atendimento, descricao = a.descricao }));
        }

        [Given(@"que não existem atendimentos cadastrados")]
        public void GivenQueNaoExistemAtendimentosCadastrados()
        {
            _mockAtendimentoService.Setup(s => s.ListarAtendimentoUltimaReferencia(It.IsAny<int>(), It.IsAny<int>()))
                                   .Returns(new List<AtendimentoModel>());
        }

        [When(@"eu solicito a lista de atendimentos")]
        public void WhenEuSolicitoAListaDeAtendimentos()
        {
            _result = _atendimentoController.Get(referencia: 0, tamanho: 10);
        }

        [When(@"eu solicito a lista de atendimentos com referência (.*) e tamanho (.*)")]
        public void WhenEuSolicitoAListaDeAtendimentosComReferenciaETamanho(int referencia, int tamanho)
        {
            _result = _atendimentoController.Get(referencia: referencia, tamanho: tamanho);
        }

        [Then(@"eu recebo uma resposta de sucesso com a lista de atendimentos")]
        public void ThenEuReceboUmaRespostaDeSucessoComAListaDeAtendimentos()
        {
            var okResult = Assert.IsType<OkObjectResult>(_result.Result);
            var viewModel = Assert.IsType<AtendimentoPaginacaoReferenciaViewModel>(okResult.Value);
            Assert.Equal(2, viewModel.Atendimento.Count()); // Verifica se há dois atendimentos retornados
            Assert.Equal(200, okResult.StatusCode); // Verifica se o status code é 200 OK
        }

        [Then(@"eu recebo uma resposta de não encontrado")]
        public void ThenEuReceboUmaRespostaDeNaoEncontrado()
        {
            var notFoundResult = Assert.IsType<NoContentResult>(_result.Result);
            Assert.Equal(204, notFoundResult.StatusCode); // Verifica se o status code é 204 No Content
        }

        [Then(@"eu recebo uma resposta de erro")]
        public void ThenEuReceboUmaRespostaDeErro()
        {
            var badRequestResult = Assert.IsType<BadRequestResult>(_result.Result);
            Assert.Equal(400, badRequestResult.StatusCode); // Verifica se o status code é 400 Bad Request
        }
    }
}
