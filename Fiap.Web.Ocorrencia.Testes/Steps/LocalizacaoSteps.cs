using AutoMapper;
using Fiap.Web.Ocorrencia.Controllers;
using Fiap.Web.Ocorrencia.Services;
using Fiap.Web.Ocorrencia.ViewModel;
using Fiap.Web.Ocorrencias.Models;
using Fiap.Web.Ocorrencias.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechTalk.SpecFlow;
using Xunit;

namespace Fiap.Web.Ocorrencias.Tests
{
    [Binding]
    public class LocalizacaoSteps
    {
        private readonly Mock<ILocalizacaoServices> _mockLocalizacaoServices;
        private readonly Mock<IMapper> _mockMapper;
        private readonly LocalizacaoController _controller;
        private IActionResult _result;

        public LocalizacaoSteps()
        {
            _mockLocalizacaoServices = new Mock<ILocalizacaoServices>();
            _mockMapper = new Mock<IMapper>();
            _controller = new LocalizacaoController(_mockLocalizacaoServices.Object, _mockMapper.Object);
        }

        [Given(@"existe uma lista de localizações")]
        public void GivenExisteUmaListaDeLocalizacoes()
        {
            var localizacoes = new List<LocalizacaoModel>
            {
                new LocalizacaoModel { id_loc = 1, endereco = "Localização 1" },
                new LocalizacaoModel { id_loc = 2, endereco = "Localização 2" }
            };

            _mockLocalizacaoServices.Setup(repo => repo.ListarLocalizacao()).Returns(localizacoes);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<LocalizacaoViewModel>>(localizacoes)).Returns(localizacoes.Select(l => new LocalizacaoViewModel { id_loc = l.id_loc, endereco = l.endereco }));
        }

        [When(@"eu solicito a lista de localizações")]
        public void WhenEuSolicitoAListaDeLocalizacoes()
        {
            _result = _controller.Get().Result;
        }

        [Then(@"eu recebo uma resposta de sucesso com a lista de localizações")]
        public void ThenEuReceboUmaRespostaDeSucessoComAListaDeLocalizacoes()
        {
            var okResult = Assert.IsType<OkObjectResult>(_result);
            var model = Assert.IsAssignableFrom<IEnumerable<LocalizacaoViewModel>>(okResult.Value);
            Assert.True(model.Any());
        }

        [Given(@"uma localização com id (.*) existe")]
        public void GivenUmaLocalizacaoComIdExiste(int id)
        {
            var localizacao = new LocalizacaoModel { id_loc = id, endereco = "Localização " + id };
            _mockLocalizacaoServices.Setup(repo => repo.ObterLocalizacaoPorId(id)).Returns(localizacao);

            var localizacaoViewModel = new LocalizacaoViewModel { id_loc = id, endereco = localizacao.endereco };
            _mockMapper.Setup(mapper => mapper.Map<LocalizacaoViewModel>(localizacao)).Returns(localizacaoViewModel);
        }

        [When(@"eu solicito a localização com id (.*)")]
        public void WhenEuSolicitoALocalizacaoComId(int id)
        {
            _result = _controller.Get(id).Result;
        }

        [Then(@"eu recebo uma resposta de sucesso com a localização")]
        public void ThenEuReceboUmaRespostaDeSucessoComALocalizacao()
        {
            var okResult = Assert.IsType<OkObjectResult>(_result);
            var model = Assert.IsAssignableFrom<LocalizacaoViewModel>(okResult.Value);
            Assert.NotNull(model);
        }

        [Given(@"um novo modelo de localização válido")]
        public void GivenUmNovoModeloDeLocalizacaoValido()
        {
            var localizacaoViewModel = new LocalizacaoViewModel { endereco = "Nova Localização" };
            var localizacaoModel = new LocalizacaoModel { id_loc = 1, endereco = "Nova Localização" };

            _mockMapper.Setup(mapper => mapper.Map<LocalizacaoModel>(localizacaoViewModel)).Returns(localizacaoModel);
            _mockLocalizacaoServices.Setup(repo => repo.CriarLocalizacao(It.IsAny<LocalizacaoModel>())).Verifiable();
        }

        [When(@"eu adiciono a nova localização")]
        public void WhenEuAdicionoANovaLocalizacao()
        {
            var localizacaoViewModel = new LocalizacaoViewModel
            {
                endereco = "Nova Localização",
                cidade = "Cidade Exemplo",
                cep = "00000-000"
            };

            var localizacaoModel = new LocalizacaoModel
            {
                id_loc = 1,
                endereco = localizacaoViewModel.endereco,
                cidade = localizacaoViewModel.cidade,
                cep = localizacaoViewModel.cep
            };

            _mockMapper.Setup(mapper => mapper.Map<LocalizacaoModel>(localizacaoViewModel)).Returns(localizacaoModel);

            _result = _controller.Post(localizacaoViewModel);
        }

        [Then(@"eu recebo uma resposta de criação com a nova localização")]
        public void ThenEuReceboUmaRespostaDeCriacaoComANovaLocalizacao()
        {
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(_result);
            var model = Assert.IsAssignableFrom<LocalizacaoModel>(createdAtActionResult.Value);
            Assert.NotNull(model);
            Assert.Equal("Nova Localização", model.endereco); 
        }

        [Given(@"uma localização existente com id (.*)")]
        public void GivenUmaLocalizacaoExistenteComId(int id)
        {
            var localizacaoModel = new LocalizacaoModel { id_loc = id, endereco = "Atualizada" };
            _mockLocalizacaoServices.Setup(repo => repo.ObterLocalizacaoPorId(id)).Returns(localizacaoModel);
        }

        [When(@"eu atualizo a localização com id (.*)")]
        public void WhenEuAtualizoALocalizacaoComId(int id)
        {
            var localizacaoViewModel = new LocalizacaoViewModel { id_loc = id, endereco = "Atualizada" };
            _result = _controller.Put(id, localizacaoViewModel);
        }

        [Then(@"eu recebo uma resposta sem conteúdo")]
        public void ThenEuReceboUmaRespostaSemConteudo()
        {
            Assert.IsType<NoContentResult>(_result);
        }

        [When(@"eu deleto a localização com id (.*)")]
        public void WhenEuDeletoALocalizacaoComId(int id)
        {
            _result = _controller.Delete(id);
        }

        [Then(@"eu recebo uma resposta de sucesso ao deletar")]
        public void ThenEuReceboUmaRespostaDeSucessoAoDeletar()
        {
            Assert.IsType<NoContentResult>(_result);
        }
    }
}
