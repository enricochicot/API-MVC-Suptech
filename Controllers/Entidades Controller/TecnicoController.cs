using API_MVC_Suptech.Data;
using API_MVC_Suptech.Entitys;
using API_MVC_Suptech.Entitys.Dtos;
using API_MVC_Suptech.Services;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace API_MVC_Suptech.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TecnicoController : ControllerBase
    {
        private readonly CrudData _context;
        private readonly IConfiguration _configuration;
        private readonly TokenService _tokenService;
        private readonly ILogger<TecnicoController> _logger;

        public TecnicoController(CrudData context, IConfiguration configuration, TokenService tokenService, ILogger<TecnicoController> logger)
        {
            _context = context;
            _configuration = configuration;
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpPost("Adicionar")]
        public async Task<IActionResult> AdicionarTecnico([FromBody] NovoTecnicoDto request)
        {
            _logger.LogInformation("Iniciando o processo de adição de um novo técnico.");
            try
            {
                var existingTecnico = await _context.Tecnicos.FirstOrDefaultAsync(t => t.Email == request.Email);
                if (existingTecnico != null)
                {
                    return BadRequest("Email já está em uso.");
                }
                var tecnico = new Tecnico
                {
                    Nome = request.Nome,
                    Email = request.Email,
                    Senha = BCrypt.Net.BCrypt.HashPassword(request.Senha),
                    Especialidade = request.Especialidade,
                    Telefone = request.Telefone
                };
                if (string.IsNullOrEmpty(tecnico.Nome) || string.IsNullOrEmpty(tecnico.Email)
                   || string.IsNullOrEmpty(tecnico.Senha))
                {
                    return BadRequest("Nome, Email e Senha são obrigatórios.");
                }
                _context.Tecnicos.Add(tecnico);
                await _context.SaveChangesAsync();
                return Ok("Técnico adicionado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar técnico.");
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("ListarTecnicosDesktop")]
        public async Task<IActionResult> ListarTecnicosDesktop()
        {
            try
            {
                var tecnicos = await _context.Tecnicos.ToListAsync();
                return Ok(tecnicos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar técnicos.");
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("ListarTecnicosWeb")]
        public async Task<IActionResult> ListarTecnicos()
        {
            try
            {
                var tecnicos = await _context.Tecnicos.Select(t => new
                { 
                    t.Nome,
                    t.Email,
                    t.Especialidade,
                    t.Telefone
                }).ToListAsync();
                return Ok(tecnicos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar técnicos.");
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("ObterPorEmail/{email}")]
        public async Task<IActionResult> ObterTecnicoPorEmail(string email)
        {
            try
            {
                var tecnico = await _context.Tecnicos
                    .Where(t => t.Email == email)
                    .Select(t => new
                    {
                        t.Nome,
                        t.Email,
                        t.Especialidade,
                        t.Telefone
                    })
                    .FirstOrDefaultAsync();

                //metodo para lidar com (tecnico = null), implementado no frontend

                return Ok(tecnico);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter técnico por email.");
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPut("Editar/{id}")]
        public async Task<IActionResult> EditarTecnico(Guid id, [FromBody] NovoTecnicoDto request)
        {
            _logger.LogInformation("Iniciando o processo de edição do técnico.");
            try
            { 
                var tecnico = await _context.Tecnicos.FindAsync(id);
                if (tecnico == null)
                {
                    return NotFound("Técnico não encontrado.");
                }

                if(tecnico.Email != request.Email)
                {
                    var emailExists = await _context.Tecnicos.AnyAsync(t => t.Email == request.Email);
                    if (emailExists)
                    {
                        return BadRequest("Email já está em uso por outro técnico.");
                    }
                    }

                    tecnico.Nome = request.Nome;
                tecnico.Email = request.Email;
                if (!string.IsNullOrEmpty(request.Senha))
                {
                    tecnico.Senha = BCrypt.Net.BCrypt.HashPassword(request.Senha);
                }
                tecnico.Especialidade = request.Especialidade;
                tecnico.Telefone = request.Telefone;

                if (string.IsNullOrEmpty(tecnico.Nome) || string.IsNullOrEmpty(tecnico.Email)
                || string.IsNullOrEmpty(tecnico.Senha))
                {
                    return BadRequest("Nome, Email e Senha são obrigatórios.");
                }
                _context.Tecnicos.Update(tecnico);
                await _context.SaveChangesAsync();
                return Ok("Técnico atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao editar técnico.");
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpDelete("Excluir/{id}")]
        public async Task<IActionResult> DeletarTecnico(Guid id)
        {
            _logger.LogInformation("Iniciando o processo de deleção do técnico.");
            try
            {
                var tecnico = await _context.Tecnicos.FindAsync(id);
                if (tecnico == null)
                {
                    return NotFound("Técnico não encontrado.");
                }
                _context.Tecnicos.Remove(tecnico);
                await _context.SaveChangesAsync();
                return Ok("Técnico deletado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar técnico.");
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }
    }
}
