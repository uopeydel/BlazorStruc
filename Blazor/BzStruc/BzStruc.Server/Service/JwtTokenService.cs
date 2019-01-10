using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BzStruc.Server.Service
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;
        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public string GenerateToken(string email, string id)
        {
            var claims = new[] {
                new Claim (JwtRegisteredClaimNames.NameId ,id),
                new Claim (JwtRegisteredClaimNames.Email ,email),
                new Claim (JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["JWT:Issuer"],
                _configuration["JWT:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(double.Parse(_configuration["JWT:ExpiredAt"])),
                signingCredentials: credential);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }

    public interface IJwtTokenService
    {
        string GenerateToken(string email, string id);
    }
}
