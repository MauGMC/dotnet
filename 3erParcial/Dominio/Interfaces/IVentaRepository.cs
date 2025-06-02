namespace Dominio.Interfaces
{
    public interface IVentaRepository : IRepositorioBase<Venta>
    {
        Task<IEnumerable<Venta>> ObtenerVentasFiltradoGeneral(
            int ordenarPor=1,
            bool ordenAscendente=true,
            int pagina = 1,
            int tamanoPagina = 10,
            int? clienteId = null,
            int? metodoPagoId= null,
            DateTime? desde = null,
            DateTime? hasta = null,
            int? estado = null,
            decimal? totalMinimo = 0,
            decimal? totalMax = decimal.MaxValue
            );
        Task CambiarEstadoAsync(int ventaid,int estado);
    }
}
