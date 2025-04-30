using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Entidades;
using WebMySqlDemo.Modelos;

namespace SQLprueba.Pages;

public class ListaModel : PageModel
{
    private readonly MySQLaux aux = new MySQLaux("localhost", "3306", "prueba", "root", "admin");
    public int Categoria { get; set; }
    public string CategoriaNombre => ((Categorias)Categoria).ToString();
    public List<Producto> Productos { get; set; }
    [BindProperty]
    public Producto Producto { get; set; }
    private readonly ILogger<ListaModel> _logger;

    public ListaModel(ILogger<ListaModel> logger)
    {
        _logger = logger;
    }
    public void OnGet()
    {
        Productos = aux.ObtenerProductos(); 
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
    public IActionResult OnPost(int id)
    {   
        switch(id)
        {
            case 1:
            aux.AgregarNuevo("productos", Producto.Nombre, Producto.Descripcion, Producto.Precio, Producto.Cantidad, Producto.Categoria);
            break;
            case 0:
            aux.EditarProducto("productos",Producto.ID, Producto.Nombre, Producto.Descripcion, Producto.Precio, Producto.Cantidad, Producto.Categoria);
            break;
            case -1:
            aux.EliminarProducto("productos", Producto.ID);
            break;
            default:  _logger.LogWarning("ID de acción no reconocido: {id}", id);
            break;
        }
        return Page();
    }    

}