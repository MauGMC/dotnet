using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Entidades;
using WebMySqlDemo.Modelos;

namespace SQLprueba.Pages;

public class AgregarModel : PageModel
{
    private readonly MySQLaux aux=new MySQLaux("localhost","3306","prueba","root","admin");
    private readonly ILogger<AgregarModel> _logger;

    public AgregarModel(ILogger<AgregarModel> logger)
    {
        _logger = logger;
    }

    [BindProperty]
    public Producto Producto { get; set; }

    public List<SelectListItem> CategoriasLista { get; set; }

    public void OnGet()
    {
        CargarCategorias();
    }

    public IActionResult OnPost()
    {
        CargarCategorias();

        if (!ModelState.IsValid)
        {
            return Page(); 
        }
        aux.AgregarNuevo("productos",Producto.Nombre,Producto.Descripcion,Producto.Precio,Producto.Cantidad,Producto.Categoria);
        return RedirectToPage("/Index");
    }

    private void CargarCategorias()
    {
        CategoriasLista = Enum.GetValues(typeof(Categorias))
            .Cast<Categorias>()
            .Select(c => new SelectListItem
            {
                Value = ((int)c).ToString(),
                Text = c.ToString()
            })
            .ToList();
    }
}