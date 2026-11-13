
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MoveisMedeiros.Api.Data;
using MoveisMedeiros.Api.DTOs;
using MoveisMedeiros.Api.Models;

namespace MoveisMedeiros.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<Usuario> _hasher;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
            _hasher = new PasswordHasher<Usuario>();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsuario(
            CriarUsuarioDto usuarioDTO)
        {
            var existe = await _context.Usuarios
                .AnyAsync(u => u.Email == usuarioDTO.Email);

            if (existe)
                return BadRequest("Email já cadastrado.");

            var usuario = new Usuario
            {
                Nome = usuarioDTO.Nome,
                Telefone = usuarioDTO.Telefone,
                Email = usuarioDTO.Email,
                Perfil = usuarioDTO.Perfil,
                Ativo = true
            };

            usuario.SenhaHash = _hasher.HashPassword(
                usuario, usuarioDTO.Senha);

            _context.Usuarios.Add(usuario);

            await _context.SaveChangesAsync();

            return Ok("Usuário cadastrado com sucesso!");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>>
            GetUsuarios()
        {
            var usuarios = await _context.Usuarios
                .Select(u => new UsuarioDto
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email,
                    Perfil = u.Perfil,
                    Ativo = u.Ativo
                })
                .ToListAsync();

            return Ok(usuarios);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUsuario(
            int id, CriarUsuarioDto usuarioDTO)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            var existe = await _context.Usuarios
                .AnyAsync(u => u.Email == usuarioDTO.Email && u.Id != id);

            if (existe)
                return BadRequest("Email já cadastrado.");

            usuario.Nome = usuarioDTO.Nome;
            usuario.Telefone = usuarioDTO.Telefone;
            usuario.Email = usuarioDTO.Email;
            usuario.Perfil = usuarioDTO.Perfil;

            if (!string.IsNullOrWhiteSpace(usuarioDTO.Senha))
            {
                usuario.SenhaHash = _hasher.HashPassword(
                    usuario, usuarioDTO.Senha);
            }

            await _context.SaveChangesAsync();

            return Ok("Usuário atualizado com sucesso!");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Obras)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            if (usuario.Obras.Any())
                return BadRequest("Não é possível excluir um usuário vinculado a obras.");

            var possuiHistorico = await _context.Historicos
                .AnyAsync(h => h.UsuarioId == id);

            if (possuiHistorico)
            {
                usuario.Ativo = false;
                await _context.SaveChangesAsync();

                return Ok("Usuário desativado, pois possui histórico.");
            }

            _context.Usuarios.Remove(usuario);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

