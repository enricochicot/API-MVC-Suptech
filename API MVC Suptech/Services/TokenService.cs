using API_MVC_Suptech.Entitys;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System;

namespace API_MVC_Suptech.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _signingKey;
        private readonly SigningCredentials _signingCredentials;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expireHours;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;

            var keyString = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("Configuração Jwt:Key ausente.");

            byte[] keyBytes;
            try
            {
                // Tenta interpretar como base64 (recomendado)
                keyBytes = Convert.FromBase64String(keyString);
            }
            catch
            {
                // Se não for base64, usa UTF8 (compatibilidade)
                keyBytes = Encoding.UTF8.GetBytes(keyString);
            }

            if (keyBytes.Length < 16) // 16 bytes = 128 bits
                throw new InvalidOperationException("Chave Jwt deve ter ao menos 128 bits (16 bytes). Forneça uma chave maior ou um valor base64 de 32 bytes.");

            _signingKey = new SymmetricSecurityKey(keyBytes);
            _signingCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);

            _issuer = _configuration["Jwt:Issuer"] ?? "Suptech";
            _audience = _configuration["Jwt:Audience"] ?? "SuptechUsuarios";
            _expireHours = int.TryParse(_configuration["Jwt:ExpireHours"], out var h) ? h : 2;
        }

        public string GenerateToken(string email)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email)
            };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_expireHours),
                signingCredentials: _signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
