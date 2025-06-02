using Aplicacion.DTOs;

namespace Web.ViewModels
{
    public class ProductoResumenViewModel
    {
        public IEnumerable<ProductoResumenDTO> Productos { get; set; }
        public decimal PrecioTotal { get; set; }
    }
}
