namespace Dominio.Interfaces
{
    public interface IEmpleadoRepository : IRepositorioBase<Empleado>
    {
        Task DarDeBajaAEmpleados(IEnumerable<int> empleadosIds);
        Task<IEnumerable<Empleado>> ObtenerEmpleadosFiltradoGeneralAsync(
            int ordenarPor=1,
            bool ordernarAscendente=true,
            int pagina=1,
            int tamanoPagina = 10,
            string? nombreCompleto = null,
            string? telefono = null,
            DateTime? fechaContratacionDesde = null,
            DateTime? fechaContratacionHasta = null,
            char? sexo = null,
            string? puesto = null,
            int? estado = null);
    }
}
