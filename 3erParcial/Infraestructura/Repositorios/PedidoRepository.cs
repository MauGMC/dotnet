
using Aplicacion.Enums;
using Aplicacion.Extensions;

namespace Infraestructura.Repositorios
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly ApplicationDbContext _context;
        public PedidoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarAsync(Pedido entidad)
        {
            _context.Pedidos.Update(entidad);
            await GuardarCambiosAsync();
        }

        public async Task AgregarAsync(Pedido entidad)
        {
            _context.Pedidos.Add(entidad);
            await GuardarCambiosAsync();
        }

        public async Task<Pedido> CambiarEstadoPedidoAsync(int pedidoID, int estadoNuevo)
        {
            var pedido = await ObtenerPorIdAsync(pedidoID);
            pedido.Estado = estadoNuevo;
            await GuardarCambiosAsync();
            return pedido;
        }

        public async Task CancelarPedidosAsync(IEnumerable<int> pedidosIDs)
        {
            var pedidos = await _context.Pedidos
                .Where(p => pedidosIDs.Contains(p.Estado))
                .ToListAsync();

            foreach (var pedido in pedidos)
            {
                pedido.Estado = 0; 
            }

            await GuardarCambiosAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await ObtenerPorIdAsync(id);
            _context.Pedidos.Remove(entidad);
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
                throw new Exception("Se produjo un error al guardar los cambios del pedido.", ex);
            }
        }

        public async Task<IEnumerable<Pedido>> ObtenerPedidosFiltradoGeneral(int ordenarPor = 1, bool ordenAscendente = true, int pagina = 1, int tamanoPagina = 10, int? clienteID = null, int? estado = null, DateTime? fechaPedidoDesde = null, DateTime? fechaPedidoHasta = null, DateTime? fechaEntregaDesde = null, DateTime? fechaEntregaHasta = null, decimal totalMinimo = 0, decimal totalMaximo = decimal.MaxValue)
        {
            var query = _context.Pedidos.AsQueryable();

            if (clienteID.HasValue)
                query = query.Where(p => p.ClienteID == clienteID.Value);

            if (estado.HasValue)
                query = query.Where(p => p.Estado == estado.Value);

            if (fechaPedidoDesde.HasValue)
                query = query.Where(p => p.FechaPedido >= fechaPedidoDesde.Value);

            if (fechaPedidoHasta.HasValue)
                query = query.Where(p => p.FechaPedido <= fechaPedidoHasta.Value);

            if (fechaEntregaDesde.HasValue)
                query = query.Where(p => p.FechaEntrega >= fechaEntregaDesde.Value);

            if (fechaEntregaHasta.HasValue)
                query = query.Where(p => p.FechaEntrega <= fechaEntregaHasta.Value);

            OrdenarPedidoPor orden;
            if (!Enum.IsDefined(typeof(OrdenarPedidoPor), ordenarPor))
                orden = OrdenarPedidoPor.PedidoID;
            else
                orden = (OrdenarPedidoPor)ordenarPor;

            query = query.OrdenarPor(orden, ordenAscendente);

                query = query
                    .Skip((pagina - 1) * tamanoPagina)
                    .Take(tamanoPagina);

            return await query.ToListAsync();
        }

        public async Task<Pedido> ObtenerPorIdAsync(int id)
        {
            return await _context.Pedidos.FindAsync(id);
        }

        public async Task<IEnumerable<Pedido>> ObtenerTodosAsync()
        {
            return await _context.Pedidos.ToListAsync();
        }
    }
}
