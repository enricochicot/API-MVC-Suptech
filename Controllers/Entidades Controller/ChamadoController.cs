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

        [HttpPost("Adicionar Chamado")]
        public async Task<IActionResult> AdicionarChamado([FromBody] NovoChamado request)
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

        [HttpGet("Listar Chamados")]
        public async Task<IActionResult> ListarChamados()
        {
            var chamados = await _context.Chamados.ToListAsync();
            return Ok(chamados);
        }
    }
}
