using API_MVC_Suptech.Data;
using API_MVC_Suptech.Entitys.Dtos;
using API_MVC_Suptech.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_MVC_Suptech.Controllers.Entidades_Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly CrudData _context;
        private readonly TokenService _tokenService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(CrudData context, TokenService tokenService, ILogger<AuthController> logger)
        {
            _context = context;
            _tokenService = tokenService;
            _logger = logger;

        }
        //Autenticação de usuario
        //Faz autenticação do usuario e retorna um token JWT
        [HttpPost("Login/Usuarios")]
        public IActionResult Login([FromBody] LoginDto request)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == request.Email);

            if (usuario == null)
                return Unauthorized("Usuário não encontrado.");

            if (!BCrypt.Net.BCrypt.Verify(request.Senha, usuario.Senha))
                return Unauthorized("Senha incorreta.");

            var token = _tokenService.GenerateToken(usuario.Email);

            return Ok(new { token });
        }

    }

}
