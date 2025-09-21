using API_MVC_Suptech.Data;
using API_MVC_Suptech.Entitys;
using API_MVC_Suptech.Entitys.Dtos;
using API_MVC_Suptech.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_MVC_Suptech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly CrudData _context;
        private readonly IConfiguration _configuration;
        private readonly TokenService _tokenService;

        public UsuarioController(CrudData context, IConfiguration configuration, TokenService tokenService)
        {
            _context = context;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        // Ações CRUD (Create, Read, Update, Delete) para a entidade Usuario podem ser implementadas aqui.
        [HttpPost("Adicionar")]
        public async Task<IActionResult> AdicionarUsuario([FromBody] NovoUsuarioDto request)
        {


            var usuario = new Usuario
           {
                Nome = request.Nome,
                Email = request.Email,
                Senha = request.Senha // Em um cenário real, a senha deve ser hashada antes de ser armazenada.
            };
            if (string.IsNullOrEmpty(usuario.Nome) || string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(usuario.Senha))
            {
                return BadRequest("Nome, Email e Senha são obrigatórios.");
            }

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return Ok("Usuário adicionado com sucesso!");
        }

    }

}
