using System.ComponentModel.DataAnnotations;
using Postgrest.Attributes;
using Postgrest.Models;

namespace Domain.Entidades.Usuario;

[Table("Tbl_Usuario")]
public class Usuario : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }

    [Required]
    [Column("nome")]
    public string Nome { get; set; }

    [Required]
    [Column("email")]
    public string Email { get; set; }

    [Required]
    [Column("senha_hash")]
    public string SenhaHash { get; set; }

    [Column("criado_em")]
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}