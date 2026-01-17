using Application.DTOs;
using Application.Interfaces;
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
    private readonly IJwtService _jwtService;

    public UsuarioService(IUsuario usuarioRepository, IJwtService jwtService)
    {
        _usuarioRepository = usuarioRepository;
        _jwtService = jwtService;
    }

    public async Task<ApiResponseDto<LoginResponseDto>> LoginAsync(string email, string senha, bool rememberMe)
    {
        var resultado = await _usuarioRepository.GetUsuarioByEmailAsync(email);

        if (!resultado.Sucesso || resultado.Data == null)
            return ApiResponseDto<LoginResponseDto>.NotFound("Usuário não encontrado");

        if (!BCrypt.Net.BCrypt.Verify(senha, resultado.Data.SenhaHash))
            return ApiResponseDto<LoginResponseDto>.Unauthorized("Email ou senha inválidos");

        var token = _jwtService.GenerateToken(resultado.Data, rememberMe);
        var expiresIn = _jwtService.GetExpirationTime(rememberMe);

        var loginResponse = new LoginResponseDto
        {
            Token = token,
            ExpiresIn = expiresIn,
            Status = 200,
            Usuario = new UsuarioDto
            {
                Id = resultado.Data.Id,
                Nome = resultado.Data.Nome,
                Email = resultado.Data.Email,
                DataCadastro = resultado.Data.DataCriacao
            }
        };

        return ApiResponseDto<LoginResponseDto>.Ok(loginResponse, "Login realizado com sucesso");
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
