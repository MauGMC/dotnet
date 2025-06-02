
namespace Infraestructura.Repositorios
{
    public class DetalleVentaRepository : IDetalleVentaRepository
    {
        private readonly ApplicationDbContext _context;
        public DetalleVentaRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task ActualizarAsync(DetalleVenta entidad)
        {
            _context.DetallesVentas.Update(entidad);
            await GuardarCambiosAsync();
        }

        public async Task AgregarAsync(DetalleVenta entidad)
        {
            _context.DetallesVentas.Add(entidad);
            await GuardarCambiosAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await ObtenerPorIdAsync(id);
            _context.Remove(entidad);
            await GuardarCambiosAsync();
        }

        public Task GuardarCambiosAsync()
        {
            try
            {
                return _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error al guardar los cambios en el detalle de venta.", ex);
            }
        }
        public async Task<IEnumerable<DetalleVenta>> ObtenerDetallesPorVentaIdAsync(int ventaId)
        {
             return await _context.DetallesVentas
                .Where(dv => dv.VentaID == ventaId)
                .ToListAsync();
        }

        public async Task<DetalleVenta> ObtenerPorIdAsync(int id)
        {
            return await _context.DetallesVentas.FindAsync(id);
        }

        public async Task<IEnumerable<(Producto producto, int cantidadVendida)>> ObtenerProductosMasVendidosAsync(int top = 5)
        {
            //Select de ProductoID y sumatoria de Cantidad,
            //ordenados por más vendidos y solo los del parámetro
            var query = await _context.DetallesVentas
                .GroupBy(dc => dc.ProductoID)
                .Select(g => new
                {
                    ProductoId = g.Key,
                    CantidadTotal = g.Sum(dc => dc.Cantidad)
                })
                .OrderByDescending(x => x.CantidadTotal)
                .Take(top)
                .ToListAsync();

            //Obtiene los IDs de los productos más vendidos
            var productoIds = query.Select(x => x.ProductoId).ToList();

            //Consulta los productos correspondientes a esos IDs
            var productos = await _context.Productos
                .Where(p => productoIds.Contains(p.ProductoID))
                .ToListAsync();

            //Join de Productos (completos) con las cantidades totales (query) 
            var resultado = query.Join(
                productos, //De la lista de productos
                q => q.ProductoId, //Donde el ProductoID del query es igual al ProductoID de productos
                p => p.ProductoID, //Donde el ProductoID de productos es igual al ProductoID del query
                (q, p) => (producto: p, cantidadComprada: q.CantidadTotal)); //Unir en el mostrado

            return resultado;
        }

        public async Task<IEnumerable<(Producto producto, int cantidadVendida)>> ObtenerProductosMenosVendidosAsync(int bottom = 5)
        {
            //Select de ProductoID y sumatoria de Cantidad,
            //ordenados por más vendidos y solo los del parámetro
            var query = await _context.DetallesVentas
                .GroupBy(dc => dc.ProductoID)
                .Select(g => new
                {
                    ProductoId = g.Key,
                    CantidadTotal = g.Sum(dc => dc.Cantidad)
                })
                .Where(x=>x.CantidadTotal > 0) // Asegurarse de que la cantidad total sea mayor a 0
                .OrderByDescending(x => x.CantidadTotal)
                .Take(bottom)
                .ToListAsync();

            //Obtiene los IDs de los productos más vendidos
            var productoIds = query.Select(x => x.ProductoId).ToList();

            //Consulta los productos correspondientes a esos IDs
            var productos = await _context.Productos
                .Where(p => productoIds.Contains(p.ProductoID))
                .ToListAsync();

            //Join de Productos (completos) con las cantidades totales (query) 
            var resultado = query.Join(
                productos, //De la lista de productos
                q => q.ProductoId, //Donde el ProductoID del query es igual al ProductoID de productos
                p => p.ProductoID, //Donde el ProductoID de productos es igual al ProductoID del query
                (q, p) => (producto: p, cantidadComprada: q.CantidadTotal)); //Unir en el mostrado

            return resultado;
        }

        public async Task<IEnumerable<DetalleVenta>> ObtenerTodosAsync()
        {
            return await _context.DetallesVentas.ToListAsync();
        }
    }
}
