using System.Runtime.CompilerServices;

namespace Dominio.Interfaces
{
    public interface ICompraRepository : IRepositorioBase<Compra>
    {
        Task CambiarEstadoDeCompraAsync(int compraId, int nuevoEstado);
        Task<IEnumerable<Compra>> ObtenerComprasFiltradoGeneralAsync(
            int ordernarPor=1,
            bool ordenAscendente=true,
            int pagina=1, 
            int tamanoPagina=10,
            string? numeroFactura = null, 
            int? proveedorId = null, 
            int? empleadoId = null, 
            int? estado = null, 
            DateTime? fechaCompraRealizadaDesde = null, 
            DateTime? fechaCompraRealizadaHasta = null, 
            DateTime? fechaEntregaDesde = null, 
            DateTime? fechaEntreagadaHasta = null, 
            decimal? precioMinimo = null, 
            decimal? precioMaximo = null);
    }
}
