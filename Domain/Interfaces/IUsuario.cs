using Domain.DTOs;
using Domain.Entidades.Usuario;

namespace Interfaces.IUsuario;

/// <summary>
/// Interface para o repositório de usuários
/// Define os métodos de acesso a dados para a entidade Usuario
/// </summary>
public interface IUsuario
{
    Task<RepositoryResponseDto<Usuario>> LoginAsync(string email, string senha, bool rememberMe);
    Task<RepositoryResponseDto<Usuario>> CadastrarAsync(string nome, string email, string senha);
    Task<RepositoryResponseDto<IEnumerable<Usuario>>> GetAllUsuariosAsync();
    Task<RepositoryResponseDto<Usuario>> GetUsuarioByIdAsync(int id);
    Task<RepositoryResponseDto<Usuario>> GetUsuarioByEmailAsync(string email);
    Task<RepositoryResponseDto<Usuario>> AddUsuarioAsync(Usuario usuario);
    Task<RepositoryResponseDto<Usuario>> UpdateUsuarioAsync(Usuario usuario);
    Task<RepositoryResponseDto<bool>> DeleteUsuarioAsync(int id);
}
