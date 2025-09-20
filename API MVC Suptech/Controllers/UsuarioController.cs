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
        public async Task<ActionResult<LoginResponseDto>> AdicionaUsuario([FromBody] NovoUsuarioDto request)
        {
            var usuarioExistente = await _context.Usuario.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (usuarioExistente != null)
            {
                return BadRequest("Email em uso, tente outro!");
            }
            var novoUsuario = new Usuario
            {
                Nome = request.Nome,
                Email = request.Email,
                Senha = request.Senha,
                Setor = "Atendimento",
                CreationDate = DateTime.Now
            };

            try
            {
                _context.Usuario.Add(novoUsuario);
                await _context.SaveChangesAsync();
                return Ok(novoUsuario);
            }
            catch
            {
                return BadRequest("Falha ao adicionar um usuário");
            }
        }

    }

}
