using System.Data;
using System.Linq.Expressions;
using Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySqlConnector;
using WebMySqlDemo.Modelos;
namespace SQLprueba.Pages;

public class IndexModel : PageModel
{
    public DataTable productos=new DataTable();
    public EditarModal editarModal { get; set; }
    public AgregarModal agregarModal { get; set; }
    private readonly MySQLaux aux = new MySQLaux("localhost", "3306", "prueba", "root", "admin");
    [BindProperty]
    public Producto Producto { get; set; }
    private List<SelectListItem> ObtenerCategorias()
    {
        return Enum.GetValues(typeof(Categorias))
            .Cast<Categorias>()
            .Select(c => new SelectListItem
            {
                Value = ((int)c).ToString(),
                Text = c.ToString()
            }).ToList();
    }


    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }
    private void CargarModales()
    {
        var categorias = ObtenerCategorias();
        agregarModal = new AgregarModal { Producto = new Producto(), CategoriasLista = categorias };
        editarModal = new EditarModal { Producto = new Producto(), CategoriasLista = categorias };
    }

    public void OnGet()
    {
        try
        {
            DataSet dst=new DataSet();
            dst= aux.SeleccionTodo("productos");
            productos=dst.Tables[0];
            CargarModales();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al mostrar producto.");
        }
    }


    public IActionResult OnPostAgregarProducto()
    {
        agregarModal = new AgregarModal
        {
            Producto = new Producto(),
            CategoriasLista = ObtenerCategorias()
        };

        if (!ModelState.IsValid)
        {
            return Page();
        }
        aux.AgregarNuevo("productos", Producto.Nombre, Producto.Descripcion, Producto.Precio, Producto.Cantidad, Producto.Categoria);
        return RedirectToPage("/Index");
    }
    /*
    public IActionResult OnPost()
    {
        CargarCategorias();

        if (!ModelState.IsValid)
        {
            return Page();
        }
        aux.AgregarNuevo("productos", Producto.Nombre, Producto.Descripcion, Producto.Precio, Producto.Cantidad, Producto.Categoria);
        return RedirectToPage("/Index");
    }*/
    
    /*
    public IActionResult OnGetEditarProducto(int id)
    {
        var productoEditar = aux.SeleccionPorID(id);

        if (productoEditar == null)
        {
            // Manejo del error si no existe el producto
            Console.WriteLine("Producto no encontrado.");
            return NotFound(); // Redirige a una página de error o muestra un mensaje apropiado
        }

        Producto = productoEditar;

        // Asegúrate de que la lista de categorías esté siendo asignada correctamente
        CategoriasLista = Enum.GetValues(typeof(Categorias))
            .Cast<Categorias>()
            .Select(c => new SelectListItem
            {
                Value = ((int)c).ToString(),
                Text = c.ToString()
            }).ToList();

        // Verifica que las categorías estén correctas
        if (CategoriasLista == null || !CategoriasLista.Any())
        {
            Console.WriteLine("Categorías no encontradas.");
            return BadRequest("No se encontraron categorías.");
        }

        editarModal = new EditarModal
        {
            Producto = Producto,
            CategoriasLista = CategoriasLista
        };

        return Page(); // Asegúrate de que la vista se cargue correctamente
    }*/


    public IActionResult OnPostEditarProducto()
    {
        editarModal = new EditarModal
        {
            Producto = new Producto(),
            CategoriasLista = ObtenerCategorias()
        };
        var productoEditar = aux.SeleccionPorID(Producto.ID);
        if (productoEditar == null)
        {
            return NotFound();
        }

        try
        {
            aux.EditarProducto(
                "productos",
                Producto.ID,
                Producto.Nombre,
                Producto.Descripcion,
                Producto.Precio,
                Producto.Cantidad,
                Producto.Categoria
            );
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al editar producto.");
            return Page(); 
        }

        return RedirectToPage("/Index"); 
    }
    [ValidateAntiForgeryToken]
    public IActionResult OnPostEliminarProducto(int id)
    {
        CargarModales();
        if (id <= 0)
            return BadRequest("ID inválido");

        var producto = aux.SeleccionPorID(id);
        if (producto == null)
            return NotFound("No existe el producto");

        aux.EliminarProducto("productos", id);
        return new JsonResult(new { mensaje = "Eliminado correctamente." });
    }
}