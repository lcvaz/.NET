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
            return BadRequest(ModelState);

        var resultado = await _usuarioService.LoginAsync(loginDto.Email, loginDto.Senha, false);

        if (resultado == null)
            return Unauthorized(new { mensagem = "Email ou senha inválidos" });

        return Ok(new { mensagem = "Login realizado com sucesso", usuario = resultado });
    }

    [HttpPost("cadastrar")]
    public async Task<IActionResult> Cadastrar([FromBody] CadastroDto cadastroDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var resultado = await _usuarioService.CadastrarAsync(cadastroDto.Nome, cadastroDto.Email, cadastroDto.Senha);

        if (resultado == null)
            return Conflict(new { mensagem = "Email já cadastrado" });

        return CreatedAtAction(nameof(Cadastrar), new { mensagem = "Usuário cadastrado com sucesso", usuario = resultado });
    }
}