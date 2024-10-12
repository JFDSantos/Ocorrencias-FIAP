using AutoMapper;
using Fiap.Web.Ocorrencia.Controllers;
using Fiap.Web.Ocorrencia.Services;
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
    public class AtendimentoControllerTests
    {
        private readonly Mock<IAtendimentoServices> _mockAtendimentoService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AtendimentoController _atendimentoController;

        public AtendimentoControllerTests()
        {
            _mockAtendimentoService = new Mock<IAtendimentoServices>();
            _mockMapper = new Mock<IMapper>();
            _atendimentoController = new AtendimentoController(_mockAtendimentoService.Object, _mockMapper.Object);
        }

        [Fact]
        public void Get_ReturnsOkResult_WithListOfAtendimentos()
        {
            // Arrange
            var atendimentos = new List<AtendimentoModel>
            {
                new AtendimentoModel { id_atendimento = 1, descricao = "Atendimento 1" },
                new AtendimentoModel { id_atendimento = 2, descricao = "Atendimento 2" }
            };

            // Configura o mock para retornar os atendimentos simulados
            _mockAtendimentoService.Setup(s => s.ListarAtendimentoUltimaReferencia(It.IsAny<int>(), It.IsAny<int>()))
                                  .Returns(atendimentos);

            // Configura o mapeamento do AutoMapper, se necessário
            _mockMapper.Setup(m => m.Map<IEnumerable<AtendimentoViewModel>>(It.IsAny<IEnumerable<AtendimentoModel>>()))
                       .Returns(atendimentos.Select(a => new AtendimentoViewModel { id_atendimento = a.id_atendimento, descricao = a.descricao }));

            // Act
            var result = _atendimentoController.Get(referencia: 0, tamanho: 10);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var viewModel = Assert.IsType<AtendimentoPaginacaoReferenciaViewModel>(okResult.Value);

            Assert.Equal(2, viewModel.Atendimento.Count()); // Verifica se há dois atendimentos retornados
            Assert.Equal(200, okResult.StatusCode); // Verifica se o status code é 200 OK
        }
    }
}
