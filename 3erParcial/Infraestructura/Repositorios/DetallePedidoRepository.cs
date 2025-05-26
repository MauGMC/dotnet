
namespace Infraestructura.Repositorios
{
    public class DetallePedidoRepository : IDetallePedidoRepository
    {
        private readonly ApplicationDbContext _context;
        public DetallePedidoRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task ActualizarAsync(DetallePedido entidad)
        {
            _context.DetallesPedidos.Update(entidad);
            await GuardarCambiosAsync();
        }

        public async Task AgregarAsync(DetallePedido entidad)
        {
            _context.DetallesPedidos.Add(entidad);
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
                throw new Exception("Error al guardar los cambios en el detalle de pedido.", ex);
            }
        }

        public async Task<IEnumerable<DetallePedido>> ObtenerDetallesDePedidos(int pedidoId)
        {
            return await _context.DetallesPedidos
                .Where(dp => dp.PedidoID == pedidoId)
                .ToListAsync();
        }

        public async Task<DetallePedido> ObtenerPorIdAsync(int id)
        {
            return await _context.DetallesPedidos.FindAsync(id);

        }

        public async Task<IEnumerable<DetallePedido>> ObtenerTodosAsync()
        {
            return await _context.DetallesPedidos.ToListAsync();
        }
    }
}
