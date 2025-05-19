using Aplicacion.DTOs;
using AutoMapper;
using Dominio.Interfaces;

namespace Aplicacion.Servicios
{
    public class ObtenerProductoService
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;
        public ObtenerProductoService(IProductoRepository productoRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }
        public async Task<List<ProductoResumenDTO>> ObtenerProductosResumen()
        {
            var productos = await _productoRepository.ObtenerTodosAsync();
            var productosDTO = _mapper.Map<List<ProductoResumenDTO>>(productos);
            return productosDTO;
        }
        public async Task<ProductoDetalleDTO> ObtenerProductoDetalle(int id)
        {
            var producto = await _productoRepository.ObtenerPorIdAsync(id);
            return _mapper.Map<ProductoDetalleDTO>(producto);
        }
        public async Task<(List<ProductoResumenDTO> Productos, int TotalRegistros)> ObtenerProductosResumenPaginado(int pagina, int tamañoPagina)
        {
            var (productos, total) = await _productoRepository.ObtenerPaginadoAsync(pagina, tamañoPagina);
            var productosDTO = _mapper.Map<List<ProductoResumenDTO>>(productos);

            return (productosDTO, total);
        }

    }
}
