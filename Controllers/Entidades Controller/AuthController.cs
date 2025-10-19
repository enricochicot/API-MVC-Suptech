using API_MVC_Suptech.Data;
using API_MVC_Suptech.Entitys.Dtos;
using API_MVC_Suptech.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

namespace API_MVC_Suptech.Controllers.Entidades_Controller
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

        [HttpPost("Login")]
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
                if (_env.IsDevelopment()    )
                {
                    return StatusCode(500, new { Message = "Erro interno no servidor.", Detail = ex.ToString() });
                }

                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }


    }
}
