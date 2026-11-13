
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoveisMedeiros.Api.Data;
using MoveisMedeiros.Api.DTOs;

namespace MoveisMedeiros.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoricoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HistoricoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoricoDto>>>
            GetHistoricos()
        {
            var historicos = await _context.Historicos
                .Include(h => h.Usuario)
                .Include(h => h.Obra)
                .OrderByDescending(h => h.DataHora)
                .Select(h => new HistoricoDto
                {
                    Id = h.Id,
                    NomeObra = h.Obra != null ? h.Obra.Nome : "Sem obra",
                    NomeUsuario = h.Usuario!.Nome,
                    Acao = h.Acao,
                    DataHora = h.DataHora
                })
                .ToListAsync();

            return Ok(historicos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HistoricoDto>>
            GetHistorico(int id)
        {
            var historico = await _context.Historicos
                .Include(h => h.Usuario)
                .Include(h => h.Obra)
                .Where(h => h.Id == id)
                .Select(h => new HistoricoDto
                {
                    Id = h.Id,
                    NomeObra = h.Obra != null ? h.Obra.Nome : "Sem obra",
                    NomeUsuario = h.Usuario!.Nome,
                    Acao = h.Acao,
                    DataHora = h.DataHora
                })
                .FirstOrDefaultAsync();

            if (historico == null)
                return NotFound("Registro não encontrado.");

            return Ok(historico);
        }
    }
}
