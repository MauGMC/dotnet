namespace Dominio.Interfaces
{
    public interface IClienteRepository : IRepositorioBase<Cliente>
    {
        Task<IEnumerable<Cliente>> ObtenerClientePorNombreAsync(string cadena);
        Task<IEnumerable<Cliente>> ObtenerClientesPorSexoAsync(string sexo);
        Task<IEnumerable<Cliente>> ObtenerClientesNacidosEntreAsync(DateTime desde, DateTime hasta);
    }
}
