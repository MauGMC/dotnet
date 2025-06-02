
using Aplicacion.Enums;
using Aplicacion.Extensions;

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
        public async Task<IEnumerable<Compra>> ObtenerComprasFiltradoGeneralAsync(int ordenarPor= 1, bool ordenAscendente = true, int pagina = 1, int tamanoPagina = 10, string? numeroFactura = null, int? proveedorId = null, int? empleadoId = null, int? estado = null, DateTime? fechaCompraRealizadaDesde = null, DateTime? fechaCompraRealizadaHasta = null, DateTime? fechaEntregaDesde = null, DateTime? fechaEntreagadaHasta = null, decimal? precioMinimo = null, decimal? precioMaximo = null)
        {
            var query = _context.Compras.AsQueryable();
            if (!string.IsNullOrWhiteSpace(numeroFactura))
                query = query.Where(c => c.NumeroFactura.Contains(numeroFactura));
            if (proveedorId.HasValue)
                query = query.Where(c => c.ProveedorID == proveedorId.Value);
            if (empleadoId.HasValue)
                query = query.Where(c => c.EmpleadoID == empleadoId.Value);
            if (estado.HasValue)
                query = query.Where(c => c.Estado == estado.Value);
            if (fechaCompraRealizadaDesde.HasValue)
                query = query.Where(c => c.FechaCompra >= fechaCompraRealizadaDesde.Value);
            if (fechaCompraRealizadaHasta.HasValue)
                query = query.Where(c => c.FechaCompra <= fechaCompraRealizadaHasta.Value);
            if (fechaEntregaDesde.HasValue)
                query = query.Where(c => c.FechaEntrega >= fechaEntregaDesde.Value);
            if (fechaEntreagadaHasta.HasValue)
                query = query.Where(c => c.FechaEntrega <= fechaEntreagadaHasta.Value);
            if (precioMinimo.HasValue)
                query = query.Where(c => c.Total >= precioMinimo.Value);
            if (precioMaximo.HasValue)
                query = query.Where(c => c.Total <= precioMaximo.Value);

            OrdenarCompraPor orden;
            if (!Enum.IsDefined(typeof(OrdenarCompraPor), ordenarPor))
                orden = OrdenarCompraPor.CompraID;
            else
                orden = (OrdenarCompraPor)ordenarPor;

            query = query.OrdenarPor(orden, ordenAscendente);

                query = query
                    .Skip((pagina - 1) * tamanoPagina)
                    .Take(tamanoPagina);
            return await query.ToListAsync();
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
