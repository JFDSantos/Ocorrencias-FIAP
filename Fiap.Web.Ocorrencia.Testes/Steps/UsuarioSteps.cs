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

namespace Fiap.Web.Ocorrencia.Testes
{
    [Binding]
    public class UsuarioSteps
    {
        private readonly Mock<IUsuarioServices> _mockUsuarioServices;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UsuarioController _controller;
        private ActionResult<IEnumerable<UsuarioViewModel>> _result;
        private ActionResult<UsuarioViewModel> _singleResult;

        public UsuarioSteps()
        {
            _mockUsuarioServices = new Mock<IUsuarioServices>();
            _mockMapper = new Mock<IMapper>();
            _controller = new UsuarioController(_mockUsuarioServices.Object, _mockMapper.Object);
        }

        [Given(@"que existem usuários cadastrados")]
        public void GivenQueExistemUsuariosCadastrados()
        {
            var usuarios = new List<UsuarioModel>
            {
                new UsuarioModel { id_usuario = 1, nome = "Usuário 1" },
                new UsuarioModel { id_usuario = 2, nome = "Usuário 2" }
            };

            _mockUsuarioServices.Setup(s => s.ListarUsuarios()).Returns(usuarios);
            _mockMapper.Setup(m => m.Map<IEnumerable<UsuarioViewModel>>(It.IsAny<IEnumerable<UsuarioModel>>()))
                       .Returns(usuarios.Select(u => new UsuarioViewModel { id_usuario = u.id_usuario, nome = u.nome }));
        }

        [Given(@"que não existem usuários cadastrados")]
        public void GivenQueNaoExistemUsuariosCadastrados()
        {
            _mockUsuarioServices.Setup(s => s.ListarUsuarios()).Returns(new List<UsuarioModel>());
        }

        [Given(@"que o ID do usuário é inválido")]
        public void GivenQueOIdDoUsuarioEInvalido()
        {
            _mockUsuarioServices.Setup(s => s.ObterUsuarioPorId(It.IsAny<int>())).Returns((UsuarioModel)null);
        }

        [When(@"eu faço uma solicitação de usuários GET para ""(.*)""")]
        public void WhenEuFacoUmaSolicitacaoDeUsuariosGETPara(string endpoint)
        {
            _result = _controller.Get();
        }

        [When(@"eu faço uma solicitação de usuários GET para ""(.*)"" com o ID inválido")]
        public void WhenEuFacoUmaSolicitacaoDeUsuariosGETParaComOIdInvalido(string endpoint)
        {
            _singleResult = _controller.Get(-1); // ID inválido
        }

        [Then(@"o status da resposta de usuários deve ser (.*)")]
        public void ThenOStatusDaRespostaDeUsuariosDeveSer(int statusCode)
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

        [Then(@"o corpo da resposta de usuários deve conter uma lista de usuários")]
        public void ThenOCorpoDaRespostaDeUsuariosDeveConterUmaListaDeUsuarios()
        {
            var okResult = Assert.IsType<OkObjectResult>(_result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<UsuarioViewModel>>(okResult.Value);
            Assert.True(model.Any());
        }

        [Then(@"a lista de usuários deve conter duas entradas")]
        public void ThenAListaDeUsuariosDeveConterDuasEntradas()
        {
            var okResult = Assert.IsType<OkObjectResult>(_result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<UsuarioViewModel>>(okResult.Value);
            Assert.Equal(2, model.Count());
        }

        [Then(@"o corpo da resposta de usuários deve estar vazio")]
        public void ThenOCorpoDaRespostaDeUsuariosDeveEstarVazio()
        {
            var noContentResult = Assert.IsType<NoContentResult>(_result.Result);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Then(@"o corpo da resposta de usuários deve conter uma mensagem de erro")]
        public void ThenOCorpoDaRespostaDeUsuariosDeveConterUmaMensagemDeErro()
        {
            var BadResult = Assert.IsType<BadRequestObjectResult>(_singleResult.Result);
            Assert.NotNull(BadResult.Value);
            Assert.Equal(400, BadResult.StatusCode);
        }
    }
}
