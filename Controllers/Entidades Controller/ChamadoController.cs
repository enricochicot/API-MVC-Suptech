using API_MVC_Suptech.Data;
using API_MVC_Suptech.Entitys;
using API_MVC_Suptech.Entitys.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API_MVC_Suptech.Controllers.Entidades_Controller
{
    [Authorize]
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
            _logger.LogInformation("Iniciando o processo de adição de um novo chamado.");
            try
            {
                var statusPermitido = new[] { "Aberto", "Pendente", "Fechado" };

                if (!statusPermitido.Contains(request.Status))
                {
                    return BadRequest("Status inválido.");
                }

                var prioridadePermitida = new[] { "Baixa", "Média", "Alta" };
                if (!prioridadePermitida.Contains(request.Prioridade))
                {
                    return BadRequest("Prioridade inválida.");
                }

                var novoChamado = new Chamado
                {
                    NomeDoUsuario = request.NomeDoUsuario,
                    EmailDoUsuario = request.EmailDoUsuario,
                    SetorDoUsuario = request.SetorDoUsuario,
                    Titulo = request.Titulo,
                    Descricao = request.Descricao,
                    Prioridade = request.Prioridade,
                    Status = request.Status,
                    Resposta = request.Resposta
                };
                _context.Chamados.Add(novoChamado);
                await _context.SaveChangesAsync();
                return Ok("Chamado adicionado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar chamado.");
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("ListarChamados")]
        public async Task<IActionResult> ListarChamados()
        {
            try
            {
                var chamados = await _context.Chamados.ToListAsync();
                return Ok(chamados);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar chamados.");
                return StatusCode(500, "Ocorreu um erro ao listar os chamados.");
            }
        }

        //Buscar chamados baseado no valor que for recebido 
        [HttpGet("BuscarChamados")]
        public async Task<IActionResult> BuscarChamados([FromQuery] string valor)
        {
            try
            {
                var chamados = await _context.Chamados
                    .Where(c =>
                        c.NomeDoUsuario.Contains(valor) ||
                        c.EmailDoUsuario.Contains(valor) ||
                        c.SetorDoUsuario.Contains(valor) ||
                        c.Titulo.Contains(valor) ||
                        c.Descricao.Contains(valor) ||
                        c.Prioridade.Contains(valor) ||
                        c.Status.Contains(valor) ||
                        (c.Resposta != null && c.Resposta.Contains(valor))
                    )
                    .ToListAsync();
                return Ok(chamados);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar chamados.");
                return StatusCode(500, "Ocorreu um erro ao buscar os chamados.");
            }
        }


        [HttpPut("Editar/{id}")]
        public async Task<IActionResult> EditarChamado(Guid id, [FromBody] EditarChamadoDto request)
        {
            _logger.LogInformation($"Iniciando o processo de edição do chamado com id.");
            try
            {
                var chamado = await _context.Chamados.FindAsync(id);
                if (chamado == null)
                {
                    return NotFound("Chamado não encontrado.");
                }

                // Validação do status, se fornecido 
                if (!string.IsNullOrEmpty(request.Status))
                {
                    var statusPermitido = new[] { "Aberto", "Pendente", "Fechado" };
                    if (!statusPermitido.Contains(request.Status))
                    {
                        return BadRequest("Status inválido.");
                    }
                    chamado.Status = request.Status;
                }

                // Validação da prioridade, se fornecida
                if (!string.IsNullOrEmpty(request.Prioridade))
                {
                    var prioridadePermitida = new[] { "Baixa", "Média", "Alta" };
                    if (!prioridadePermitida.Contains(request.Prioridade))
                    {
                        return BadRequest("Prioridade inválida.");
                    }
                    chamado.Prioridade = request.Prioridade;
                }

                chamado.NomeDoUsuario = request.NomeDoUsuario ?? chamado.NomeDoUsuario;
                chamado.EmailDoUsuario = request.EmailDoUsuario ?? chamado.EmailDoUsuario;
                chamado.SetorDoUsuario = request.SetorDoUsuario ?? chamado.SetorDoUsuario;
                chamado.Titulo = request.Titulo ?? chamado.Titulo;
                chamado.Descricao = request.Descricao ?? chamado.Descricao;
                chamado.Prioridade = request.Prioridade ?? chamado.Prioridade;
                chamado.Status = request.Status ?? chamado.Status;
                chamado.Resposta = request.Resposta ?? chamado.Resposta;
                await _context.SaveChangesAsync();
                return Ok("Chamado editado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao editar chamado com id {id}.");
                return StatusCode(500, "Ocorreu um erro ao editar o chamado.");
            }
        }


        [HttpDelete("Excluir/{id}")]
        public async Task<IActionResult> ExcluirChamado(Guid id)
        {
            _logger.LogInformation($"Iniciando o processo de exclusão do chamado com id.");
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao excluir chamado com id {id}.");
                return StatusCode(500, "Ocorreu um erro ao excluir o chamado.");
            }
        }
    }
}
