namespace Dominio.Interfaces
{
    public interface IVentaRepository : IRepositorioBase<Venta>
    {
        Task<IEnumerable<Venta>> ObtenerVentasPorClientesAsync(int clienteId);
        Task<IEnumerable<Venta>> ObtenerVentasPorRangoDeFechasAsync(DateTime desde, DateTime hasta);
        Task<IEnumerable<Venta>> ObtenerVentasPorEstadoAsync(int estado);
        Task CambiarEstadoAsync(int ventaid,int estado);
    }
}
