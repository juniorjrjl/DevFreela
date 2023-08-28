using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DevFreela.Core.DTOs;
using DevFreela.Core.Enums;
using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DevFreela.Infrastructure.Auth
{

    public class AuthService : IAuthService
    {

        public IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public CredentialDTO GenerateJwtToken(string email, ICollection<RoleNameEnum> roles)
        {
            var issuer = _configuration.GetValue<string>("Jwt:Issuer")?? 
                throw new ArgumentException("A propriedade 'Jwt:Issuer' não foi devidamente configurada");
            var audience = _configuration.GetValue<string>("Jwt:Audience")?? 
                throw new ArgumentException("A propriedade 'Jwt:Audience' não foi devidamente configurada");
            var key = _configuration.GetValue<string>("Jwt:Key")?? 
                throw new ArgumentException("A propriedade 'Jwt:Key' não foi devidamente configurada");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim("userName", email),
            };

            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var expires = DateTime.Now.AddHours(8);
            var expiresResponse = new DateTimeOffset(expires).ToUnixTimeMilliseconds();
            var token = new JwtSecurityToken(
                issuer: issuer, 
                audience: audience, 
                expires: expires, 
                signingCredentials: credentials, 
                claims: claims
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return new CredentialDTO(stringToken, expiresResponse);
        }

        public string ComputeSha256Hash(string password)
        {
            using(var sha256Hash = SHA256.Create())
            {

                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }

}
