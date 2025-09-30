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
    public class AdministradorController : ControllerBase

    {

        private readonly CrudData _context;
        private readonly IConfiguration _configuration;
        private readonly TokenService _tokenService;
        public AdministradorController(CrudData context, IConfiguration configuration, TokenService tokenService)

        {
            _context = context;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        [HttpPost("Adicionar")]
        public async Task<IActionResult> AdicionarAdministrador([FromBody] NovoAdministradorDto request)
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
                Senha = request.Senha,
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


        [HttpGet("Listar")]
        public async Task<IActionResult> ListarAdministradores()
        {
            List<Administrador> administradores = await _context.Administradores.ToListAsync();
            return Ok(administradores);
        }


        [HttpPut("Editar/{id}")]
        public async Task<IActionResult> EditarAdministrador(Guid id, [FromBody] EditarDto request)
        {
            var administrador = await _context.Administradores.FindAsync(id);
            if (administrador == null)
            {
                return NotFound("Administrador não encontrado.");
            }
            administrador.Nome = request.Nome ?? administrador.Nome;
            administrador.Email = request.Email ?? administrador.Email;
            administrador.Senha = request.Senha ?? administrador.Senha;
            administrador.Setor = request.Setor ?? administrador.Setor;
            if (request.Telefone.HasValue)
            {
                administrador.Telefone = request.Telefone.Value.ToString();
            }
            await _context.SaveChangesAsync();
            return Ok("Administrador atualizado com sucesso!");
        }


        [HttpDelete("Excluir")]
        public async Task<IActionResult> DeletarAdministrador(Guid id)
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
    }
}