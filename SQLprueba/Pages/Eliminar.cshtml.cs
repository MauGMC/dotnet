using Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebMySqlDemo.Modelos;

namespace SQLprueba.Pages
{
    public class EliminarModel : PageModel
    {
        private readonly MySQLaux aux = new MySQLaux("localhost", "3306", "prueba", "root", "admin");

        [BindProperty]
        public Producto Producto { get; set; } = new Producto();

        public IActionResult OnGet(int idProducto)
        {
            Producto = aux.SeleccionPorID(idProducto);

            if (Producto == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            if (id == -1)
            {
                aux.EliminarProducto("productos", Producto.ID);
            }

            return RedirectToPage("/Index");
        }
    }
}
