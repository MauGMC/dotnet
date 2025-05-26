namespace Dominio.Interfaces
{
    public interface IProveedorRepository : IRepositorioBase<Proveedor>
    {
        Task<Proveedor> ObtenerProveedorPorNombreAsync(string nombre);
        Task<Proveedor> ObtenerProveedorPorTelefonoAsync(string telefono);
        Task<Proveedor> ObtenerProveedorPorEmailAsync(string email);
        Task<IEnumerable<Proveedor>> ObtenerProveedoresPorEstadoAsync(bool estado);
        Task CambiarEstadoDeProveedorAsync(int idProveedor, bool estado);
    }
}
