namespace Dominio.Interfaces
{
    public interface IDetallePedidoRepository : IRepositorioBase<DetallePedido>
    {
        Task<IEnumerable<DetallePedido>> ObtenerDetallesDePedidos(int pedidoId);
    }
}
