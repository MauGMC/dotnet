namespace Dominio.Interfaces
{
    public interface IDetallePedidoRepository : IRepositorioBase<DetallePedido>
    {
        Task<IEnumerable<DetallePedido>> ObtenerDetallesDePedidosAsync(int pedidoId);
    }
}
