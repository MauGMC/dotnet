
namespace Infraestructura.Repositorios
{
    public class NotificacionRepository : INotificacionRepository
    {
        private readonly ApplicationDbContext _context;
        public NotificacionRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task ActualizarAsync(Notificacion entidad)
        {
            _context.Update(entidad);
            await GuardarCambiosAsync();
        }

        public async Task AgregarAsync(Notificacion entidad)
        {
            _context.Add(entidad);
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
                throw new Exception("Error al guardar los cambios en la notificación.", ex);
            }
        }
        public async Task<Notificacion> ObtenerPorIdAsync(int id)
        {
            return await _context.Notificaciones.FindAsync(id);
        }

        public async Task<IEnumerable<Notificacion>> ObtenerTodosAsync()
        {
            return await _context.Notificaciones.ToListAsync();
        }
    }
}
