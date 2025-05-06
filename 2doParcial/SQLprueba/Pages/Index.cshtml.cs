using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;
using Microsoft.AspNetCore.Http;
using WebMySqlDemo.Modelos;
using Entidades;
using SQLprueba.Pages.Entidades;
namespace SQLprueba.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MySQLaux aux = new MySQLaux("localhost", "3306", "prueba", "root", "admin");
        [BindProperty]
        public Usuarios Usuarios { get; set; }
        
    }
}
