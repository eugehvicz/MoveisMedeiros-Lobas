
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoveisMedeiros.Api.Data;
using MoveisMedeiros.Api.DTOs;
using MoveisMedeiros.Api.Models;

namespace MoveisMedeiros.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DocumentoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDocumento(
            CriarDocumentoDto documentoDTO)
        {
            var obra = await _context.Obras
                .FindAsync(documentoDTO.ObraId);

            if (obra == null)
                return BadRequest("Obra não encontrada.");

            var documento = new Documento
            {
                Tipo = documentoDTO.Tipo,
                Arquivo = documentoDTO.Arquivo,
                Data = documentoDTO.Data,
                ObraId = documentoDTO.ObraId
            };

            _context.Documentos.Add(documento);

            await _context.SaveChangesAsync();

            return Ok("Documento cadastrado com sucesso!");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentoDto>>>
            GetDocumentos()
        {
            var documentos = await _context.Documentos
                .Include(d => d.Obra)
                .Select(d => new DocumentoDto
                {
                    Id = d.Id,
                    Tipo = d.Tipo,
                    Arquivo = d.Arquivo,
                    Data = d.Data,
                    ObraId = d.ObraId,
                    NomeObra = d.Obra!.Nome
                })
                .ToListAsync();

            return Ok(documentos);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDocumento(
            int id, CriarDocumentoDto documentoDTO)
        {
            var documento = await _context.Documentos
                .FindAsync(id);

            if (documento == null)
                return NotFound("Documento não encontrado.");

            var obra = await _context.Obras
                .FindAsync(documentoDTO.ObraId);

            if (obra == null)
                return BadRequest("Obra não encontrada.");

            documento.Tipo = documentoDTO.Tipo;
            documento.Arquivo = documentoDTO.Arquivo;
            documento.Data = documentoDTO.Data;
            documento.ObraId = documentoDTO.ObraId;

            await _context.SaveChangesAsync();

            return Ok("Documento atualizado com sucesso!");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDocumento(int id)
        {
            var documento = await _context.Documentos
                .FindAsync(id);

            if (documento == null)
                return NotFound("Documento não encontrado.");

            _context.Documentos.Remove(documento);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

