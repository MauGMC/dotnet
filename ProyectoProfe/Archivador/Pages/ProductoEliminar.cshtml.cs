using System.Collections;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Models;

namespace WebAppCRUDMySQL.Pages;

public class ProductoEliminarModel : PageModel
{
    private readonly MySqlAux aux = new MySqlAux("localhost", 3306, "prueba", "root", "admin");
    public Producto Producto { get; set; } = new Producto
    {
        Id = -1,
        Nombre = "",
        Precio = 0,
        Descripcion = ""
    };
    
    private readonly ILogger<ProductoEliminarModel> _logger;

    public ProductoEliminarModel(ILogger<ProductoEliminarModel> logger)
    {
        _logger = logger;
    }

    public void OnGet(int id)
    {
        this.Producto = aux.Buscar(id);
        if (this.Producto == null)
        {
            Console.WriteLine("No se encontró producto con ID " + id);
        }
        
    }
}