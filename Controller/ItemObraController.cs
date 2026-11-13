
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoveisMedeiros.Api.Data;
using MoveisMedeiros.Api.DTOs;
using MoveisMedeiros.Api.Models;

namespace MoveisMedeiros.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemObraController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemObraController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateItemObra(
            CriarItemObraDto itemDTO)
        {
            var obra = await _context.Obras
                .FindAsync(itemDTO.ObraId);

            if (obra == null)
                return BadRequest("Obra não encontrada.");

            var item = new ItemObra
            {
                NomeMovel = itemDTO.NomeMovel,
                Tipo = itemDTO.Tipo,
                Ambiente = itemDTO.Ambiente,
                Material = itemDTO.Material,
                Altura = itemDTO.Altura,
                Largura = itemDTO.Largura,
                ObraId = itemDTO.ObraId
            };

            _context.ItensObra.Add(item);

            await _context.SaveChangesAsync();

            return Ok("Móvel cadastrado com sucesso!");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemObraDto>>>
            GetItensObra()
        {
            var itens = await _context.ItensObra
                .Select(i => new ItemObraDto
                {
                    Id = i.Id,
                    NomeMovel = i.NomeMovel,
                    Tipo = i.Tipo,
                    Ambiente = i.Ambiente,
                    Material = i.Material,
                    Altura = i.Altura,
                    Largura = i.Largura,
                    ObraId = i.ObraId
                })
                .ToListAsync();

            return Ok(itens);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateItemObra(
            int id, CriarItemObraDto itemDTO)
        {
            var item = await _context.ItensObra.FindAsync(id);

            if (item == null)
                return NotFound("Móvel não encontrado.");

            var obra = await _context.Obras.FindAsync(itemDTO.ObraId);

            if (obra == null)
                return BadRequest("Obra não encontrada.");

            item.NomeMovel = itemDTO.NomeMovel;
            item.Tipo = itemDTO.Tipo;
            item.Ambiente = itemDTO.Ambiente;
            item.Material = itemDTO.Material;
            item.Altura = itemDTO.Altura;
            item.Largura = itemDTO.Largura;
            item.ObraId = itemDTO.ObraId;

            await _context.SaveChangesAsync();

            return Ok("Móvel atualizado com sucesso!");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteItemObra(int id)
        {
            var item = await _context.ItensObra.FindAsync(id);

            if (item == null)
                return NotFound("Móvel não encontrado.");

            _context.ItensObra.Remove(item);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
