namespace Dominio.Interfaces
{
    public interface IProductoEnCarritoRepository : IRepositorioBase<ProductoEnCarrito>
    {
        //Consultas de filtrado general
        Task<IEnumerable<ProductoEnCarrito>> ObtenerProductosEnCarritoAsync(int carritoId);
        Task<IEnumerable<ProductoEnCarrito>> ObtenerProductosPorMenorCantidadAsync(int carritoId, int cantidadMin = 1);
        Task<IEnumerable<ProductoEnCarrito>> ObtenerProductosPorMayorCantidadAsync(int carritoId, int cantidadMax = 100);
        Task<IEnumerable<ProductoEnCarrito>> ObtenerProductosPorRangoDePrecioAsync(int carritoId, decimal precioMin = 0, decimal precioMax = 10000);
        //Consultas de filtrado especifico
        Task<ProductoEnCarrito> ObtenerProductoEnCarritoAsync(int carritoId, int productoId);
        Task<ProductoEnCarrito> ObtenerPorNombreAsync(int carritoId, string nombreProducto);
        //Acciones individuales al carrito
        Task EliminarProductoDelCarritoAsync(int carritoId, int productoId);
        Task<bool> ExisteProductoEnCarritoAsync(int carritoId, int productoId);
        Task IncrementarCantidadAsync(int carritoId, int productoId, int cantidad = 1);
        Task DisminuirCantidadAsync(int carritoId, int productoId, int cantidad = 1);
        //Acciones masivas al carrito
        Task AgregarProductosAlCarritoAsync(IEnumerable<ProductoEnCarrito> productos);
        Task EliminarProductosDelCarritoAsync(IEnumerable<ProductoEnCarrito> productos);


    }
}
