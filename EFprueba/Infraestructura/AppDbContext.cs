using Microsoft.EntityFrameworkCore;
using Dominio.Entidades;
using Infraestructura.DbConfigurations;
namespace Infraestructura
{
    public class AppDbContext : DbContext
    {
        public DbSet<Producto> Productos { get; set; }
        public DbSet<CsvLog> CsvLogs { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}   
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.ApplyConfiguration(new ProductoConfiguration());
            modelBuilder.ApplyConfiguration(new CsvLogConfiguration());
            base.OnModelCreating(modelBuilder);

        }
    }
}
