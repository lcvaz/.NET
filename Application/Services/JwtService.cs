using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Domain.Entidades.Usuario;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

/// <summary>
/// Serviço para geração de tokens JWT
/// </summary>
public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private const int SHORT_EXPIRATION_HOURS = 1;
    private const int LONG_EXPIRATION_DAYS = 7;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(Usuario usuario, bool rememberMe)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim("rememberMe", rememberMe.ToString())
        };

        var expiration = rememberMe
            ? DateTime.UtcNow.AddDays(LONG_EXPIRATION_DAYS)
            : DateTime.UtcNow.AddHours(SHORT_EXPIRATION_HOURS);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public int GetExpirationTime(bool rememberMe)
    {
        return rememberMe
            ? LONG_EXPIRATION_DAYS * 24 * 60 * 60
            : SHORT_EXPIRATION_HOURS * 60 * 60;
    }
}
