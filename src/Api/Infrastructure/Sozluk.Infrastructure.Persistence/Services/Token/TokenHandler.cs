using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sozluk.Api.Application.Interfaces.Token;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sozluk.Infrastructure.Persistence.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(Claim[] claims)
        {
            var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Secret"]));
            var credentials=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var expiry= DateTime.Now.AddDays(10);

            var jwt = new JwtSecurityToken(
                claims: claims,
                expires: expiry,
                signingCredentials: credentials,
                notBefore: DateTime.Now);
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
