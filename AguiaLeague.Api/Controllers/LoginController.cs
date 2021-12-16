using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace AguiaLeague.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public object Erro()
        {
            return new { Message = "A autenticação falhou." };
        }

        public object Sucesso()
        {
            return new { Message = "Login bem-sucedido" };
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Discord")]
        public object GetToken()
        {
            var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var key = _configuration["Jwt:EncryptionKey"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var permClaims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new ("discordId", userId)
            };

            var token = new JwtSecurityToken(
                issuer,
                audience,
                permClaims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new { ApiToken = jwtToken };
        }
    }
}
