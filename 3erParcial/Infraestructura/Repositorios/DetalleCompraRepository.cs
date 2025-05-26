
namespace Infraestructura.Repositorios
{
    public class DetalleCompraRepository : IDetalleCompraRepository
    {
        private readonly ApplicationDbContext _context;
        public DetalleCompraRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task ActualizarAsync(DetalleCompra entidad)
        {
            _context.DetallesCompras.Update(entidad);
            await GuardarCambiosAsync();
        }

        public async Task AgregarAsync(DetalleCompra entidad)
        {
            _context.DetallesCompras.Add(entidad);
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
                throw new Exception("Error al guardar los cambios en el detalle de compra.", ex);
            }
        }
        public async Task<IEnumerable<DetalleCompra>> ObtenerDetallesPorCompraIdAsync(int compraId)
        {
            return await _context.DetallesCompras
                .Where(dc => dc.CompraID == compraId)
                .ToListAsync();
        }

        public async Task<DetalleCompra> ObtenerPorIdAsync(int id)
        {
            return await _context.DetallesCompras.FindAsync(id);
        }

        public async Task<IEnumerable<(Producto producto, int cantidadComprada)>> ObtenerProductosMasCompradosAsync(int top = 5)
        {
            //Select de ProductoID y sumatoria de Cantidad,
            //ordenados por más comprados y solo los del parámetro
            var query = await _context.DetallesCompras
                .GroupBy(dc => dc.ProductoID)
                .Select(g => new
                {
                    ProductoId = g.Key,
                    CantidadTotal = g.Sum(dc => dc.Cantidad)
                })
                .OrderByDescending(x => x.CantidadTotal)
                .Take(top)
                .ToListAsync();

            //Obtiene los IDs de los productos más comprados
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


        public async Task<IEnumerable<(Producto producto, int cantidadComprada)>> ObtenerProductosMenosCompradosAsync(int bottom = 5)
        {
            //Select de ProductoID y sumatoria de Cantidad,
            //ordenados por menos comprados y solo los del parámetro
            var query = await _context.DetallesCompras
                .GroupBy(dc => dc.ProductoID)
                .Select(g => new
                {
                    ProductoId = g.Key,
                    CantidadTotal = g.Sum(dc => dc.Cantidad)
                })
                .Where(x => x.CantidadTotal > 0)
                .OrderBy(x => x.CantidadTotal)
                .Take(bottom)
                .ToListAsync();

            //Obtiene los IDs de los productos menos comprados
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

        public async Task<IEnumerable<DetalleCompra>> ObtenerTodosAsync()
        {
            return await _context.DetallesCompras
                .Include(dc => dc.Producto) 
                .ToListAsync();
        }
    }
}
