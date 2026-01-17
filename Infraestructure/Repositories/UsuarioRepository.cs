using Domain.Entidades.Usuario;
using Interfaces.IUsuario;
using Supabase;

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

    public async Task<Usuario> LoginAsync(string email, string senha, bool rememberMe)
    {
        var response = await _supabaseClient
            .From<Usuario>()
            .Where(u => u.Email == email)
            .Single();

        return response;
    }

    public async Task<Usuario> CadastrarAsync(string nome, string email, string senha)
    {
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

        return response.Models.FirstOrDefault();
    }

    public async Task<IEnumerable<Usuario>> GetAllUsuariosAsync()
    {
        var response = await _supabaseClient
            .From<Usuario>()
            .Get();

        return response.Models;
    }

    public async Task<Usuario> GetUsuarioByIdAsync(int id)
    {
        var response = await _supabaseClient
            .From<Usuario>()
            .Where(u => u.Id == id)
            .Single();

        return response;
    }

    public async Task<Usuario> GetUsuarioByEmailAsync(string email)
    {
        var response = await _supabaseClient
            .From<Usuario>()
            .Where(u => u.Email == email)
            .Single();

        return response;
    }

    public async Task AddUsuarioAsync(Usuario usuario)
    {
        await _supabaseClient
            .From<Usuario>()
            .Insert(usuario);
    }

    public async Task UpdateUsuarioAsync(Usuario usuario)
    {
        await _supabaseClient
            .From<Usuario>()
            .Where(u => u.Id == usuario.Id)
            .Update(usuario);
    }

    public async Task DeleteUsuarioAsync(int id)
    {
        await _supabaseClient
            .From<Usuario>()
            .Where(u => u.Id == id)
            .Delete();
    }
}
