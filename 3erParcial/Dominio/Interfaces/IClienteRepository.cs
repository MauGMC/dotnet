namespace Dominio.Interfaces
{
    public interface IClienteRepository : IRepositorioBase<Cliente>
    {
        Task<IEnumerable<Cliente>> ObtenerClientesFiltradoGeneralAsync
        (
            int ordenarPor = 1,
            bool ordenAscendente = true,
            int pagina = 1,
            int tamanoPagina = 10,
            string? nombreCompleto = null,
            string? telefono = null,
            DateTime? fechaNacimientoDesde = null,
            DateTime? fechaNacimientoHasta = null,
            char? sexo = null
        );

    }
}
