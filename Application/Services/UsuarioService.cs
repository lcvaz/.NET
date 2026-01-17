using Application.DTOs;
using Domain.DTOs;
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

    public async Task<ApiResponseDto<LoginDto>> LoginAsync(string email, string senha, bool rememberMe)
    {
        var resultado = await _usuarioRepository.GetUsuarioByEmailAsync(email);

        if (!resultado.Sucesso || resultado.Data == null)
            return ApiResponseDto<LoginDto>.NotFound("Usuário não encontrado");

        if (!BCrypt.Net.BCrypt.Verify(senha, resultado.Data.SenhaHash))
            return ApiResponseDto<LoginDto>.Unauthorized("Email ou senha inválidos");

        var loginDto = new LoginDto
        {
            Email = resultado.Data.Email,
            Senha = string.Empty
        };

        return ApiResponseDto<LoginDto>.Ok(loginDto, "Login realizado com sucesso");
    }

    public async Task<ApiResponseDto<CadastroDto>> CadastrarAsync(string nome, string email, string senha)
    {
        var usuarioExistente = await _usuarioRepository.GetUsuarioByEmailAsync(email);

        if (usuarioExistente.Sucesso && usuarioExistente.Data != null)
            return ApiResponseDto<CadastroDto>.Conflict("Email já cadastrado");

        var resultado = await _usuarioRepository.CadastrarAsync(nome, email, senha);

        if (!resultado.Sucesso || resultado.Data == null)
            return ApiResponseDto<CadastroDto>.InternalServerError(resultado.Mensagem);

        var cadastroDto = new CadastroDto
        {
            Nome = resultado.Data.Nome,
            Email = resultado.Data.Email,
            Senha = string.Empty
        };

        return ApiResponseDto<CadastroDto>.Created(cadastroDto, "Usuário cadastrado com sucesso");
    }
}
