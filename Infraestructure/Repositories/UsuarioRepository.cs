using Domain.DTOs;
using Domain.Entidades.Usuario;
using Interfaces.IUsuario;

namespace Infraestructure.Repositories;

/// <summary>
/// Repositório de usuários utilizando Supabase
/// </summary>
public class UsuarioRepository : IUsuario
{
    private readonly Supabase.Client _supabaseClient;

    public UsuarioRepository(Supabase.Client supabaseClient)
    {
        _supabaseClient = supabaseClient;
    }

    public async Task<RepositoryResponseDto<Usuario>> LoginAsync(string email, string senha, bool rememberMe)
    {
        try
        {
            var response = await _supabaseClient
                .From<Usuario>()
                .Where(u => u.Email == email)
                .Single();

            if (response == null)
                return RepositoryResponseDto<Usuario>.NotFound("Usuário não encontrado");

            return RepositoryResponseDto<Usuario>.Ok(response, "Usuário encontrado");
        }
        catch (Exception ex)
        {
            return RepositoryResponseDto<Usuario>.Error($"Erro ao buscar usuário: {ex.Message}");
        }
    }

    public async Task<RepositoryResponseDto<Usuario>> CadastrarAsync(string nome, string email, string senha)
    {
        try
        {
            var usuarioExistente = await _supabaseClient
                .From<Usuario>()
                .Where(u => u.Email == email)
                .Single();

            if (usuarioExistente != null)
                return RepositoryResponseDto<Usuario>.Conflict("Email já cadastrado");

            var usuario = new Usuario
            {
                Nome = nome,
                Email = email,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(senha),
                DataCriacao = DateTime.UtcNow
            };

            var response = await _supabaseClient
                .From<Usuario>()
                .Insert(usuario);

            var usuarioCriado = response.Models.FirstOrDefault();
            if (usuarioCriado == null)
                return RepositoryResponseDto<Usuario>.Error("Erro ao criar usuário");

            return RepositoryResponseDto<Usuario>.Ok(usuarioCriado, "Usuário cadastrado com sucesso");
        }
        catch (Exception ex)
        {
            return RepositoryResponseDto<Usuario>.Error($"Erro ao cadastrar usuário: {ex.Message}");
        }
    }

    public async Task<RepositoryResponseDto<IEnumerable<Usuario>>> GetAllUsuariosAsync()
    {
        try
        {
            var response = await _supabaseClient
                .From<Usuario>()
                .Get();

            return RepositoryResponseDto<IEnumerable<Usuario>>.Ok(response.Models, "Usuários encontrados");
        }
        catch (Exception ex)
        {
            return RepositoryResponseDto<IEnumerable<Usuario>>.Error($"Erro ao buscar usuários: {ex.Message}");
        }
    }

    public async Task<RepositoryResponseDto<Usuario>> GetUsuarioByIdAsync(int id)
    {
        try
        {
            var response = await _supabaseClient
                .From<Usuario>()
                .Where(u => u.Id == id)
                .Single();

            if (response == null)
                return RepositoryResponseDto<Usuario>.NotFound("Usuário não encontrado");

            return RepositoryResponseDto<Usuario>.Ok(response, "Usuário encontrado");
        }
        catch (Exception ex)
        {
            return RepositoryResponseDto<Usuario>.Error($"Erro ao buscar usuário: {ex.Message}");
        }
    }

    public async Task<RepositoryResponseDto<Usuario>> GetUsuarioByEmailAsync(string email)
    {
        try
        {
            var response = await _supabaseClient
                .From<Usuario>()
                .Where(u => u.Email == email)
                .Single();

            if (response == null)
                return RepositoryResponseDto<Usuario>.NotFound("Usuário não encontrado");

            return RepositoryResponseDto<Usuario>.Ok(response, "Usuário encontrado");
        }
        catch (Exception ex)
        {
            return RepositoryResponseDto<Usuario>.Error($"Erro ao buscar usuário: {ex.Message}");
        }
    }

    public async Task<RepositoryResponseDto<Usuario>> AddUsuarioAsync(Usuario usuario)
    {
        try
        {
            var response = await _supabaseClient
                .From<Usuario>()
                .Insert(usuario);

            var usuarioCriado = response.Models.FirstOrDefault();
            if (usuarioCriado == null)
                return RepositoryResponseDto<Usuario>.Error("Erro ao adicionar usuário");

            return RepositoryResponseDto<Usuario>.Ok(usuarioCriado, "Usuário adicionado com sucesso");
        }
        catch (Exception ex)
        {
            return RepositoryResponseDto<Usuario>.Error($"Erro ao adicionar usuário: {ex.Message}");
        }
    }

    public async Task<RepositoryResponseDto<Usuario>> UpdateUsuarioAsync(Usuario usuario)
    {
        try
        {
            var response = await _supabaseClient
                .From<Usuario>()
                .Where(u => u.Id == usuario.Id)
                .Update(usuario);

            var usuarioAtualizado = response.Models.FirstOrDefault();
            if (usuarioAtualizado == null)
                return RepositoryResponseDto<Usuario>.NotFound("Usuário não encontrado para atualização");

            return RepositoryResponseDto<Usuario>.Ok(usuarioAtualizado, "Usuário atualizado com sucesso");
        }
        catch (Exception ex)
        {
            return RepositoryResponseDto<Usuario>.Error($"Erro ao atualizar usuário: {ex.Message}");
        }
    }

    public async Task<RepositoryResponseDto<bool>> DeleteUsuarioAsync(int id)
    {
        try
        {
            var usuarioExistente = await GetUsuarioByIdAsync(id);
            if (!usuarioExistente.Sucesso)
                return RepositoryResponseDto<bool>.NotFound("Usuário não encontrado para exclusão");

            await _supabaseClient
                .From<Usuario>()
                .Where(u => u.Id == id)
                .Delete();

            return RepositoryResponseDto<bool>.Ok(true, "Usuário excluído com sucesso");
        }
        catch (Exception ex)
        {
            return RepositoryResponseDto<bool>.Error($"Erro ao excluir usuário: {ex.Message}");
        }
    }
}
