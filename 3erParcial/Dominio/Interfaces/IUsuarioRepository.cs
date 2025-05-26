namespace Dominio.Interfaces
{
    public interface IUsuarioRepository : IRepositorioBase<Usuario>
    {
        //Consultas generales
        Task<IEnumerable<Usuario>> ObtenerPorRangoDeFechaDeRegistroAsync(DateTime desde, DateTime hasta);
        Task<IEnumerable<Usuario>> ObtenerPorUltimoAccesoAsync(string parametroFecha);
        Task<IEnumerable<Usuario>> ObtenerPorEstadoAsync(bool activo);
        //Consultas especificas
        Task<Usuario> ObtenerPorNombreDeUsuarioAsync(string nombreUsuario);
        Task<Usuario> ObtenerPorEmailAsync(string email);
        //Acciones especificas
        Task<Usuario> CambiarEstadoAsync(string idUsuario, bool estado);
        //Acciones masivas
        Task EliminarUsuariosAsync(IEnumerable<Usuario> idUsuarios);

    }
}
