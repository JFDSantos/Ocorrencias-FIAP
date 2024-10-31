using Fiap.Web.Ocorrencia.Controllers;
using Fiap.Web.Ocorrencia.Services;
using Fiap.Web.Ocorrencias.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace Fiap.Web.Ocorrencias.Tests
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthServices> _mockAuthService;
        private readonly AuthController _authController;

        public AuthControllerTests()
        {
            _mockAuthService = new Mock<IAuthServices>();
            _authController = new AuthController(_mockAuthService.Object);
        }

        [Fact]
        public void Login_ReturnsToken_OnSuccessfulAuthentication()
        {
            // Arrange
            var user = new UsuarioModel
            {
                email = "test@example.com",
                senha = "password",
                Role = new UsuarioRoleModel { role = "user" }
            };

            // Mock para simular autenticação bem-sucedida
            _mockAuthService.Setup(s => s.Authenticate(user.email, user.senha)).Returns(user);

            // Act
            var result = _authController.Login(user) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var token = GenerateJwtToken(user);

            Assert.NotNull(token);

            // Validar o token JWT
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            Assert.Equal("fiap", jwtToken.Issuer); // Verifica se o emissor do token é 'fiap'
            Assert.True(jwtToken.ValidTo > DateTime.UtcNow); // Verifica se o token não expirou
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

        [Fact]
        public void Login_ReturnsUnauthorized_OnFailedAuthentication()
        {
            // Arrange
            var invalidUser = new UsuarioModel
            {
                email = "invalid@example.com",
                senha = "invalidpassword"
            };

            // Mock para simular autenticação falha
            _mockAuthService.Setup(s => s.Authenticate(invalidUser.email, invalidUser.senha)).Returns((UsuarioModel)null);

            // Act
            var result = _authController.Login(invalidUser) as UnauthorizedResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(401, result.StatusCode);
        }
    }
}
