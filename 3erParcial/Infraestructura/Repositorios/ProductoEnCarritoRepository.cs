
using Aplicacion.Enums;
using Aplicacion.Extensions;

namespace Infraestructura.Repositorios
{
    public class ProductoEnCarritoRepository : IProductoEnCarritoRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductoEnCarritoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarAsync(ProductoEnCarrito entidad)
        {
            _context.ProductosEnCarrito.Update(entidad);
            await GuardarCambiosAsync();
        }

        public async Task AgregarAsync(ProductoEnCarrito entidad)
        {
            _context.ProductosEnCarrito.Add(entidad);
            await GuardarCambiosAsync();
        }

        public async Task AgregarProductosAlCarritoAsync(IEnumerable<ProductoEnCarrito> productos)
        {
            await _context.ProductosEnCarrito.AddRangeAsync(productos);
            await GuardarCambiosAsync();
        }

        public async Task DisminuirCantidadAsync(int carritoId, int productoId, int cantidad = 1)
        {
            var entidad = await _context.ProductosEnCarrito
                .FirstOrDefaultAsync(pc => pc.CarritoID == carritoId && pc.ProductoID == productoId);
            entidad.Cantidad -= cantidad;
            if(entidad.Cantidad < 0)
                entidad.Cantidad = 0;
            await GuardarCambiosAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await ObtenerPorIdAsync(id);
            _context.ProductosEnCarrito.Remove(entidad);
            await GuardarCambiosAsync();
        }

        public async Task EliminarProductoDelCarritoAsync(int carritoId, int productoId)
        {
            var productoEnCarrito = await _context.ProductosEnCarrito
                .FirstOrDefaultAsync(p => p.CarritoID == carritoId && p.ProductoID == productoId);

            if(productoEnCarrito == null)
                throw new InvalidOperationException("El producto no se encontró en el carrito.");

            _context.ProductosEnCarrito.Remove(productoEnCarrito);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarProductosDelCarritoAsync(IEnumerable<ProductoEnCarrito> productos)
        {
            if (productos == null || !productos.Any())
                throw new ArgumentException("No hay productos para eliminar.");
            _context.ProductosEnCarrito.RemoveRange(productos);
            await GuardarCambiosAsync();
        }

        public async Task<bool> ExisteProductoEnCarritoAsync(int carritoId, int productoId)
        {
            return await _context.ProductosEnCarrito
                .AnyAsync(pc => pc.CarritoID == carritoId && pc.ProductoID == productoId);
        }

        public async Task GuardarCambiosAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException ex)
            {
                throw new Exception("Hubo un error al guardar los cambios en la entidad", ex);
            }
        }

        public async Task IncrementarCantidadAsync(int carritoId, int productoId, int cantidad = 1)
        {
            var carrito = await _context.ProductosEnCarrito
                .FirstOrDefaultAsync(pc => pc.CarritoID == carritoId && pc.ProductoID == productoId);
            carrito.Cantidad += cantidad;   
            await _context.SaveChangesAsync();
        }

        public async Task<ProductoEnCarrito> ObtenerPorIdAsync(int id)
        {
            return await _context.ProductosEnCarrito.FindAsync();
        }

        public async Task<ProductoEnCarrito> ObtenerProductoEnCarritoAsync(int carritoId, int productoId)
        {
            return await _context.ProductosEnCarrito
                .FirstOrDefaultAsync(pc => pc.CarritoID == carritoId && pc.ProductoID == productoId);
        }

        public async Task<IEnumerable<ProductoEnCarrito>> ObtenerProductosEnCarritoFiltradoGeneralAsync(int ordenarPor=1, bool ordenAscendente=true, int pagina = 1, int tamanoPagina = 10, int? carritoId = null, string? nombre = null, decimal? precioMin = null, decimal? precioMax = null, int? cantidadMin = null, int? cantidadMax = null)
        {
            var query =  _context.ProductosEnCarrito
                .Include(pc=>pc.Producto)
                .AsQueryable();
            if (carritoId.HasValue)
                query=query.Where(pc => pc.CarritoID == carritoId);

            if (!string.IsNullOrWhiteSpace(nombre))
                query = query.Where(pc => EF.Functions.Like(pc.Producto.Nombre, $"%{nombre}%"));

            if (precioMin.HasValue)
                query = query.Where(pc => pc.Producto.Precio >= precioMin);

            if (precioMax.HasValue)
                query = query.Where(pc=>pc.Producto.Precio<=precioMax);

            if (cantidadMin.HasValue)
                query = query.Where(pc => pc.Cantidad >= cantidadMin);

            if (cantidadMax.HasValue)
                query = query.Where(pc=>pc.Cantidad<=cantidadMax);

            OrdenarProductosEnCarritoPor orden;

            if (!Enum.IsDefined(typeof(OrdenarProductosEnCarritoPor), ordenarPor))
                orden = OrdenarProductosEnCarritoPor.ProductoEnCarritoID;
            else
                orden = (OrdenarProductosEnCarritoPor)ordenarPor;

            query = query.OrdenarPor(orden, ordenAscendente);

            query = query.OrderByDescending(p => p.ProductoEnCarritoID);

            query = query
                .Skip((pagina - 1) * tamanoPagina)
                .Take(tamanoPagina);

            return await query.ToListAsync();
        }   

        public async Task<IEnumerable<ProductoEnCarrito>> ObtenerTodosAsync()
        {
            return await _context.ProductosEnCarrito.ToListAsync();
        }
    }
}
