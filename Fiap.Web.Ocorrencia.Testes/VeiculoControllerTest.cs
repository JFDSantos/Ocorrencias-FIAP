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
    public class VeiculosControllerTests
    {
        private readonly Mock<IVeiculoServices> _mockVeiculoServices;
        private readonly Mock<IMapper> _mockMapper;
        private readonly VeiculosController _controller;

        public VeiculosControllerTests()
        {
            _mockVeiculoServices = new Mock<IVeiculoServices>();
            _mockMapper = new Mock<IMapper>();
            _controller = new VeiculosController(_mockVeiculoServices.Object, _mockMapper.Object);
        }

        [Fact]
        public void Get_ReturnsOkResult_WithVeiculoViewModels()
        {
            // Arrange
            var veiculos = new List<VeiculoModel>
            {
                new VeiculoModel { id_veic = 1, placa = "ABC1234"},
                new VeiculoModel { id_veic = 2, placa = "XYZ5678"}
            };
            var viewModelList = veiculos.Select(v => new VeiculoViewModel { id_veic = v.id_veic, placa = v.placa });

            _mockVeiculoServices.Setup(repo => repo.ListarVeiculos()).Returns(veiculos);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<VeiculoViewModel>>(veiculos)).Returns(viewModelList);

            // Act
            var result = _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<VeiculoViewModel>>(okResult.Value);
            Assert.Equal(2, model.Count());
            Assert.Equal(200, okResult.StatusCode);
        }

    }
}
