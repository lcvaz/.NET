using System.ComponentModel.DataAnnotations;

namespace DTOs.UsuarioCadastroDto;

public class UsuarioCadastroDTO
{
    [Required]
    [MaxLength(200, ErrorMessage = "O nome deve ter no máximo 200 caracteres")]
    public string Nome { get; set; }

    [Required]
    [MaxLength(200, ErrorMessage = "O email deve ter no máximo 200 caracteres")]
    public string Email { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 8, ErrorMessage = "A senha deve ter entre 8 e 20 caracteres")]
    public string Senha { get; set; }

}