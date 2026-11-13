
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoveisMedeiros.Api.Data;
using MoveisMedeiros.Api.DTOs;
using MoveisMedeiros.Api.Models;
using MoveisMedeiros.DTOs;

namespace MoveisMedeiros.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClienteController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCliente(
            CriarClienteDTO clienteDTO)
        {
            var cliente = new Cliente
            {
                Nome = clienteDTO.Nome,
                Endereco = clienteDTO.Endereco,
                Telefone = clienteDTO.Telefone,
                Email = clienteDTO.Email
            };

            _context.Clientes.Add(cliente);

            await _context.SaveChangesAsync();

            return Ok("Cliente cadastrado com sucesso!");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDto>>>
            GetClientes()
        {
            var clientes = await _context.Clientes
                .Select(c => new ClienteDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Endereco = c.Endereco,
                    Telefone = c.Telefone,
                    Email = c.Email
                })
                .ToListAsync();

            return Ok(clientes);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCliente(
            int id, CriarClienteDTO clienteDTO)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
                return NotFound("Cliente não encontrado.");

            cliente.Nome = clienteDTO.Nome;
            cliente.Endereco = clienteDTO.Endereco;
            cliente.Telefone = clienteDTO.Telefone;
            cliente.Email = clienteDTO.Email;

            await _context.SaveChangesAsync();

            return Ok("Cliente atualizado com sucesso!");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Obras)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente == null)
                return NotFound("Cliente não encontrado.");

            if (cliente.Obras.Any())
                return BadRequest("Não é possível excluir um cliente que possui obras.");

            _context.Clientes.Remove(cliente);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
