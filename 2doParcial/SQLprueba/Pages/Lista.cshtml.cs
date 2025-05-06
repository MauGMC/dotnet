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
    public List<Producto> Productos { get; set; }
    [BindProperty]
    public Producto Producto { get; set; }
    
    public IActionResult OnGet()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
        {
            return RedirectToPage("/Login");
        }
        Productos=Producto.ConvierteALista(aux.SeleccionTodo("productos"));   
        return Page();
    } 

}