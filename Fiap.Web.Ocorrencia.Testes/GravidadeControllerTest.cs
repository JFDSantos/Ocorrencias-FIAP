using AutoMapper;
using Fiap.Web.Ocorrencia.Controllers;
using Fiap.Web.Ocorrencia.ViewModel;
using Fiap.Web.Ocorrencias.Models;
using Fiap.Web.Ocorrencias.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Fiap.Web.Gravidades.Tests
{
    public class GravidadeControllerTests
    {
        private readonly Mock<IGravidadeServices> _mockGravidadeervices;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GravidadeController _controller;

        public GravidadeControllerTests()
        {
            _mockGravidadeervices = new Mock<IGravidadeServices>();
            _mockMapper = new Mock<IMapper>();
            _controller = new GravidadeController(_mockGravidadeervices.Object, _mockMapper.Object);
        }

        [Fact]
        public void Get_ReturnsOkResult_WithGravidadeViewModels()
        {
            // Arrange
            var Gravidade = new List<GravidadeModel>
            {
                new GravidadeModel { id_gravidade = 1, descricao = "ABC1234"},
                new GravidadeModel { id_gravidade = 2, descricao = "XYZ5678"}
            };
            var viewModelList = Gravidade.Select(v => new GravidadeViewModel { id_gravidade = v.id_gravidade, descricao = v.descricao });

            _mockGravidadeervices.Setup(s => s.ListarGravidadeUltimaReferencia(It.IsAny<int>(), It.IsAny<int>()))
                                 .Returns(Gravidade);

            // Configura o mapeamento do AutoMapper, se necessário
            _mockMapper.Setup(m => m.Map<IEnumerable<GravidadeViewModel>>(It.IsAny<IEnumerable<GravidadeModel>>()))
                       .Returns(Gravidade.Select(o => new GravidadeViewModel { id_gravidade = o.id_gravidade, descricao = o.descricao }));

            // Act
            var result = _controller.Get(referencia: 0, tamanho: 10);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var viewModel = Assert.IsType<GravidadePaginacaoReferenciaViewModel>(okResult.Value);

            Assert.Equal(2, viewModel.Gravidade.Count()); // Verifica se há duas ocorrências retornadas
            Assert.Equal(200, okResult.StatusCode); // Verifica se o status code é 200 OK
        }
    }
}
