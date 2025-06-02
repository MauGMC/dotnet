namespace Dominio.Interfaces
{
    public interface IProductoEnCarritoRepository : IRepositorioBase<ProductoEnCarrito>
    {
        //Consultas de filtrado especifico
        Task<ProductoEnCarrito> ObtenerProductoEnCarritoAsync(int carritoId, int productoId);
        //Acciones individuales al carrito
        Task EliminarProductoDelCarritoAsync(int carritoId, int productoId);
        Task<bool> ExisteProductoEnCarritoAsync(int carritoId, int productoId);
        Task IncrementarCantidadAsync(int carritoId, int productoId, int cantidad = 1);
        Task DisminuirCantidadAsync(int carritoId, int productoId, int cantidad = 1);
        //Acciones masivas al carrito
        Task AgregarProductosAlCarritoAsync(IEnumerable<ProductoEnCarrito> productos);
        Task EliminarProductosDelCarritoAsync(IEnumerable<ProductoEnCarrito> productos);
        //Filtrado general
        Task<IEnumerable<ProductoEnCarrito>> ObtenerProductosEnCarritoFiltradoGeneralAsync(
            int ordernarPor=1,
            bool ordenAscendente=true,
            int pagina = 1,
            int tamanoPagina = 10,
            int? carritoId=null,
            string? nombre = null,
            decimal? precioMin = null,
            decimal? precioMax = null,
            int? cantidadMin = null,
            int? cantidadMax = null);


    }
}
