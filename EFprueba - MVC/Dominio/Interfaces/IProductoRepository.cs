using Dominio.Entidades;
namespace Dominio.Interfaces
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> ObtenerTodosAsync();
        Task<Producto?> ObtenerPorIdAsync(int id);
        Task AgregarAsync(Producto producto);
        Task ActualizarAsync(Producto producto);
        Task EliminarAsync(int id);
        Task<(List<Producto> Productos, int TotalRegistros)> ObtenerPaginadoAsync(int pagina, int tamañoPagina);
        Task AgregarRangoAsync(IEnumerable<Producto> productos);
        Task <bool>ExistenciaPorIdONombre(int id, string nombre);
    }
}
