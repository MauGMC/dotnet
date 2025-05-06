using System.Collections;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Models;
using MySqlConnector;

namespace WebAppCRUDMySQL.Pages;

public class ProductoMtoModel : PageModel
{
    private readonly MySqlAux aux = new MySqlAux("localhost", 3306, "prueba", "root", "admin");
    public Producto Producto { get; set; } = new Producto
    {
        Id = -1,
        Nombre = "",
        Precio = 0,
        Descripcion = ""
    };

    public void OnGet(int id)
    {
        this.Producto = new Producto()
        {
            Id = -1,
            Nombre = "",
            Precio = 0,
            Descripcion = ""
        };
    }
}