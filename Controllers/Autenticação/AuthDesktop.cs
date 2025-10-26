using API_MVC_Suptech.Data;
using API_MVC_Suptech.Entitys.Dtos;
using API_MVC_Suptech.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_MVC_Suptech.Controllers.Autenticação
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthDesktop : Controller
    {
        private readonly TokenService _tokenService;
        private readonly CrudData _context;
        private readonly ILogger<AuthDesktop> _logger;
        private readonly IWebHostEnvironment _env;

        public AuthDesktop(TokenService tokenService, CrudData context, ILogger<AuthDesktop> logger, IWebHostEnvironment env)
        {
            _tokenService = tokenService;
            _context = context;
            _logger = logger;
            _env = env;
        }
        [HttpPost("LoginDesktop")]
        public async Task<IActionResult> LoginDesktop([FromBody] LoginDto request)
        {
            try
            {
                var gerente = await _context.Gerentes.FirstOrDefaultAsync(g => g.Email == request.Email);
                if (gerente != null && BCrypt.Net.BCrypt.Verify(request.Senha, gerente.Senha))
                {
                    var token = _tokenService.GenerateToken(gerente);
                    return Ok(new { Token = token });
                }
                return Unauthorized("Credenciais inválidas.");
            }
            catch (Exception ex)
            {
                // Log completo
                _logger.LogError(ex, "Erro durante o login.");
                if (_env.IsDevelopment())
                {
                    return StatusCode(500, new { Message = "Erro interno no servidor.", Detail = ex.ToString() });
                }
                // Em produção, retorna mensagem genérica
                return StatusCode(500, "Erro interno no servidor.");
            }
        }
    }
}
