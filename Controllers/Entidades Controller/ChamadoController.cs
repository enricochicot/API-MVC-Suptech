using API_MVC_Suptech.Data;
using API_MVC_Suptech.Entitys;
using API_MVC_Suptech.Entitys.Dtos;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API_MVC_Suptech.Controllers.Entidades_Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChamadoController : Controller
    {
        private readonly CrudData _context;
        private readonly ILogger<ChamadoController> _logger;

        public ChamadoController(CrudData context, ILogger<ChamadoController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("Adicionar")]
        public async Task<IActionResult> AdicionarChamado([FromBody] NovoChamadoDto request)
        {
            var novoChamado = new Chamado
            {
                NomeDoUsuario = request.NomeDoUsuario,
                EmailDoUsuario = request.EmailDoUsuario,
                SetorDoUsuario = request.SetorDoUsuario,
                Titulo = request.Titulo,
                Descricao = request.Descricao,
                Prioridade = request.Prioridade
            };
            _context.Chamados.Add(novoChamado);
            await _context.SaveChangesAsync();
            return Ok("Chamado adicionado com sucesso!");
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> ListarChamados()
        {
            var chamados = await _context.Chamados.ToListAsync();
            return Ok(chamados);
        }

        [HttpPut("Editar/{id}")]
        public async Task<IActionResult> EditarChamado(Guid id, [FromBody] EditarChamadoDto request)
        {
            var chamado = await _context.Chamados.FindAsync(id);
            if (chamado == null)
            {
                return NotFound("Chamado não encontrado.");
            }

            chamado.NomeDoUsuario = request.NomeDoUsuario ?? chamado.NomeDoUsuario;
            chamado.EmailDoUsuario = request.EmailDoUsuario ?? chamado.EmailDoUsuario;
            chamado.SetorDoUsuario = request.SetorDoUsuario ?? chamado.SetorDoUsuario;
            chamado.Titulo = request.Titulo ?? chamado.Titulo;
            chamado.Descricao = request.Descricao ?? chamado.Descricao;
            chamado.Prioridade = request.Prioridade ?? chamado.Prioridade;
            await _context.SaveChangesAsync();
            return Ok("Chamado editado com sucesso!");
        }   


        [HttpDelete("Excluir/{id}")]
        public async Task<IActionResult> ExcluirChamado(Guid id)
        {
            var chamado = await _context.Chamados.FindAsync(id);
            if (chamado == null)
            {
                return NotFound("Chamado não encontrado.");
            }
            _context.Chamados.Remove(chamado);
            await _context.SaveChangesAsync();
            return Ok("Chamado excluído com sucesso!");
        }
    }
}
