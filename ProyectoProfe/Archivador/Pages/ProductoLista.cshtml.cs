using System.Collections;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Models;
namespace WebAppCRUDMySQL.Pages;

public class ProductoListaModel : PageModel
{
    private readonly MySqlAux aux = new MySqlAux("localhost", 3306, "prueba", "root", "admin");
    public List<Producto>? Productos { get; set; }

    public void OnGet()
    {
        Productos = Producto.ConvierteALista( 
            aux.SeleccionaTodo("producto")
        );
    }
    
}

