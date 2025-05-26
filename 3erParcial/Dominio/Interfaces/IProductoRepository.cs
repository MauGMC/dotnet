namespace Dominio.Interfaces
{
    public interface IProductoRepository : IRepositorioBase<Producto>
    {
        Task<IEnumerable<Producto>> ObtenerProductosPorNombreAsync(string nombre);
        Task<IEnumerable<Producto>> ObtenerProductosPorCategoriaAsync(int categoriaId);
        Task<IEnumerable<Producto>> ObtenerProductosPorRangoDePreciosAsync(decimal precioMinimo, decimal precioMaximo);
        Task<IEnumerable<Producto>> ObtenerProductosConInventarioBajoAsync(int cantidadMinima=10);
        Task<IEnumerable<Producto>> ObtenerProductosConInventarioAltoAsync(int cantidadMaxima = 500);
        Task<IEnumerable<Producto>> ObtenerProductosPorRangoDeFechaDeRegistroAsync(DateTime desde, DateTime hasta);
        Task EliminarProductosAsync(IEnumerable<int> productosIds);

    }
}
