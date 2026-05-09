using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Arkive_API.Models
{
    [Table("TB_ARKIVE_RACA")]
    [Index(nameof(IdEspecie), nameof(Raca), IsUnique = true, Name = "UQ_ARKIVE_RACA_ESPECIE_NOME")]
    public class RacaEntity
    {
        [Key]
        [Column("ID_RACA")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da Raça é obrigatório.")]
        [Column("NM_RACA")]
        [StringLength(50, ErrorMessage = "O nome da Raça deve ter no máximo 50 caracteres.")]
        public string Raca { get; set; }

        [Required(ErrorMessage = "O ID da Espécie é obrigatório.")]
        [Column("ID_ESPECIE")]
        public int IdEspecie { get; set; }

        [ForeignKey(nameof(IdEspecie))]
        public EspecieEntity Especie { get; set; } = null!;

        [Column("TP_PORTE")]
        [RegularExpression("Pequeno|Médio|Grande", ErrorMessage = "Tamanho/Porte inválido.")]
        public string? Porte { get; set; }
    }
}
