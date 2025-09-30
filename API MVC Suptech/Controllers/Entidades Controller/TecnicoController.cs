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
    public class TecnicoController : Controller
    {
        private readonly CrudData _context;
        private readonly IConfiguration _configuration;
        private readonly TokenService _tokenService;

        // Construtor para injeção de dependência
        public TecnicoController(CrudData context, IConfiguration configuration, TokenService tokenService)
        {
            _context = context;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        [HttpPost("Adicionar")]
        public async Task<IActionResult> AdicionarTecnico([FromBody] NovoTecnicoDto request)
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
                Senha = request.Senha, // Em um cenário real, a senha deve ser hashada antes de ser armazenada.
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

    }
}
