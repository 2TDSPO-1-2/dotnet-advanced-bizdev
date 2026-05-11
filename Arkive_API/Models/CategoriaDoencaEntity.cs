using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arkive_API.Models
{
    [Table("TB_ARKIVE_CATEGORIA_DOENCA")]
    [Index(nameof(Nome), IsUnique = true, Name = "UQ_ARKIVE_CATEGORIA_NOME")]
    public class CategoriaDoencaEntity
    {
        [Key]
        [Column("ID_CATEGORIA")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da Categoria é obrigatório.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome da Categoria deve ter no máximo 100 caracteres.")]
        [Column("NM_CATEGORIA")]
        public string Nome { get; set; }

        [Column("ST_ATIVO")]
        [Required]
        public char StAtivo { get; set; } = 'S';
    }
}
