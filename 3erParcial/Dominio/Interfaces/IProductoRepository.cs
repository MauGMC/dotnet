namespace Dominio.Interfaces
{
    public interface IProductoRepository : IRepositorioBase<Producto>
    {
        Task EliminarProductosAsync(IEnumerable<int> productosIds);
        Task<IEnumerable<Producto>> ObtenerProductosFiltradoGeneralAsync(
            int ordenarPor=1,
            bool ordenAscendente=true,
            int pagina=1,
            int tamanoPagina=10,
            string? nombre = null,
            int? categoriaId = null,
            decimal? precioMinimo = null,
            decimal? precioMaximo = null,
            DateTime? fechaRegistroDesde = null,
            DateTime? fechaRegistroHasta = null);

    }
}
