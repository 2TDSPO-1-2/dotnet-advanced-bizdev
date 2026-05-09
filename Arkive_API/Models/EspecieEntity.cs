using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arkive_API.Models
{
    [Table("TB_ARKIVE_ESPECIE")]
    [Index(nameof(Especie), IsUnique = true, Name = "UQ_ARKIVE_ESPECIE_NOME")]
    public class EspecieEntity
    {
        [Key]
        [Column("ID_ESPECIE")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da Espécie é obrigatório.")]
        [Column("NM_ESPECIE")]
        [StringLength(50, ErrorMessage = "O nome da Espécie deve ter no máximo 50 caracteres.")]
        public string Especie { get; set; }
    }
}
