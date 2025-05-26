namespace Dominio.Interfaces
{
    public interface IInventarioRepository : IRepositorioBase<Inventario>
    {
        Task<IEnumerable<Inventario>> ObtenerInventarioQueSeEstaAcabandoAsync(int minimoStock=10);
    }
}
