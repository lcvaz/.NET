using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entidades.Usuario; 

[Table("Tbl_Usuario")]
public class Usuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-incrementa o ID
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nome { get; set; }

    [Required]
    [MaxLength(200)]
    public string Email { get; set; }

    [Required]
    public string SenhaHash { get; set; }

    [MaxLength(15)]
    public string Telefone { get; set; }

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}