using Aplicacion.DTOs;
using AutoMapper;
using Dominio.Entidades;
using Dominio.Interfaces;

namespace Aplicacion.Servicios
{
    public class ProductoService
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;
        public ProductoService(IProductoRepository productoRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }
        public async Task AgregarAsync(Producto producto)
        {
            Console.WriteLine($"Intentando agregar el producto: {producto.Nombre}");
            await _productoRepository.AgregarAsync(producto);
        }

        public async Task ActualizarAsync(Producto producto)
        {
            Console.WriteLine($"Intentando actualizar el producto: {producto.Nombre}");
            await _productoRepository.ActualizarAsync(producto);
        }

        public async Task EliminarAsync(int id)
        {
            Console.WriteLine($"Intentando agregar el producto: {id}");
            await _productoRepository.EliminarAsync(id);
        }
        

    }
}
