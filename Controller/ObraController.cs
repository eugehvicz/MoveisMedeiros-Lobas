
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoveisMedeiros.Api.Data;
using MoveisMedeiros.Api.DTOs;
using MoveisMedeiros.Api.Models;

namespace MoveisMedeiros.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ObraController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ObraController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateObra(
            CriarObraDto obraDTO)
        {
            var cliente = await _context.Clientes
                .FindAsync(obraDTO.ClienteId);

            if (cliente == null)
                return BadRequest("Cliente não encontrado.");

            var obra = new Obra
            {
                Nome = obraDTO.Nome,
                Endereco = obraDTO.Endereco,
                Status = obraDTO.Status,
                NomeCliente = cliente.Nome,
                ClienteId = obraDTO.ClienteId
            };

            //Adicionar lista de itens à obra
            foreach (var item in obraDTO.Itens)
            {
                var novoItem = new ItemObra
                {
                    NomeMovel = item.NomeMovel,
                   Tipo = item.Tipo,
                    Altura = item.Altura,
                    Largura = item.Largura,
                    Material = item.Material,
                    Ambiente = item.Ambiente,

                    Inspecoes = new List<Inspecao>
                    {
                        new Inspecao
                        {
                            InspecaoKey = "",
                            Status = "teste",
                            Observacoes = "teste",
                            FurnitureId = "testeeee",
                            Checklist = new List<Checklist>
                            {
                                new Checklist
                                {
                                    Descricao = "Medidas confe",
                                    
                                },
                                new Checklist
                                {
                                    Descricao = "Medidas mont",

                                },
                                new Checklist
                                {
                                    Descricao = "Medidas entrega",

                                },
                                new Checklist
                                {
                                    Descricao = "Medidas final",
                                }
                            }

                        }
                    }
                };
                obra.Itens.Add(novoItem);
            }


            _context.Obras.Add(obra);

            await _context.SaveChangesAsync();

            return Ok("Obra cadastrada com sucesso!");
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ObraDto>> GetObra(int id)
        {
            var obra = await _context.Obras
                .Include(o => o.Cliente)
                .Select(o => new ObraDto
                {
                    Id = o.Id,
                    Nome = o.Nome,
                    Endereco = o.Endereco,
                    NomeCliente = o.Cliente!.Nome
                })
                .FirstOrDefaultAsync(o => o.Id == id);

            if (obra == null)
                return NotFound("Obra não encontrada.");

            return Ok(obra);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Obra>>>
            GetObras()
        {
            var obras = await _context.Obras
                .Include(o => o.Cliente)
                .Include(o => o.Itens)
                .ToListAsync();

            return Ok(obras);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateObra(
            int id, CriarObraDto obraDTO)
        {
            var obra = await _context.Obras.FindAsync(id);

            if (obra == null)
                return NotFound("Obra não encontrada.");

            var cliente = await _context.Clientes
                .FindAsync(obraDTO.ClienteId);

            if (cliente == null)
                return BadRequest("Cliente não encontrado.");

            obra.Nome = obraDTO.Nome;
            obra.Endereco = obraDTO.Endereco;
            obra.Status = obraDTO.Status;
            obra.ClienteId = obraDTO.ClienteId;
            obra.NomeCliente = cliente.Nome;

            await _context.SaveChangesAsync();

            return Ok("Obra atualizada com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteObra(int id)
        {
            var obra = await _context.Obras
                .Include(o => o.Itens)
                .Include(o => o.Documentos)
                .Include(o => o.Historicos)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (obra == null)
                return NotFound("Obra não encontrada.");

            if (obra.Itens.Any() ||
                obra.Documentos.Any() ||
                obra.Historicos.Any())
            {
                return BadRequest(
                    "Não é possível excluir uma obra que possui registros vinculados.");
            }

            _context.Obras.Remove(obra);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

