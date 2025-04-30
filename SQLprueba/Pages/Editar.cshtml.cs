using System.Data;
using System.Linq.Expressions;
using Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySqlConnector;
using WebMySqlDemo.Modelos;

namespace SQLprueba.Pages
{
    public class EditarModel : PageModel
    {
        private readonly MySQLaux aux=new MySQLaux("localhost","3306","prueba","root","admin");
        [BindProperty]
        public Producto Producto { get; set; } = new Producto();

        public List<SelectListItem> CategoriasLista { get; set; }

        public IActionResult OnGet(int idProducto)
        {
            Producto = aux.SeleccionPorID(idProducto);

            if (Producto == null)
            {
                return NotFound();
            }

            CategoriasLista = ObtenerCategorias();
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            switch (id)
            {
                case 0:
                    aux.EditarProducto("productos", Producto.ID, Producto.Nombre, Producto.Descripcion, Producto.Precio, Producto.Cantidad, Producto.Categoria);
                    break;
            }

            return RedirectToPage("/Index");
        }

        private List<SelectListItem> ObtenerCategorias()
        {
            return Enum.GetValues(typeof(Categorias))
                .Cast<Categorias>()
                .Select(c => new SelectListItem
                {
                    Value = ((int)c).ToString(),
                    Text = c.ToString()
                }).ToList();
        }
    }
}
