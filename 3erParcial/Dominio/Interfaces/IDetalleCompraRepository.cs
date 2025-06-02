namespace Dominio.Interfaces
{
    public interface IDetalleCompraRepository : IRepositorioBase<DetalleCompra>
    {
        Task<IEnumerable<DetalleCompra>> ObtenerDetallesPorCompraIdAsync(int compraId);
        Task<IEnumerable<(Producto producto, int cantidadComprada)>> ObtenerProductosMasCompradosAsync(int top = 5);
        Task<IEnumerable<(Producto producto, int cantidadComprada)>> ObtenerProductosMenosCompradosAsync(int bottom = 5);
    
    }
}
