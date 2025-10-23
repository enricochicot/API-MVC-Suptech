using API_MVC_Suptech.Data;
using API_MVC_Suptech.Entitys;
using API_MVC_Suptech.Entitys.Dtos;
using API_MVC_Suptech.Services;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
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
        private readonly ILogger<UsuarioController> _logger;

        // Construtor para injeção de dependência
        public UsuarioController(CrudData context, IConfiguration configuration, TokenService tokenService, ILogger<UsuarioController> logger)
        {
            _context = context;
            _configuration = configuration;
            _tokenService = tokenService;
            _logger = logger;
        }

        // Ações CRUD (Create, Read, Update, Delete) para a entidade Usuario podem ser implementadas aqui.
        [HttpPost("Adicionar")]
        public async Task<IActionResult> AdicionarUsuario([FromBody] NovoUsuarioDto request)
        {
            try
            {
                var existingUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Email);
                if (existingUser != null)
                {
                    return BadRequest("Email já está em uso.");
                }

                var usuario = new Usuario
                {
                    Nome = request.Nome,
                    Email = request.Email,
                    Senha = BCrypt.Net.BCrypt.HashPassword(request.Senha),
                    Setor = request.Setor,
                    Telefone = request.Telefone
                };
                if (string.IsNullOrEmpty(usuario.Nome) || string.IsNullOrEmpty(usuario.Email)
                   || string.IsNullOrEmpty(usuario.Senha))
                {
                    return BadRequest("Nome, Email e Senha são obrigatórios.");
                }

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                return Ok("Usuário adicionado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar usuário.");
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> ListarUsuarios()
        {
            try
            {
                var usuarios = await _context.Usuarios.ToListAsync();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar usuários.");
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPut("Editar/{id}")]
        public async Task<IActionResult> EditarUsuario(Guid id, [FromBody] EditarDto request)
        {
            try { 
                var usuario = await _context.Usuarios.FindAsync(id);
                if (usuario == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                usuario.Nome = request.Nome ?? usuario.Nome;
                usuario.Email = request.Email ?? usuario.Email;
                if (!string.IsNullOrEmpty(request.Senha))
                {
                    usuario.Senha = BCrypt.Net.BCrypt.HashPassword(request.Senha);
                }
                usuario.Setor = request.Setor ?? usuario.Setor;
                usuario.Telefone = request.Telefone ?? usuario.Telefone;

                await _context.SaveChangesAsync();
                return Ok("Usuário atualizado com sucesso!");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao editar usuário.");
                    return StatusCode(500, "Ocorreu um erro interno no servidor.");
                }
        }

        [HttpDelete("Excluir/{id}")]
        public async Task<IActionResult> ExcluirUsuario(Guid id)
        {
            try
            {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return Ok("Usuário excluído com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir usuário.");
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

    }

}
