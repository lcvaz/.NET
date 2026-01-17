using Domain.Entidades.Usuario;

namespace Application.Interfaces;

/// <summary>
/// Interface para o serviço de geração de tokens JWT
/// </summary>
public interface IJwtService
{
    string GenerateToken(Usuario usuario, bool rememberMe);
    int GetExpirationTime(bool rememberMe);
}
