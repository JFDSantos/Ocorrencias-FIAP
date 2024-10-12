using AutoMapper;
using Fiap.Web.Ocorrencia.Controllers;
using Fiap.Web.Ocorrencia.Services;
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
    public class UsuarioControllerTest
    {
        private readonly Mock<IUsuarioServices> _mockUsuarioervices;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UsuarioController _controller;

        public UsuarioControllerTest()
        {
            _mockUsuarioervices = new Mock<IUsuarioServices>();
            _mockMapper = new Mock<IMapper>();
            _controller = new UsuarioController(_mockUsuarioervices.Object, _mockMapper.Object);
        }

        [Fact]
        public void Get_ReturnsOkResult_WithUsuarioViewModels()
        {
            // Arrange
            var Usuario = new List<UsuarioModel>
            {
                new UsuarioModel { id_usuario = 1, nome = "Teste"},
                new UsuarioModel { id_usuario = 2, nome = "Teste"}
            };
            var viewModelList = Usuario.Select(v => new UsuarioViewModel { id_usuario = v.id_usuario, nome = v.nome });

            _mockUsuarioervices.Setup(repo => repo.ListarUsuarios()).Returns(Usuario);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<UsuarioViewModel>>(Usuario)).Returns(viewModelList);

            // Act
            var result = _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<UsuarioViewModel>>(okResult.Value);
            Assert.Equal(2, model.Count());
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
