using Arkive_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Arkive_API.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public DbSet<CategoriaDoencaEntity> CategoriaDoenca { get; set; }
        public DbSet<DoencaEntity> Doenca { get; set; }
        public DbSet<EspecieEntity> Especie { get; set; }
        public DbSet<FeedbackNPSEntity> FeedbackNPS { get; set; }
        public DbSet<PredisposicaoEntity> Predisposicao { get; set; }
        public DbSet<RacaEntity> Raca { get; set; }
    }
}
