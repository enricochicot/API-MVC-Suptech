using API_MVC_Suptech.Entitys;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_MVC_Suptech.Services
{
    // Serviço responsável por gerar tokens JWT para diferentes tipos de entidade
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        // Injeta IConfiguration para ler chaves e opções de JWT do appsettings
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Gera token para a entidade Usuario.
        // Usa o campo `Setor` do Usuario como roles (pode conter múltiplos valores separados por vírgula).
        public string GenerateToken(Usuario usuario)
        {
            var roles = new List<string>();
            if (!string.IsNullOrWhiteSpace(usuario.Setor))
            {
                // Permite vários roles em Setor separados por vírgula ou ponto-e-vírgula
                roles.AddRange(usuario.Setor.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                                              .Select(r => r.Trim())
                                              .Where(r => !string.IsNullOrEmpty(r)));
            }

            // Constrói o token reutilizando o builder centralizado
            return BuildToken(usuario.UsuarioID.ToString(), usuario.Nome, usuario.Email, roles);
        }

        // Gera token para Gerente.
        // Inclui sempre a role explícita "gerente" e também considera o campo Setor se preenchido.
        public string GenerateToken(Gerente gerente)
        {
            var roles = new List<string>();
            if (!string.IsNullOrWhiteSpace(gerente.Setor))
            {
                roles.AddRange(gerente.Setor.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                                              .Select(r => r.Trim())
                                              .Where(r => !string.IsNullOrEmpty(r)));
            }
            // Adiciona explicitamente a role de gerente
            roles.Add("gerente");

            return BuildToken(gerente.GerenteID.ToString(), gerente.Nome, gerente.Email, roles.Distinct());
        }

        // Gera token para Tecnico.
        // Atribui a role "tecnico" e adiciona a especialidade como claim adicional.
        public string GenerateToken(Tecnico tecnico)
        {
            var roles = new List<string> { "tecnico" };
            // Adiciona a especialidade como claim personalizada (não é usada como role)
            return BuildToken(tecnico.TecnicoID.ToString(), tecnico.Nome, tecnico.Email, roles, additionalClaims: new[] { new Claim("especialidade", tecnico.Especialidade ?? string.Empty) });
        }

        // Método central que monta o JWT com as claims e assinaturas.
        // Lê as configurações em Jwt:Key, Jwt:Issuer, Jwt:Audience e usa apenas Jwt:ExpireHours para expiração
        private string BuildToken(string id, string nome, string email, IEnumerable<string> roles, IEnumerable<Claim>? additionalClaims = null)
        {
            // Leitura das configurações do appsettings ou variáveis de ambiente
            var key = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            // Usa apenas Jwt:ExpireHours (número de horas). Default = 1 hora.
            var expiresMinutes = 60; // default 1 hora
            var expireHoursValue = _configuration["Jwt:ExpireHours"];
            if (!string.IsNullOrEmpty(expireHoursValue) && int.TryParse(expireHoursValue, out var parsedHours))
            {
                expiresMinutes = parsedHours * 60;
            }

            if (string.IsNullOrEmpty(key))
            {
                // Falha explícita para facilitar identificação de configuração faltando
                throw new InvalidOperationException("JWT key is not configured. Set Jwt:Key in configuration.");
            }

            // Cria a chave simétrica e as credenciais de assinatura (HMAC-SHA256)
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Claims padrão incluídas no token
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim(ClaimTypes.Name, nome ?? string.Empty),
                new Claim(ClaimTypes.Email, email ?? string.Empty)
            };

            // Adiciona claims adicionais, se fornecidas
            if (additionalClaims != null)
            {
                claims.AddRange(additionalClaims.Where(c => c != null));
            }

            // Adiciona roles como ClaimTypes.Role
            if (roles != null)
            {
                foreach (var role in roles.Where(r => !string.IsNullOrEmpty(r)).Distinct())
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            // Monta o token JWT com validade e assinatura
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(expiresMinutes),
                signingCredentials: credentials
            );

            // Serializa o token para string compacta
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
