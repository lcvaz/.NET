namespace Application.DTOs;

/// <summary>
/// DTO genérico para respostas da API
/// Encapsula status HTTP, mensagens e dados de resposta
/// </summary>
public class ApiResponseDto<T>
{
    public int StatusCode { get; set; }
    public bool Sucesso { get; set; }
    public string Mensagem { get; set; } = string.Empty;
    public T? Data { get; set; }

    public static ApiResponseDto<T> Ok(T data, string mensagem = "Operação realizada com sucesso")
    {
        return new ApiResponseDto<T>
        {
            StatusCode = 200,
            Sucesso = true,
            Mensagem = mensagem,
            Data = data
        };
    }

    public static ApiResponseDto<T> Created(T data, string mensagem = "Recurso criado com sucesso")
    {
        return new ApiResponseDto<T>
        {
            StatusCode = 201,
            Sucesso = true,
            Mensagem = mensagem,
            Data = data
        };
    }

    public static ApiResponseDto<T> NotFound(string mensagem = "Recurso não encontrado")
    {
        return new ApiResponseDto<T>
        {
            StatusCode = 404,
            Sucesso = false,
            Mensagem = mensagem,
            Data = default
        };
    }

    public static ApiResponseDto<T> Unauthorized(string mensagem = "Não autorizado")
    {
        return new ApiResponseDto<T>
        {
            StatusCode = 401,
            Sucesso = false,
            Mensagem = mensagem,
            Data = default
        };
    }

    public static ApiResponseDto<T> Conflict(string mensagem = "Conflito de dados")
    {
        return new ApiResponseDto<T>
        {
            StatusCode = 409,
            Sucesso = false,
            Mensagem = mensagem,
            Data = default
        };
    }

    public static ApiResponseDto<T> BadRequest(string mensagem = "Requisição inválida")
    {
        return new ApiResponseDto<T>
        {
            StatusCode = 400,
            Sucesso = false,
            Mensagem = mensagem,
            Data = default
        };
    }

    public static ApiResponseDto<T> InternalServerError(string mensagem = "Erro interno do servidor")
    {
        return new ApiResponseDto<T>
        {
            StatusCode = 500,
            Sucesso = false,
            Mensagem = mensagem,
            Data = default
        };
    }
}
