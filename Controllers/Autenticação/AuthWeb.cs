using API_MVC_Suptech.Data;
using API_MVC_Suptech.Entitys.Dtos;
using API_MVC_Suptech.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API_MVC_Suptech.Controllers.Autenticação
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly CrudData _context;
        private readonly TokenService _tokenService;
        private readonly ILogger<AuthController> _logger;
        private readonly IWebHostEnvironment _env;
        public AuthController(CrudData context, TokenService tokenService, ILogger<AuthController> logger, IWebHostEnvironment env)
        {
            _context = context;
            _tokenService = tokenService;
            _logger = logger;
            _env = env;
        }

        [HttpPost("LoginWeb")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Email);
                if (usuario != null && BCrypt.Net.BCrypt.Verify(request.Senha, usuario.Senha))
                {
                    var token = _tokenService.GenerateToken(usuario);
                    return Ok(new { Token = token });
                }
                var gerente = await _context.Gerentes.FirstOrDefaultAsync(g => g.Email == request.Email);
                if (gerente != null && BCrypt.Net.BCrypt.Verify(request.Senha, gerente.Senha))
                {
                    var token = _tokenService.GenerateToken(gerente);
                    return Ok(new { Token = token });
                }
                var tecnico = await _context.Tecnicos.FirstOrDefaultAsync(t => t.Email == request.Email);
                if (tecnico != null && BCrypt.Net.BCrypt.Verify(request.Senha, tecnico.Senha))
                {
                    var token = _tokenService.GenerateToken(tecnico);
                    return Ok(new { Token = token });
                }
                return Unauthorized("Credenciais inválidas.");
            }
            catch (Exception ex)
            {
                // Log completo
                _logger.LogError(ex, "Erro durante o login.");

                // Em Development, retorna detalhes para ajudar depuração
                if (_env.IsDevelopment())
                {
                    return StatusCode(500, new { Message = "Erro interno no servidor.", Detail = ex.ToString() });
                }

                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }
        /// <summary>
        /// Decodifica o token e retorna apenas o email
        /// </summary>
        [HttpPost("ObterEmail")]
        public IActionResult ObterEmailDoToken([FromBody] TokenRequestDto request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Token))
                {
                    return BadRequest("Token não fornecido.");
                }

                var handler = new JwtSecurityTokenHandler();

                // Verifica se é um token válido
                if (!handler.CanReadToken(request.Token))
                {
                    return BadRequest("Token inválido.");
                }

                // Decodifica o token
                var jwtToken = handler.ReadJwtToken(request.Token);

                // Extrai apenas o email
                var email = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

                if (string.IsNullOrEmpty(email))
                {
                    return NotFound("Email não encontrado no token.");
                }

                return Ok(new { Email = email });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao decodificar token.");
                return StatusCode(500, "Erro ao processar o token.");
            }
        }
    }
}
