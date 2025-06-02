using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentacion.Models
{
    public class ProductoFormViewModel
    {
        public ProductoDetalleViewModel Producto { get; set; } = new();
        public string Accion { get; set; } = "agregar";
        public List<SelectListItem> CategoriasLista { get; set; } = new();
    }
}
