using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using _NET.Data;
using _NET.DTOs;
using _NET.Models;
using BCrypt.Net;

namespace _NET.Services;

public class UsuarioService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public UsuarioService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<LoginResponseDTO?> LoginAsync(LoginDTO loginDto)
    {
        // Buscar usuário por email
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

        // Verificar se usuário existe
        if (usuario == null)
        {
            return null;
        }

        // Verificar senha com BCrypt
        bool senhaValida = BCrypt.Net.BCrypt.Verify(loginDto.Senha, usuario.SenhaHash);
        if (!senhaValida)
        {
            return null;
        }

        // Gerar token JWT
        string token = GerarTokenJWT(usuario);

        // Retornar resposta com o token
        return new LoginResponseDTO
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Token = token
        };
    }

    public async Task<Usuario?> CadastrarAsync(CadastroDTO cadastroDto)
    {
        // Verificar se email já existe
        var usuarioExistente = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == cadastroDto.Email);

        if (usuarioExistente != null)
        {
            return null;
        }

        // Hash da senha com BCrypt
        string senhaHash = BCrypt.Net.BCrypt.HashPassword(cadastroDto.Senha);

        // Criar novo usuário
        var novoUsuario = new Usuario
        {
            Nome = cadastroDto.Nome,
            Email = cadastroDto.Email,
            SenhaHash = senhaHash,
            CriadoEm = DateTime.UtcNow,
            AtualizadoEm = DateTime.UtcNow
        };

        // Adicionar ao banco
        _context.Usuarios.Add(novoUsuario);
        await _context.SaveChangesAsync();

        return novoUsuario;
    }

    private string GerarTokenJWT(Usuario usuario)
    {
        var jwtKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key não configurada");
        var jwtIssuer = _configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT Issuer não configurado");
        var jwtAudience = _configuration["Jwt:Audience"] ?? throw new InvalidOperationException("JWT Audience não configurado");

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
            new Claim(JwtRegisteredClaimNames.Name, usuario.Nome),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
