using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebMySqlDemo.Modelos;
using System.Data;
using Entidades;
namespace SQLprueba.Pages
{
    public class ProcesarCRUDModel : PageModel
    {
        private readonly MySQLaux aux = new MySQLaux("localhost", "3306", "prueba", "root", "admin");
        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
            {
                return RedirectToPage("/Login");
            }
            return Page();
        }
        [ValidateAntiForgeryToken]
        public JsonResult OnPost([FromBody] Producto producto)
        {
            Console.WriteLine("Estoy dentro");
            if (producto==null)
            {
                Console.WriteLine("Producto está vacío");
                return new JsonResult(new { success = false, message = "El producto no se recibió"});
            }
            try
            {
                if(producto.ID==-1)
                {
                    aux.AgregarNuevo("productos", producto.Nombre, producto.Descripcion, producto.Precio, producto.Cantidad, producto.Categoria);
                    return new JsonResult(new { success = true, message = "Producto agregado correctamente" });
                }
                else
                {
                    aux.EditarProducto("productos", producto.ID, producto.Nombre, producto.Descripcion, producto.Precio, producto.Cantidad, producto.Categoria);
                    return new JsonResult(new { success = true, message = "Producto editado correctamente" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return new JsonResult(new { success = false, message = "Error al procesar la solicitud" });
            }
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostEliminar([FromBody]int id)
        {
            aux.EliminarProducto("productos", id);
            return new JsonResult(new { success = true, message = "Producto eliminado correctamente" });
        }
    }
}
