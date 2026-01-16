using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entidades.Usuario; 

[Table("Tbl_Usuario")]
public class Usuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-incrementa o ID
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("nome")]
    public string Nome { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("email")]
    public string Email { get; set; }

    [Required]
    [Column("senha_hash")]
    public string SenhaHash { get; set; }

    [Column("criado_em")]
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}