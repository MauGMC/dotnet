
namespace Infraestructura.Repositorios
{
    public class CompraRepository : ICompraRepository
    {
        public ApplicationDbContext _context;
        public CompraRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task ActualizarAsync(Compra entidad)
        {
            _context.Compras.Update(entidad);
            await GuardarCambiosAsync();
        }

        public async Task AgregarAsync(Compra entidad)
        {
            _context.Compras.Add(entidad);
            await GuardarCambiosAsync();
        }

        public async Task CambiarEstadoDeCompraAsync(int compraId, int nuevoEstado)
        {
            var compra = await ObtenerPorIdAsync(compraId);
            if (compra == null)
            {
                throw new KeyNotFoundException($"Compra with ID {compraId} not found.");
            }
            compra.Estado = nuevoEstado;
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
                throw new Exception("Error al guardar los cambios de la compra.", ex);
            }
        }   
        public async Task<Compra> ObtenerCompraPorNumeroFactura(string numeroFactura)
        {
            return await _context.Compras
                .FirstOrDefaultAsync(c => c.NumeroFactura == numeroFactura);
        }

        public async Task<IEnumerable<Compra>> ObtenerComprasEntregadasEntreFechasAsync(DateTime desde, DateTime hasta)
        {
            return await _context.Compras
                .Where(compras=>compras.FechaEntrega >= desde && compras.FechaEntrega <= hasta)
                .ToListAsync();
        }

        public async Task<IEnumerable<Compra>> ObtenerComprasPorEmpleadoAsync(int empleadoId)
        {
            return await _context.Compras
                .Where(compra => compra.EmpleadoID == empleadoId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Compra>> ObtenerComprasPorEstadoAsync(int estado)
        {
            return await _context.Compras
                .Where(compra => compra.Estado == estado)
                .ToListAsync();
        }

        public async Task<IEnumerable<Compra>> ObtenerComprasPorProveedorAsync(int proveedorId)
        {
            return await _context.Compras
                .Where(compra => compra.ProveedorID == proveedorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Compra>> ObtenerComprasPorRangoDePrecioAsync(decimal minimo, decimal max)
        {
            return await _context.Compras
                .Where(compra => compra.Total >= minimo && compra.Total <= max)
                .ToListAsync();
        }

        public async Task<IEnumerable<Compra>> ObtenerComprasRealizadasEntreFechasAsync(DateTime desde, DateTime hasta)
        {
            return await _context.Compras
                .Where(compra => compra.FechaCompra >= desde && compra.FechaCompra <= hasta)
                .ToListAsync();
        }

        public async Task<Compra> ObtenerPorIdAsync(int id)
        {
            return await _context.Compras.FindAsync(id);
        }

        public async Task<IEnumerable<Compra>> ObtenerTodosAsync()
        {
            return await _context.Compras.ToListAsync();
        }
    }
}
