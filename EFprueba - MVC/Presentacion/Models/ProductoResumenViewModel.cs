using Aplicacion.DTOs;

namespace Presentacion.Models
{
    public class ProductoResumenViewModel
    {
        public IEnumerable<ProductoResumenDTO> Productos { get; set; }
        public decimal PrecioTotal { get; set; }
    }
}
