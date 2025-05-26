namespace Dominio.Interfaces
{
    public interface IRolRepository : IRepositorioBase<Rol>
    {
        Task<IEnumerable<Rol>> ObtenerRolesPorNombreAsync(string nombre);
        Task<IEnumerable<Rol>> ObtenerRolesPorEstadoAsync(bool activo);
        Task CambiarEstadoDeRolAsync(int idRol, bool estado);
    }
}
