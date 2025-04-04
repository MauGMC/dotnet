using CatalogoProductosAplicacion.Interfaces;
using CatalogoProductosDominio.Entidades;
using CatalogoProductosInfraestructura.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CatalogoProductosInfraestructura.Repositorios
{
    public class ProductosRepo : IProductosRepo
    {
        private readonly CatalogoProductosDbContext context;

        public ProductosRepo(IDbContextFactory<CatalogoProductosDbContext> factory)
        {
            context = factory.CreateDbContext();
        }

        public async Task AddSync(Productos producto)
        {
            context.Productos.Add(producto);
            await context.SaveChangesAsync();
        }

        public async Task<List<Productos>> GetAllAsync()
        {
            var productos = await context.Productos.ToListAsync();
            return productos;
        }

        public async Task<Productos?> GetById(int id)
        {
            var producto = await context.Productos.FirstOrDefaultAsync(e => e.ID_prod == id);
            return producto;
        }

        public async Task UpdateAsync(Productos producto)
        {
            context.Entry(producto).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}

