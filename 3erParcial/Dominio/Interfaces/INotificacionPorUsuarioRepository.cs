namespace Dominio.Interfaces
{
    public interface INotificacionPorUsuarioRepository : IRepositorioBase<NotificacionPorUsuario>
    {
        Task<IEnumerable<NotificacionPorUsuario>> ObtenerNotificacionesPorUsuarioAsync(string usuarioId);
        Task<IEnumerable<NotificacionPorUsuario>> ObtenerPorFechaCreacionAsync(string filtro);
        Task<NotificacionPorUsuario> MarcarComoLeidaAsync(int notificacionPorUsuarioId);
        Task MarcarComoLeidasAsync(IEnumerable<int> notificacionIds);
        Task EliminarTodasAsync(IEnumerable<int> notificacionIds);
    }
}
