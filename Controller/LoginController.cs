using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MoveisMedeiros.Api.Data;
using MoveisMedeiros.Api.DTOs;
using MoveisMedeiros.Api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context; 
    private readonly IConfiguration _configuration;

    private readonly PasswordHasher<Usuario> _hasher;

    public AuthController(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
        _hasher = new PasswordHasher<Usuario>();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto request)
    {

        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == request.Email);

        // Verifica se o usuário existe e se a senha está correta com _hasher;
        var passwordVerificationResult = _hasher.VerifyHashedPassword(usuario, usuario?.SenhaHash, request.Senha);


        if (!(usuario != null && passwordVerificationResult == PasswordVerificationResult.Success))
        {

            return Unauthorized(new { mensagem = "E-mail ou senha inválidos." });
        }

        var token = GerarTokenJwt(usuario);


        return Ok(new
        {
            token = token,
            usuario = new
            {
                usuario.Id,
                usuario.Email
            }
        });
    }

    private string GerarTokenJwt(Usuario usuario)
    {
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(2), 
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
