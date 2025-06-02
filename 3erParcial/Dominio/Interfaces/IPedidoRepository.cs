namespace Dominio.Interfaces
{
    public interface IPedidoRepository : IRepositorioBase<Pedido>
    {
        Task<Pedido> CambiarEstadoPedidoAsync(int pedidoID, int estadoNuevo);
        Task CancelarPedidosAsync(IEnumerable<int> pedidosIDs);
        Task<IEnumerable<Pedido>> ObtenerPedidosFiltradoGeneral(
            int ordenarPor=1,
            bool ordenAscendente=true,
            int pagina = 1,
            int tamanoPagina = 10,
            int? clienteID = null,
            int? estado = null,
            DateTime? fechaPedidoDesde = null,
            DateTime? fechaPedidoHasta = null,
            DateTime? fechaEntregaDesde = null,
            DateTime? fechaEntregaHasta = null,
            decimal totalMinimo=0,
            decimal totalMaximo=decimal.MaxValue);
    }
}
