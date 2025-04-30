using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Entidades;
using WebMySqlDemo.Modelos;

namespace SQLprueba.Pages
{
    public class AgregarModel : PageModel
    {
        private readonly MySQLaux aux=new MySQLaux("localhost","3306","prueba","root","admin");
        [BindProperty]
        public Producto Producto { get; set; } = new Producto();

        public List<SelectListItem> CategoriasLista { get; set; }

        public void OnGet()
        {
            CategoriasLista = ObtenerCategorias();
        }

        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                CategoriasLista = ObtenerCategorias();
                return Page();
            }

            switch (id)
            {
                case 1:
                    aux.AgregarNuevo("productos", Producto.Nombre, Producto.Descripcion, Producto.Precio, Producto.Cantidad, Producto.Categoria);
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
