using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Repositorios
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly AppDbContext _context;

        public ProductoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Producto>> ObtenerTodosAsync()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task<Producto?> ObtenerPorIdAsync(int id)
        {
            return await _context.Productos.FindAsync(id);
        }

        public async Task AgregarAsync(Producto producto)
        {
            if (producto == null)
            {
                throw new ArgumentNullException(nameof(producto), "El producto no puede ser null");
            }
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Producto producto)
        {
            if (producto == null)
            {
                throw new ArgumentNullException(nameof(producto), "El producto no puede ser null");
            }
            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<(List<Producto> Productos, int TotalRegistros)> ObtenerPaginadoAsync(int pagina, int tamañoPagina)
        {
            var query = _context.Productos.AsQueryable();

            int total = await query.CountAsync();
            var productos = await query
                .OrderBy(p => p.ProductoID)
                .Skip((pagina - 1) * tamañoPagina)
                .Take(tamañoPagina)
                .ToListAsync();

            return (productos, total);
        }

        public async Task AgregarRangoAsync(IEnumerable<Producto> productos)
        {
            await _context.Productos.AddRangeAsync(productos);
            await _context.SaveChangesAsync();
        }

        public async Task <bool>ExistenciaPorIdONombre(int id, string nombre)
        {
            return await _context.Productos.AnyAsync(p => p.ProductoID == id || p.Nombre==nombre);
        }

    }

}
