
namespace Infraestructura.Repositorios
{
    public class NotificacionPorUsuarioRepository : INotificacionPorUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        public NotificacionPorUsuarioRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task ActualizarAsync(NotificacionPorUsuario entidad)
        {
            _context.Update(entidad);
            await GuardarCambiosAsync();
        }

        public async Task AgregarAsync(NotificacionPorUsuario entidad)
        {
            _context.NotificacionesPorUsuarios.Add(entidad);
            await GuardarCambiosAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad= await ObtenerPorIdAsync(id);
            _context.NotificacionesPorUsuarios.Remove(entidad);
            await GuardarCambiosAsync();
        }

        public async Task EliminarTodasAsync(IEnumerable<int> notificacionIds)
        {
            var entidades = _context.NotificacionesPorUsuarios
                .Where(n => notificacionIds.Contains(n.NotificacionPorUsuarioID))
                .ToList();
            _context.NotificacionesPorUsuarios.RemoveRange(entidades);
            await GuardarCambiosAsync();
        }

        public async Task GuardarCambiosAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error al guardar los cambios en las notificaciones por usuario.", ex);
            }
        }

        public async Task<NotificacionPorUsuario> MarcarComoLeidaAsync(int notificacionPorUsuarioId)
        {
            var notificacion = await ObtenerPorIdAsync(notificacionPorUsuarioId);
            if (notificacion == null)
            {
                throw new KeyNotFoundException($"Notificación con ID {notificacionPorUsuarioId} no encontrada.");
            }
            if(!notificacion.Leido)
            {
                notificacion.Leido = true;
                await ActualizarAsync(notificacion);
                await GuardarCambiosAsync();
            }
            return notificacion;
        }

        public async Task MarcarComoLeidasAsync(IEnumerable<int> notificacionIds)
        {
            var ids = notificacionIds.Distinct().ToList();

            var notificaciones = await _context.NotificacionesPorUsuarios
                .Where(n => ids.Contains(n.NotificacionPorUsuarioID) && !n.Leido)
                .ToListAsync();
            foreach (var notificacion in notificaciones)
            {
                notificacion.Leido = true;
                _context.NotificacionesPorUsuarios.Update(notificacion);
            }
            await GuardarCambiosAsync();
        }

        public async Task<IEnumerable<NotificacionPorUsuario>> ObtenerNotificacionesPorUsuarioAsync(string usuarioId)
        {
            return await _context.NotificacionesPorUsuarios
                .Where(n => n.UsuarioID == usuarioId)
                .ToListAsync();
        }

        public async Task<IEnumerable<NotificacionPorUsuario>> ObtenerPorFechaCreacionAsync(string filtro)
        {
            return await _context.NotificacionesPorUsuarios
                .Where(n => n.FechaCreacion.ToString().Contains(filtro))
                .ToListAsync();
        }

        public async Task<NotificacionPorUsuario> ObtenerPorIdAsync(int id)
        {
            return await _context.NotificacionesPorUsuarios.FindAsync(id)
                ?? throw new KeyNotFoundException($"Notificación con ID {id} no encontrada.");
        }

        public async Task<IEnumerable<NotificacionPorUsuario>> ObtenerTodosAsync()
        {
            return await _context.NotificacionesPorUsuarios.ToListAsync();
        }
    }
}
