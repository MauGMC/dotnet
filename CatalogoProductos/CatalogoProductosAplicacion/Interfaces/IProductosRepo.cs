using CatalogoProductosDominio.Entidades;

namespace CatalogoProductosAplicacion.Interfaces
{
    public interface IProductosRepo
    {
        Task AddSync(Productos producto);
        Task<List<Productos>> GetAllAsync();
        Task<Productos?> GetById(int id);
        Task UpdateAsync(Productos producto);
    }
}
