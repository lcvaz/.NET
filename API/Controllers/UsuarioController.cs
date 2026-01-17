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
            return BadRequest(ApiResponseDto<LoginDto>.BadRequest("Dados inválidos"));

        var resultado = await _usuarioService.LoginAsync(loginDto.Email, loginDto.Senha, false);

        return StatusCode(resultado.StatusCode, resultado);
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
