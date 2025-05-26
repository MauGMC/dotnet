namespace Dominio.Interfaces
{
    public interface IPedidoRepository : IRepositorioBase<Pedido>
    {
        Task<Pedido> CambiarEstadoPedidoAsync(int pedidoID, int estadoNuevo);
        Task CancelarPedidosAsync(IEnumerable<int> pedidosIDs);
        Task<IEnumerable<Pedido>> ObtenerPedidosPorClienteIDAsync(int clienteID);
        Task<IEnumerable<Pedido>> ObtenerPedidosPorEstadoAsync(int estado);
        Task<IEnumerable<Pedido>> ObtenerPedidosPorRangoDeFechaPedidoAsync(DateTime desde, DateTime hasta);
        Task<IEnumerable<Pedido>> ObtenerPedidosPorRangoDeFechaEntregaAsync(DateTime desde, DateTime hasta);
    }
}
