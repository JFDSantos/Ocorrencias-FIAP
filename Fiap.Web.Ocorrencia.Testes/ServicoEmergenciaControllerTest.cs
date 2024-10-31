using AutoMapper;
using Fiap.Web.Ocorrencia.Controllers;
using Fiap.Web.Ocorrencia.ViewModel;
using Fiap.Web.Ocorrencias.Models;
using Fiap.Web.Ocorrencias.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Web.Ocorrencia.Testes
{
    public class ServicoEmergenciaControllerTest
    {
        private readonly Mock<IServEmergenciaServices> _mockServicoEmergenciaervices;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ServicoEmergenciaController _controller;

        public ServicoEmergenciaControllerTest()
        {
            _mockServicoEmergenciaervices = new Mock<IServEmergenciaServices>();
            _mockMapper = new Mock<IMapper>();
            _controller = new ServicoEmergenciaController(_mockServicoEmergenciaervices.Object, _mockMapper.Object);
        }

        [Fact]
        public void Get_ReturnsOkResult_WithServicoEmergenciaViewModels()
        {
            // Arrange
            var ServicoEmergencia = new List<ServicoEmergenciaModel>
            {
                new ServicoEmergenciaModel { id_serv_emergencia = 1, descricao = "ABC1234"},
                new ServicoEmergenciaModel { id_serv_emergencia = 2, descricao = "XYZ5678"}
            };
            var viewModelList = ServicoEmergencia.Select(v => new ServicoEmergenciaViewModel { id_serv_emergencia = v.id_serv_emergencia, descricao = v.descricao });

            _mockServicoEmergenciaervices.Setup(repo => repo.ListarServicoEmergencia()).Returns(ServicoEmergencia);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<ServicoEmergenciaViewModel>>(ServicoEmergencia)).Returns(viewModelList);

            // Act
            var result = _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<ServicoEmergenciaViewModel>>(okResult.Value);
            Assert.Equal(2, model.Count());
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
