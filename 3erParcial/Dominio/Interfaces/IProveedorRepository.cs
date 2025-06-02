namespace Dominio.Interfaces
{
    public interface IProveedorRepository : IRepositorioBase<Proveedor>
    {
        Task CambiarEstadoDeProveedorAsync(int idProveedor, bool estado);
        Task<IEnumerable<Proveedor>> ObtenerProveedoresFiltradoGeneralAsync(
            int ordenarPor=1,
            bool ordenAscendente=true,
            int pagina = 1,
            int tamanoPagina = 10,
            string? nombre = null,
            string? telefono = null,
            string? email = null,
            bool? estado = null);
    }
}
