using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Entidades;
using WebMySqlDemo.Modelos;

namespace SQLprueba.Pages;

public class EditarModel : PageModel
{
    private readonly MySQLaux aux=new MySQLaux("localhost","3306","prueba","root","admin");
    private readonly ILogger<AgregarModel> _logger;

    public EditarModel(ILogger<AgregarModel> logger)
    {
        _logger = logger;
    }

    [BindProperty]
    public Producto Producto { get; set; }

    public List<SelectListItem> CategoriasLista { get; set; }

    public void OnGet()
    {
        
    }
}