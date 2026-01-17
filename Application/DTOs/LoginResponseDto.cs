namespace Application.DTOs;

/// <summary>
/// DTO de resposta do login com token JWT
/// </summary>
public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;
    public UsuarioDto Usuario { get; set; } = new();
    public int ExpiresIn { get; set; }
    public int Status { get; set; }
}

/// <summary>
/// DTO com dados do usuário para resposta
/// </summary>
public class UsuarioDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DataCadastro { get; set; }
}
