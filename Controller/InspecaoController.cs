
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoveisMedeiros.Api.Data;
using MoveisMedeiros.Api.DTOs;
using MoveisMedeiros.Api.Models;

namespace MoveisMedeiros.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InspecaoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InspecaoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateInspecao(
            CriarInspecaoDto inspecaoDTO)
        {
            var obra = await _context.Obras
                .FindAsync(inspecaoDTO.ObraId);

            if (obra == null)
                return BadRequest("Obra não encontrada.");

            var inspecao = new Inspecao
            {
                InspecaoKey = inspecaoDTO.InspecaoKey,
                Status = inspecaoDTO.Status,
                Observacoes = inspecaoDTO.Observacoes,
                ObraId = inspecaoDTO.ObraId,
                FurnitureId = inspecaoDTO.FurnitureId
            };

            _context.Inspecoes.Add(inspecao);

            await _context.SaveChangesAsync();

            return Ok("Inspeção cadastrada com sucesso!");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InspecaoDto>>>
            GetInspecoes()
        {
            var inspecoes = await _context.Inspecoes
                .Include(i => i.Obra)
                .Select(i => new InspecaoDto
                {
                    Id = i.Id,
                    InspecaoKey = i.InspecaoKey,
                    Status = i.Status,
                    Observacoes = i.Observacoes,
                    ObraId = i.ObraId,
                    NomeObra = i.Obra!.Nome,
                    FurnitureId = i.FurnitureId
                })
                .ToListAsync();

            return Ok(inspecoes);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateInspecao(
            int id, CriarInspecaoDto inspecaoDTO)
        {
            var inspecao = await _context.Inspecoes
                .FindAsync(id);

            if (inspecao == null)
                return NotFound("Inspeção não encontrada.");

            var obra = await _context.Obras
                .FindAsync(inspecaoDTO.ObraId);

            if (obra == null)
                return BadRequest("Obra não encontrada.");

            inspecao.InspecaoKey = inspecaoDTO.InspecaoKey;
            inspecao.Status = inspecaoDTO.Status;
            inspecao.Observacoes = inspecaoDTO.Observacoes;
            inspecao.ObraId = inspecaoDTO.ObraId;
            inspecao.FurnitureId = inspecaoDTO.FurnitureId;

            await _context.SaveChangesAsync();

            return Ok("Inspeção atualizada com sucesso!");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteInspecao(int id)
        {
            var inspecao = await _context.Inspecoes
                .Include(i => i.Checklist)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (inspecao == null)
                return NotFound("Inspeção não encontrada.");

            _context.Inspecoes.Remove(inspecao);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

