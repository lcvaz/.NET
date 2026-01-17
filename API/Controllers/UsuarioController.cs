using Microsoft.AspNetCore.Mvc; 
using Interfaces.IUsuarioService;

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

}