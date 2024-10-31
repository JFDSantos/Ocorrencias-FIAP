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
    public class UsuarioRoleControllerTest
    {
        private readonly Mock<IUsuarioRoleServices> _mockUsuarioRoleervices;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UsuarioRoleController _controller;

        public UsuarioRoleControllerTest()
        {
            _mockUsuarioRoleervices = new Mock<IUsuarioRoleServices>();
            _mockMapper = new Mock<IMapper>();
            _controller = new UsuarioRoleController(_mockUsuarioRoleervices.Object, _mockMapper.Object);
        }

        [Fact]
        public void Get_ReturnsOkResult_WithUsuarioRoleViewModels()
        {
            // Arrange
            var UsuarioRole = new List<UsuarioRoleModel>
            {
                new UsuarioRoleModel { id_role = 1, role = "Teste"},
                new UsuarioRoleModel { id_role = 2, role = "Teste"}
            };
            var viewModelList = UsuarioRole.Select(v => new UsuarioRoleViewModel { id_role = v.id_role, role = v.role });

            _mockUsuarioRoleervices.Setup(repo => repo.ListarRoles()).Returns(UsuarioRole);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<UsuarioRoleViewModel>>(UsuarioRole)).Returns(viewModelList);

            // Act
            var result = _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<UsuarioRoleViewModel>>(okResult.Value);
            Assert.Equal(2, model.Count());
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
