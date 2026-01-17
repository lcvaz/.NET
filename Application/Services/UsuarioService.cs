using Application.DTOs;
using Domain.Entidades.Usuario;
using Interfaces.IUsuario;
using Interfaces.IUsuarioService;

namespace Application.Services;

/// <summary>
/// Serviço de usuários
/// Implementa a lógica de negócio para operações de usuário
/// </summary>
public class UsuarioService : IUsuarioService
{
    private readonly IUsuario _usuarioRepository;

    public UsuarioService(IUsuario usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<LoginDto> LoginAsync(string email, string senha, bool rememberMe)
    {
        var usuario = await _usuarioRepository.GetUsuarioByEmailAsync(email);

        if (usuario == null)
            return null;

        if (!BCrypt.Net.BCrypt.Verify(senha, usuario.SenhaHash))
            return null;

        return new LoginDto
        {
            Email = usuario.Email,
            Senha = string.Empty
        };
    }

    public async Task<CadastroDto> CadastrarAsync(string nome, string email, string senha)
    {
        var usuarioExistente = await _usuarioRepository.GetUsuarioByEmailAsync(email);

        if (usuarioExistente != null)
            return null;

        var usuario = new Usuario
        {
            Nome = nome,
            Email = email,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(senha),
            DataCriacao = DateTime.UtcNow
        };

        await _usuarioRepository.AddUsuarioAsync(usuario);

        return new CadastroDto
        {
            Nome = usuario.Nome,
            Email = usuario.Email,
            Senha = string.Empty
        };
    }
}
