using Microsoft.AspNetCore.Mvc;
using Interfaces.IUsuarioService;
using Application.DTOs;

namespace Controllers.usuarioController;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponseDto<LoginResponseDto>.BadRequest("Dados inválidos"));

        var resultado = await _usuarioService.LoginAsync(loginDto.Email, loginDto.Senha, loginDto.LembrarMe);

        if (!resultado.Sucesso)
            return StatusCode(resultado.StatusCode, resultado);

        // Retorna diretamente o LoginResponseDto para o frontend
        return Ok(resultado.Data);
    }

    [HttpPost("cadastrar")]
    public async Task<IActionResult> Cadastrar([FromBody] CadastroDto cadastroDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponseDto<CadastroDto>.BadRequest("Dados inválidos"));

        var resultado = await _usuarioService.CadastrarAsync(cadastroDto.Nome, cadastroDto.Email, cadastroDto.Senha);

        return StatusCode(resultado.StatusCode, resultado);
    }
}
