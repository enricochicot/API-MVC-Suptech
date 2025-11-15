using API_MVC_Suptech.Data;
using API_MVC_Suptech.Entitys;
using API_MVC_Suptech.Entitys.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Microsoft.Extensions.Logging;

namespace API_MVC_Suptech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GerenteController : ControllerBase
    {
        private readonly CrudData _context;
        private readonly ILogger<GerenteController> _logger;

        public GerenteController(CrudData context, ILogger<GerenteController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("Adicionar")]
        public async Task<IActionResult> AdicionarGerente([FromBody] NovoGerenteDto request)
        {
            try
            {
                var existing = await _context.Gerentes.FirstOrDefaultAsync(a => a.Email == request.Email);
                if (existing != null)
                    return BadRequest("Email já está em uso.");

                var gerente = new Gerente
                {
                    Nome = request.Nome,
                    Email = request.Email,
                    Senha = BCrypt.Net.BCrypt.HashPassword(request.Senha),
                    Setor = request.Setor,
                    Telefone = request.Telefone
                };
                _context.Gerentes.Add(gerente);
                await _context.SaveChangesAsync();
                return Ok("Gerente adicionado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar gerente.");
                return StatusCode(500, "Ocorreu um erro ao adicionar o gerente.");
            }
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> ListarGerentes()
        {
            try
            {
                var gerentes = await _context.Gerentes.Select(g => new
                {
                    g.Nome,
                    g.Email,
                    g.Setor,
                    g.Telefone
                }).ToListAsync();
                return Ok(gerentes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar gerentes.");
                return StatusCode(500, "Ocorreu um erro ao listar os gerentes.");
            }
        }

        [HttpGet("ObterPorEmail/{email}")]
        public async Task<IActionResult> ObterUsuarioPorEmail(string email)
        {
            try
            {
                var gerente = await _context.Gerentes
                    .Where(g => g.Email == email)
                    .Select(g => new
                    {
                        g.Nome,
                        g.Email,
                        g.Setor,
                        g.Telefone
                    })
                    .FirstOrDefaultAsync();
                if (gerente == null)
                    return NotFound("Gerente não encontrado.");
                return Ok(gerente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao obter gerente com email {email}.");
                return StatusCode(500, "Ocorreu um erro ao obter o gerente.");
            }
        }


        [HttpPut("Editar/{id}")]
        public async Task<IActionResult> EditarGerente(Guid id, [FromBody] EditarDto request)
        {
            try
            {
                var gerente = await _context.Gerentes.FindAsync(id);
                if (gerente == null)
                    return NotFound("Gerente não encontrado.");

                if (!string.IsNullOrEmpty(request.Email) && request.Email != gerente.Email)
                {
                    var emailExists = await _context.Gerentes.AnyAsync(g => g.Email == request.Email);
                    if (emailExists)
                        return BadRequest("Email já está em uso por outro gerente.");
                }

                gerente.Nome = request.Nome ?? gerente.Nome;
                gerente.Email = request.Email ?? gerente.Email;
                if (!string.IsNullOrEmpty(request.Senha))
                {
                    gerente.Senha = BCrypt.Net.BCrypt.HashPassword(request.Senha);
                }
                gerente.Setor = request.Setor ?? gerente.Setor;
                gerente.Telefone = request.Telefone ?? gerente.Telefone;

                await _context.SaveChangesAsync();
                return Ok("Gerente atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao editar gerente com id {id}.");
                return StatusCode(500, "Ocorreu um erro ao editar o gerente.");
            }
        }

        [HttpDelete("Excluir/{id}")]
        public async Task<IActionResult> DeletarGerente(Guid id)
        {
            try
            {
                var gerente = await _context.Gerentes.FindAsync(id);
                if (gerente == null)
                    return NotFound("Gerente não encontrado.");

                _context.Gerentes.Remove(gerente);
                await _context.SaveChangesAsync();
                return Ok("Gerente deletado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao deletar gerente com id {id}.");
                return StatusCode(500, "Ocorreu um erro ao deletar o gerente.");
            }
        }
    }
}
