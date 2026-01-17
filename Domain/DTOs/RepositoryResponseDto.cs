namespace Domain.DTOs;

/// <summary>
/// DTO genérico para respostas do repositório
/// Encapsula status e dados de operações de banco de dados
/// </summary>
public class RepositoryResponseDto<T>
{
    public bool Sucesso { get; set; }
    public string Mensagem { get; set; } = string.Empty;
    public T? Data { get; set; }
    public RepositoryStatusCode StatusCode { get; set; }

    public static RepositoryResponseDto<T> Ok(T data, string mensagem = "Operação realizada com sucesso")
    {
        return new RepositoryResponseDto<T>
        {
            Sucesso = true,
            StatusCode = RepositoryStatusCode.Success,
            Mensagem = mensagem,
            Data = data
        };
    }

    public static RepositoryResponseDto<T> NotFound(string mensagem = "Registro não encontrado")
    {
        return new RepositoryResponseDto<T>
        {
            Sucesso = false,
            StatusCode = RepositoryStatusCode.NotFound,
            Mensagem = mensagem,
            Data = default
        };
    }

    public static RepositoryResponseDto<T> Conflict(string mensagem = "Registro já existe")
    {
        return new RepositoryResponseDto<T>
        {
            Sucesso = false,
            StatusCode = RepositoryStatusCode.Conflict,
            Mensagem = mensagem,
            Data = default
        };
    }

    public static RepositoryResponseDto<T> Error(string mensagem = "Erro ao acessar o banco de dados")
    {
        return new RepositoryResponseDto<T>
        {
            Sucesso = false,
            StatusCode = RepositoryStatusCode.Error,
            Mensagem = mensagem,
            Data = default
        };
    }
}

/// <summary>
/// Enum para códigos de status do repositório
/// </summary>
public enum RepositoryStatusCode
{
    Success = 200,
    NotFound = 404,
    Conflict = 409,
    Error = 500
}
