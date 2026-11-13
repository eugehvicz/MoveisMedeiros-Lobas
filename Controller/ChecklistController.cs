
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoveisMedeiros.Api.Data;
using MoveisMedeiros.Api.DTOs;
using MoveisMedeiros.Api.Models;

namespace MoveisMedeiros.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChecklistController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ChecklistController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateChecklist(
            CriarChecklistDto checklistDTO)
        {
            var inspecao = await _context.Inspecoes
                .FindAsync(checklistDTO.InspecaoId);

            if (inspecao == null)
                return BadRequest("Inspeção não encontrada.");

            var checklist = new Checklist
            {
                Chave = checklistDTO.Chave,
                Descricao = checklistDTO.Descricao,
                Resultado = checklistDTO.Resultado,
                DescricaoProblema = checklistDTO.DescricaoProblema,
                UrlImagem = checklistDTO.UrlImagem,
                InspecaoId = checklistDTO.InspecaoId
            };

            _context.Checklist.Add(checklist);

            await _context.SaveChangesAsync();

            return Ok("Item do checklist cadastrado com sucesso!");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChecklistDto>>>
            GetChecklists()
        {
            var checklists = await _context.Checklist
                .Select(c => new ChecklistDto
                {
                    Id = c.Id,
                    Chave = c.Chave,
                    Descricao = c.Descricao,
                    Resultado = c.Resultado,
                    DescricaoProblema = c.DescricaoProblema,
                    UrlImagem = c.UrlImagem,
                    InspecaoId = c.InspecaoId
                })
                .ToListAsync();

            return Ok(checklists);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateChecklist(
            int id, CriarChecklistDto checklistDTO)
        {
            var checklist = await _context.Checklist
                .FindAsync(id);

            if (checklist == null)
                return NotFound("Item do checklist não encontrado.");

            var inspecao = await _context.Inspecoes
                .FindAsync(checklistDTO.InspecaoId);

            if (inspecao == null)
                return BadRequest("Inspeção não encontrada.");

            checklist.Chave = checklistDTO.Chave;
            checklist.Descricao = checklistDTO.Descricao;
            checklist.Resultado = checklistDTO.Resultado;
            checklist.DescricaoProblema = checklistDTO.DescricaoProblema;
            checklist.UrlImagem = checklistDTO.UrlImagem;
            checklist.InspecaoId = checklistDTO.InspecaoId;

            await _context.SaveChangesAsync();

            return Ok("Checklist atualizado com sucesso!");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteChecklist(int id)
        {
            var checklist = await _context.Checklist
                .FindAsync(id);

            if (checklist == null)
                return NotFound("Item do checklist não encontrado.");

            _context.Checklist.Remove(checklist);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

