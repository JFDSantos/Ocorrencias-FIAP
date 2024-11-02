using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Fiap.Web.Ocorrencia.Controllers;
using Fiap.Web.Ocorrencia.Services;
using Fiap.Web.Ocorrencias.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Moq;
using TechTalk.SpecFlow;
using Xunit;

namespace Fiap.Web.Ocorrencias.Tests
{
    [Binding]
    public class AuthSteps
    {
        private readonly Mock<IAuthServices> _mockAuthService;
        private readonly AuthController _authController;
        private ActionResult _result;
        private UsuarioModel _user;

        public AuthSteps()
        {
            _mockAuthService = new Mock<IAuthServices>();
            _authController = new AuthController(_mockAuthService.Object);
        }

        [Given(@"que o usuário possui credenciais válidas")]
        public void GivenQueOUsuarioPossuiCredenciaisValidas()
        {
            _user = new UsuarioModel
            {
                email = "test@example.com",
                senha = "password",
                Role = new UsuarioRoleModel { role = "user" }
            };

            _mockAuthService.Setup(s => s.Authenticate(_user.email, _user.senha)).Returns(_user);
        }

        [Given(@"que o usuário possui credenciais inválidas")]
        public void GivenQueOUsuarioPossuiCredenciaisInvalidas()
        {
            _user = new UsuarioModel
            {
                email = "invalid@example.com",
                senha = "invalidpassword"
            };

            _mockAuthService.Setup(s => s.Authenticate(_user.email, _user.senha)).Returns((UsuarioModel)null);
        }

        [When(@"eu realizo o login")]
        public void WhenEuRealizoOLogin()
        {
            _result = (ActionResult)_authController.Login(_user);
        }

        [Then(@"eu recebo uma resposta de sucesso com um token JWT válido")]
        public void ThenEuReceboUmaRespostaDeSucessoComUmTokenJwtValido()
        {
            var okResult = Assert.IsType<OkObjectResult>(_result);
            Assert.Equal(200, okResult.StatusCode);

            var token = GenerateJwtToken(_user);
            Assert.NotNull(token);

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            Assert.Equal("fiap", jwtToken.Issuer);
            Assert.True(jwtToken.ValidTo > DateTime.UtcNow);
        }

        [Then(@"eu recebo uma resposta de não autorizado")]
        public void ThenEuReceboUmaRespostaDeNaoAutorizado()
        {
            var unauthorizedResult = Assert.IsType<UnauthorizedResult>(_result);
            Assert.Equal(401, unauthorizedResult.StatusCode);
        }

        private string GenerateJwtToken(UsuarioModel user)
        {
            byte[] secret = Encoding.ASCII.GetBytes("f+ujXAKHk00L5jlMXo2XhAWawsOoihNP1OiAM25lLSO57+X7uBMQgwPju6yzyePi");
            var securityKey = new SymmetricSecurityKey(secret);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.email),
                new Claim(ClaimTypes.Role, user.Role.role),
                new Claim(ClaimTypes.Hash, Guid.NewGuid().ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = "fiap",
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }
    }
}
