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
    public class GerenteController : Controller
    {
        private readonly CrudData _context;
        private readonly IConfiguration _configuration;
        private readonly TokenService _tokenService;

        public GerenteController(CrudData context, IConfiguration configuration, TokenService tokenService)
        {
            _context = context;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        // Ações CRUD (Create, Read, Update, Delete) para a entidade Gerente podem ser implementadas aqui.
        [HttpPost("Adicionar")]
        public async Task<IActionResult> AdicionarGerente([FromBody] NovoGerenteDto request)
        {
            var existingGerente = await _context.Gerentes.FirstOrDefaultAsync(g => g.Email == request.Email);
            if (existingGerente != null)
            {
                return BadRequest("Email já está em uso.");
            }
            var gerente = new Gerente
            {
                Nome = request.Nome,
                Email = request.Email,
                Senha = request.Senha, // Em um cenário real, a senha deve ser hashada antes de ser armazenada.
                Setor = request.Setor,
                Telefone = request.Telefone
            };
            if (string.IsNullOrEmpty(gerente.Nome) || string.IsNullOrEmpty(gerente.Email)
               || string.IsNullOrEmpty(gerente.Senha))
            {
                return BadRequest("Nome, Email e Senha são obrigatórios.");
            }
            _context.Gerentes.Add(gerente);
            await _context.SaveChangesAsync();
            return Ok("Gerente adicionado com sucesso!");

        }

        [HttpGet("Listar")]
        public async Task<IActionResult> ListarGerentes()
        {
            var gerentes = await _context.Gerentes.ToListAsync();
            return Ok(gerentes);
        }

        [HttpPut("Editar/{id}")]
        public async Task<IActionResult> EditarGerente(Guid id, [FromBody] EditarDto request)
        {
            var gerente = await _context.Gerentes.FindAsync(id);
            if (gerente == null)
            {
                return NotFound("Gerente não encontrado.");
            }
            gerente.Nome = request.Nome ?? gerente.Nome;
            gerente.Email = request.Email ?? gerente.Email;
            gerente.Senha = request.Senha ?? gerente.Senha;
            gerente.Setor = request.Setor ?? gerente.Setor;
            gerente.Telefone = request.Telefone ?? gerente.Telefone;
            await _context.SaveChangesAsync();
            return Ok("Gerente atualizado com sucesso!");
        }

        [HttpDelete("Deletar/{id}")]
        public async Task<IActionResult> DeletarGerente(Guid id)
        {
            var gerente = await _context.Gerentes.FindAsync(id);
            if (gerente == null)
            {
                return NotFound("Gerente não encontrado.");
            }
            _context.Gerentes.Remove(gerente);
            await _context.SaveChangesAsync();
            return Ok("Gerente deletado com sucesso!");
        }
    }
}
