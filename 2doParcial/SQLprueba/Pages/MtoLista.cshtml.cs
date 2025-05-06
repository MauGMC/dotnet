using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Entidades;
using WebMySqlDemo.Modelos;

namespace SQLprueba.Pages
{
    public class MtoListaModel : PageModel
    {
        private readonly MySQLaux aux=new MySQLaux("localhost","3306","prueba","root","admin");
        [BindProperty]
        public Producto Producto { get; set; } = new Producto();

        public List<SelectListItem> CategoriasLista { get; set; }

        public IActionResult OnGet(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
            {
                return RedirectToPage("/Login");
            }
            if (id==-1)
            {
                Producto = new Producto()
                {
                    ID = -1,
                    Nombre = "",
                    Precio = 0,
                    Cantidad = 0,
                    Descripcion = "",
                    Categoria = 0
                };
            }
            else
            {
                Producto = aux.SeleccionPorID(id);
            }
            CategoriasLista = ObtenerCategorias();
            return Page();
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
