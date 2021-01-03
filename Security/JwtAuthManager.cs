using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KanbanApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace KanbanApi.Security
{
    public class JwtAuthManager : IJwtAuthManager
    {
        public IConfiguration _config { get; }
        public JwtAuthManager(IConfiguration config)
        {
            this._config = config;

        }
        public string GenerateTokens(User user)
        {
            var claims = new[]
            {
                new Claim("username", user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var token = new JwtSecurityToken(
            _config["JwtAuth:Issuer"],
            _config["JwtAuth:Issuer"],
            claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials:  new SigningCredentials(
                                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["JwtAuth:Key"])),
                                    SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
            
        }
    }
}