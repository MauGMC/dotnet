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
    public int PaginaActual{get;set;}
    public int TotalPaginas{get;set;}
    
    public IActionResult OnGet(int pagina = 1)
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
        {
            return RedirectToPage("/Login");
        }

        int ProductosPorPagina = 5;
        
        var todosProductos = Producto.ConvierteALista(aux.SeleccionTodo("productos"));
        int totalProductos = todosProductos.Count;

        TotalPaginas = (int)Math.Ceiling(totalProductos / (double)ProductosPorPagina);
        PaginaActual = pagina;

        Productos = todosProductos
            .Skip((pagina - 1) * ProductosPorPagina)
            .Take(ProductosPorPagina)
            .ToList();

        return Page();
    }


}