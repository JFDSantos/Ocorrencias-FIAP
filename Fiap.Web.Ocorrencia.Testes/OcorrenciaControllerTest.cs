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

namespace Fiap.Web.Ocorrencias.Tests
{
    public class OcorrenciaControllerTests
    {
        private readonly Mock<IOcorrenciaServices> _mockOcorrenciaService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly OcorrenciaController _ocorrenciaController;

        public OcorrenciaControllerTests()
        {
            _mockOcorrenciaService = new Mock<IOcorrenciaServices>();
            _mockMapper = new Mock<IMapper>();
            _ocorrenciaController = new OcorrenciaController(_mockOcorrenciaService.Object, _mockMapper.Object);
        }

        [Fact]
        public void Get_ReturnsOkResult_WithListOfOcorrencias()
        {
            // Arrange
            var ocorrencias = new List<OcorrenciaModel>
            {
                new OcorrenciaModel { id_ocorrencia = 1, descricao = "Ocorrencia 1" },
                new OcorrenciaModel { id_ocorrencia = 2, descricao = "Ocorrencia 2" }
            };

            // Configura o mock para retornar as ocorrências simuladas
            _mockOcorrenciaService.Setup(s => s.ListarOcorrenciaUltimaReferencia(It.IsAny<int>(), It.IsAny<int>()))
                                  .Returns(ocorrencias);

            // Configura o mapeamento do AutoMapper, se necessário
            _mockMapper.Setup(m => m.Map<IEnumerable<OcorrenciaViewModel>>(It.IsAny<IEnumerable<OcorrenciaModel>>()))
                       .Returns(ocorrencias.Select(o => new OcorrenciaViewModel { id_ocorrencia = o.id_ocorrencia, descricao = o.descricao }));

            // Act
            var result = _ocorrenciaController.Get(referencia: 0, tamanho: 10);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var viewModel = Assert.IsType<OcorrenciaPaginacaoReferenciaViewModel>(okResult.Value);

            Assert.Equal(2, viewModel.Ocorrencia.Count()); // Verifica se há duas ocorrências retornadas
            Assert.Equal(200, okResult.StatusCode); // Verifica se o status code é 200 OK
        }
    }
}
