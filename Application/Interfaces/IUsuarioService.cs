using Application.DTOs;

namespace Interfaces.IUsuarioService;

/// <summary>
/// Interface para o serviço de usuários
/// Define as operações de negócio para a entidade Usuario
/// </summary>
public interface IUsuarioService
{
    Task<ApiResponseDto<LoginResponseDto>> LoginAsync(string email, string senha, bool rememberMe);
    Task<ApiResponseDto<CadastroDto>> CadastrarAsync(string nome, string email, string senha);
}
