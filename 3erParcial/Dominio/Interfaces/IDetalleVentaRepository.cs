namespace Dominio.Interfaces
{
    public interface IDetalleVentaRepository : IRepositorioBase<DetalleVenta>
    {
        Task<IEnumerable<DetalleVenta>> ObtenerDetallesPorVentaIdAsync(int ventaId);
        Task<IEnumerable<(Producto producto, int cantidadVendida)>> ObtenerProductosMasVendidosAsync(int top = 5);
        Task<IEnumerable<(Producto producto, int cantidadVendida)>> ObtenerProductosMenosVendidosAsync(int bottom = 5);
    }
}
