using System.Data;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;
using WebMySqlDemo.Modelos;
namespace SQLprueba.Pages;

public class IndexModel : PageModel
{
    public DataTable productos=new DataTable();
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        try
        {
        DataSet dst=new DataSet(); 
        MySQLaux aux=new MySQLaux("localhost","3306","prueba","root","admin");
        dst=aux.SeleccionTodo("productos");
        productos = dst.Tables[0];
        }
        catch(Exception e)
        {
            Console.WriteLine($"Error: {e}");
        }
    }
}