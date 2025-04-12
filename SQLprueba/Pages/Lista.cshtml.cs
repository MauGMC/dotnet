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
    private readonly MySQLaux aux=new MySQLaux("localhost","3306","prueba","root","admin");
    private readonly ILogger<AgregarModel> _logger;

    public ListaModel(ILogger<AgregarModel> logger)
    {
        _logger = logger;
    }

    [BindProperty]
    public Producto Producto { get; set; }

    public List<SelectListItem> CategoriasLista { get; set; }

    public void OnGet(int id)
    {
        //Nuevo producto
        if(id==-1)
        {
            this.Producto=new Producto();
            /*
            this.Producto=new Producto(){
                ID=-1,
                Nombre="",
                Descripcion="",
                Precio=-1,
                Cantidad=-1,
                Categoria=1
            };
            */
        }
    }
    public void OnPost(Producto p)
    {

    }
}