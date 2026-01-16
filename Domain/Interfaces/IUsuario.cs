using Domain.Entidades.Usuario;

namespace Interfaces.IUsuario;

/// <summary>
/// Interface para o repositório de usuários
/// Define os métodos de acesso a dados para a entidade Usuario
/// </summary>
public interface IUsuario
{
    Task<IEnumerable<Usuario>> GetAllUsuariosAsync();
    Task<Usuario> GetUsuarioByIdAsync(int id);
    Task<Usuario> GetUsuarioByEmailAsync(string email);
    Task AddUsuarioAsync(Usuario usuario);
    Task UpdateUsuarioAsync(Usuario usuario);
    Task DeleteUsuarioAsync(int id);
}