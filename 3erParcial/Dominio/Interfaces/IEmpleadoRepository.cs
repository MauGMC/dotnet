namespace Dominio.Interfaces
{
    public interface IEmpleadoRepository : IRepositorioBase<Empleado>
    {
        Task<Empleado> ObtenerPorNombreCompletoAsync(string nombreCompleto);
        Task<Empleado> ObtenerPorTelefonoAsync(string telefono);
        Task<IEnumerable<Empleado>> ObtenerPorRangoDeEdadAsync(DateTime desde, DateTime hasta);
        Task<IEnumerable<Empleado>> ObtenerPorSexoAsync(char sexo);
        Task<IEnumerable<Empleado>> ObtenerPorPuestoAsync(string puesto);
        Task<IEnumerable<Empleado>> ObtenerPorEstadoAsync(int estado);
        Task<IEnumerable<Empleado>> ObtenerPorFechaContratacionAsync(DateTime fechaContratacion);
        Task<IEnumerable<Empleado>> ObtenerPorRangoDeFechaDeContratacionAsync(DateTime desde, DateTime hasta);
        Task DarDeBajaAEmpleados(IEnumerable<int> empleadosIds);
    }
}
