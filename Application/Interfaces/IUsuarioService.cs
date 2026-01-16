namespace Interfaces.IUsuarioService;

/// <summary>
/// Interface para o serviço de usuários
/// Define as operações de negócio para a entidade Usuario
/// </summary>
public interface IUsuarioService
{
    Task<UsuarioLoginDto> LoginAsync(string email, string senha, bool rememberMe);  
    Task<UsuarioCadastroDto> CadastrarAsync(string nome, string email, string senha); 
}