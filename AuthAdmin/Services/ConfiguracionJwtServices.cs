using AuthAdmin.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthAdmin.Services
{
    public class ConfiguracionJwtServices
    {
        private readonly IConfiguration _config;

 
        public ConfiguracionJwtServices(IConfiguration configuration)
        {
            _config = configuration;
        }

        public string GenerarJwtToken(Usuario user)
        {
            var jwtSettings = _config.GetSection("Jwt");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Correo)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiresInMinutes"])),
                signingCredentials: creds
            );
            Console.WriteLine("KEY GENERACION: " + key);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
