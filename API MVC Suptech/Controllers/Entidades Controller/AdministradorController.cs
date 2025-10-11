using API_MVC_Suptech.Data;
using API_MVC_Suptech.Entitys;
using API_MVC_Suptech.Entitys.Dtos;
using API_MVC_Suptech.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Microsoft.Extensions.Logging;

namespace API_MVC_Suptech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministradorController : ControllerBase
    {
        private readonly CrudData _context;
        private readonly IConfiguration _configuration;
        private readonly TokenService _tokenService;
        private readonly ILogger<AdministradorController> _logger;

        public AdministradorController(
            CrudData context,
            IConfiguration configuration,
            TokenService tokenService,
            ILogger<AdministradorController> logger)
        {
            _context = context;
            _configuration = configuration;
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpPost("Adicionar")]
        public async Task<IActionResult> AdicionarAdministrador([FromBody] NovoAdministradorDto request)
        {
            try
            {
                var existingAdmin = await _context.Administradores.FirstOrDefaultAsync(a => a.Email == request.Email);
                if (existingAdmin != null)
                {
                    return BadRequest("Email já está em uso.");
                }
                var administrador = new Administrador
                {
                    Nome = request.Nome,
                    Email = request.Email,
                    Senha = BCrypt.Net.BCrypt.HashPassword(request.Senha),
                    Setor = request.Setor,
                    Telefone = request.Telefone
                };
                if (string.IsNullOrEmpty(administrador.Nome) || string.IsNullOrEmpty(administrador.Email)
                   || string.IsNullOrEmpty(administrador.Senha))
                {
                    return BadRequest("Nome, Email e Senha são obrigatórios.");
                }
                _context.Administradores.Add(administrador);
                await _context.SaveChangesAsync();
                return Ok("Administrador adicionado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar administrador.");
                return StatusCode(500, "Ocorreu um erro ao adicionar o administrador.");
            }
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> ListarAdministradores()
        {
            try
            {
                var administradores = await _context.Administradores.ToListAsync();
                return Ok(administradores);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar administradores.");
                return StatusCode(500, "Ocorreu um erro ao listar os administradores.");
            }
        }

        [HttpPut("Editar/{id}")]
        public async Task<IActionResult> EditarAdministrador(Guid id, [FromBody] EditarDto request)
        {
            try
            {
                var administrador = await _context.Administradores.FindAsync(id);
                if (administrador == null)
                {
                    return NotFound("Administrador não encontrado.");
                }
                administrador.Nome = request.Nome ?? administrador.Nome;
                administrador.Email = request.Email ?? administrador.Email;
                if (!string.IsNullOrEmpty(request.Senha))
                {
                    administrador.Senha = BCrypt.Net.BCrypt.HashPassword(request.Senha);
                }
                administrador.Setor = request.Setor ?? administrador.Setor;
                administrador.Telefone = request.Telefone ?? administrador.Telefone;
                await _context.SaveChangesAsync();
                return Ok("Administrador atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao editar administrador com id {id}.");
                return StatusCode(500, "Ocorreu um erro ao editar o administrador.");
            }
        }

        [HttpDelete("Excluir")]
        public async Task<IActionResult> DeletarAdministrador(Guid id)
        {
            try
            {
                var administrador = await _context.Administradores.FindAsync(id);
                if (administrador == null)
                {
                    return NotFound("Administrador não encontrado.");
                }
                _context.Administradores.Remove(administrador);
                await _context.SaveChangesAsync();
                return Ok("Administrador deletado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao deletar administrador com id {id}.");
                return StatusCode(500, "Ocorreu um erro ao deletar o administrador.");
            }
        }
    }
}