using System.Runtime.CompilerServices;

namespace Dominio.Interfaces
{
    public interface ICompraRepository : IRepositorioBase<Compra>
    {
        Task<Compra> ObtenerCompraPorNumeroFactura(string numeroFactura);
        Task<IEnumerable<Compra>> ObtenerComprasRealizadasEntreFechasAsync(DateTime desde, DateTime hasta);
        Task<IEnumerable<Compra>> ObtenerComprasEntregadasEntreFechasAsync(DateTime desde, DateTime hasta);
        Task<IEnumerable<Compra>> ObtenerComprasPorProveedorAsync(int proveedorId);
        Task<IEnumerable<Compra>> ObtenerComprasPorEmpleadoAsync(int empleadoId);
        Task<IEnumerable<Compra>> ObtenerComprasPorEstadoAsync(int estado);
        Task<IEnumerable<Compra>> ObtenerComprasPorRangoDePrecioAsync(decimal minimo, decimal max);
        Task CambiarEstadoDeCompraAsync(int compraId, int nuevoEstado);
    }
}
