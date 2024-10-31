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
    public class LocalizacaoControllerTests
    {
        private readonly Mock<ILocalizacaoServices> _mockLocalizacaoServices;
        private readonly Mock<IMapper> _mockMapper;
        private readonly LocalizacaoController _controller;

        public LocalizacaoControllerTests()
        {
            _mockLocalizacaoServices = new Mock<ILocalizacaoServices>();
            _mockMapper = new Mock<IMapper>();
            _controller = new LocalizacaoController(_mockLocalizacaoServices.Object, _mockMapper.Object);
        }

        [Fact]
        public void Get_ReturnsOkResult_WithLocalizacaoViewModels()
        {
            // Arrange
            var localizacoes = new List<LocalizacaoModel>
            {
                new LocalizacaoModel { id_loc = 1, endereco = "Localização 1" },
                new LocalizacaoModel { id_loc = 2, endereco = "Localização 2" }
            };
            var viewModelList = localizacoes.Select(l => new LocalizacaoViewModel { id_loc = l.id_loc, endereco = l.endereco });

            _mockLocalizacaoServices.Setup(repo => repo.ListarLocalizacao()).Returns(localizacoes);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<LocalizacaoViewModel>>(localizacoes)).Returns(viewModelList);

            // Act
            var result = _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<LocalizacaoViewModel>>(okResult.Value);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void Get_ReturnsNotFound_WhenLocalizacaoNotFound()
        {
            // Arrange
            int id = 1;
            _mockLocalizacaoServices.Setup(repo => repo.ObterLocalizacaoPorId(id)).Returns((LocalizacaoModel)null);

            // Act
            var result = _controller.Get(id);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Post_ReturnsCreatedAtAction_WhenValidObjectPassed()
        {
            // Arrange
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

            // Act
            var result = _controller.Post(localizacaoViewModel);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var model = Assert.IsAssignableFrom<LocalizacaoModel>(createdAtActionResult.Value);
            Assert.Equal(localizacaoModel.id_loc, model.id_loc);
        }

        [Fact]
        public void Put_ReturnsNoContent_WhenValidObjectPassed()
        {
            // Arrange
            int id = 1;
            var localizacaoViewModel = new LocalizacaoViewModel { id_loc = id, endereco = "Atualizada" };
            var localizacaoModel = new LocalizacaoModel { id_loc = id, endereco = "Atualizada" };

            _mockMapper.Setup(mapper => mapper.Map<LocalizacaoModel>(localizacaoViewModel)).Returns(localizacaoModel);

            // Act
            var result = _controller.Put(id, localizacaoViewModel);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Delete_ReturnsNoContent_WhenValidIdPassed()
        {
            // Arrange
            int id = 1;

            // Act
            var result = _controller.Delete(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
