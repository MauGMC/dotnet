using CatalogoProductosDominio.Entidades;
using Microsoft.EntityFrameworkCore;
namespace CatalogoProductosInfraestructura.Contexto
{
    public class CatalogoProductosDbContext:DbContext
    {
        public CatalogoProductosDbContext(DbContextOptions<CatalogoProductosDbContext>options): base(options)
        {
            
        }
        public DbSet<Productos> Productos { get; set; }
        /*Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Productos>().Property(e => e.Nombre).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Productos>().Property(e => e.Descripcion).IsRequired().HasMaxLength(100);
        }*/

    }
}
