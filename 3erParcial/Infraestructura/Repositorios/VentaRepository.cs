
using Aplicacion.Enums;
using Aplicacion.Extensions;

namespace Infraestructura.Repositorios
{
    public class VentaRepository : IVentaRepository
    {
        private readonly ApplicationDbContext _context;
        public VentaRepository (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarAsync(Venta entidad)
        {
            _context.Ventas.Update(entidad);
            await GuardarCambiosAsync();
        }

        public async Task AgregarAsync(Venta entidad)
        {
            _context.Ventas.Add(entidad);
            await GuardarCambiosAsync();
        }

        public async Task CambiarEstadoAsync(int ventaid, int estado)
        {
            var entidad = await ObtenerPorIdAsync(ventaid);
            entidad.Estado = estado;
            await GuardarCambiosAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await ObtenerPorIdAsync(id);
            _context.Ventas.Remove(entidad);
            await GuardarCambiosAsync();
        }

        public async Task GuardarCambiosAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException ex)
            {
                throw new Exception("Se produjo un error al guardar los cambios en la venta.",ex);
            }
        }

        public async Task<Venta> ObtenerPorIdAsync(int id)
        {
            return await _context.Ventas.FindAsync(id);
        }

        public async Task<IEnumerable<Venta>> ObtenerTodosAsync()
        {
            return await _context.Ventas.ToListAsync();
        }

        public async Task<IEnumerable<Venta>> ObtenerVentasFiltradoGeneral(int ordenarPor = 1, bool ordenAscendente = true, int pagina = 1, int tamanoPagina = 10, int? clienteId = null, int? metodoPagoId = null, DateTime? desde = null, DateTime? hasta = null, int? estado = null, decimal? totalMinimo = 0, decimal? totalMax = decimal.MaxValue)
        {
            var query = _context.Ventas.AsQueryable();

            if(clienteId.HasValue)
                query = query.Where(v=>v.ClienteID == clienteId);
            if(metodoPagoId.HasValue)
                query = query.Where(v=>v.MetodoPagoID == metodoPagoId);
            if(desde.HasValue)
                query = query.Where(v=>v.FechaVenta>=desde);
            if(hasta.HasValue)
                query = query.Where(v=>v.FechaVenta<=hasta);
            if(estado.HasValue)
                query = query.Where(v=>v.Estado==estado);
            if(totalMinimo.HasValue)
                query = query.Where(v=>v.TotalConIVA>=totalMinimo);
            if(totalMax.HasValue)
                query = query.Where(v=>v.TotalConIVA<=totalMax);

            OrdenarVentasPor orden;
            if (!Enum.IsDefined(typeof(OrdenarVentasPor), ordenarPor))
                orden = OrdenarVentasPor.VentaID;
            else
                orden = (OrdenarVentasPor)ordenarPor;

            query = query.OrdenarPor(orden, ordenAscendente);

            query = query.Skip((pagina - 1) * tamanoPagina).Take(tamanoPagina);

            return await query.ToListAsync();
        }
    }
}
