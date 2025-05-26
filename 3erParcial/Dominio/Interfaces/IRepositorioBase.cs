global using Dominio.Entidades;
namespace Dominio.Interfaces
{
    public interface IRepositorioBase<T> where T : class
    {
        Task<T> ObtenerPorIdAsync(int id);
        Task<IEnumerable<T>> ObtenerTodosAsync();
        Task AgregarAsync(T entidad);
        Task ActualizarAsync(T entidad);
        Task EliminarAsync(int id);
        Task GuardarCambiosAsync();
    }
}
