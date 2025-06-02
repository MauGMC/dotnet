using Dominio.Interfaces;
using Dominio.Entidades;
namespace Aplicacion.Servicios
{
    public class ProductoService
    {
        private readonly IProductoRepository _productoRepo;
        public ProductoService(IProductoRepository productoRepo)
        {
            _productoRepo = productoRepo;
        }
        public async Task<IEnumerable<Producto>> ObtenerTodosAsync()
        {
            return await _productoRepo.ObtenerTodosAsync();
        }
        public async Task<IEnumerable<Producto>> ObtenerFiltradosAsync()
        {
            return await _productoRepo.ObtenerProductosFiltradoGeneralAsync();
        }
        public async Task<Producto?> ObtenerPorIDAsync(int id)
        {
            return await _productoRepo.ObtenerPorIdAsync(id);
        }
        public async Task CrearAsync(Producto producto)
        {
            await _productoRepo.AgregarAsync(producto);
        }
        public async Task ActualizarAsync(Producto producto)
        {
            await _productoRepo.ActualizarAsync(producto);
        }
        public async Task EliminarAsync(int id)
        {
            await _productoRepo.EliminarAsync(id);
        }

    }
}
