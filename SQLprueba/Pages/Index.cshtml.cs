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
    public List<SelectListItem> CategoriasLista { get; set; }
    
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
            MySQLaux aux= new MySQLaux("localhost", "3306", "prueba", "root", "admin");
            dst= aux.SeleccionTodo("productos");
            productos=dst.Tables[0];

            editarModal = new EditarModal
            {
                Producto = new Producto(),
                CategoriasLista = Enum.GetValues(typeof(Categorias))
                    .Cast<Categorias>()
                    .Select(c => new SelectListItem
                    {
                        Value = ((int)c).ToString(),
                        Text = c.ToString()
                    }).ToList()
            };
            agregarModal = new AgregarModal
            {
                Producto = new Producto(),
                CategoriasLista = Enum.GetValues(typeof(Categorias))
                    .Cast<Categorias>()
                    .Select(c => new SelectListItem
                    {
                        Value = ((int)c).ToString(),
                        Text = c.ToString()
                    }).ToList()
            };
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }


    public void OnPostAgregar()
    {
        agregarModal = new AgregarModal
        {
            Producto = new Producto(),
            CategoriasLista = Enum.GetValues(typeof(Categorias))
            .Cast<Categorias>()
            .Select(c => new SelectListItem
            {
                Value = ((int)c).ToString(),
                Text = c.ToString()
            }).ToList()
        };
    }
    public IActionResult OnPost()
    {
        CargarCategorias();

        if (!ModelState.IsValid)
        {
            return Page();
        }
        aux.AgregarNuevo("productos", Producto.Nombre, Producto.Descripcion, Producto.Precio, Producto.Cantidad, Producto.Categoria);
        return RedirectToPage("/Index");
    }
    private void CargarCategorias()
    {
        CategoriasLista = Enum.GetValues(typeof(Categorias))
            .Cast<Categorias>()
            .Select(c => new SelectListItem
            {
                Value = ((int)c).ToString(),
                Text = c.ToString()
            })
            .ToList();
    }
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


    public IActionResult OnPostEditarProducto(int id, Producto producto)
    {
        editarModal = new EditarModal
        {
            Producto = new Producto(),
            CategoriasLista = Enum.GetValues(typeof(Categorias))
                    .Cast<Categorias>()
                    .Select(c => new SelectListItem
                    {
                        Value = ((int)c).ToString(),
                        Text = c.ToString()
                    }).ToList()
        };
        Console.WriteLine("ID recibido: " + id);
        Console.WriteLine("Producto recibido: " + producto.Nombre);
        var productoEditar = aux.SeleccionPorID(Producto.ID);
        if (productoEditar == null)
        {
            return NotFound();  // Retorna una página de error si el producto no existe
        }

        try
        {
            // Actualiza el producto con los valores enviados en el formulario
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
            Console.WriteLine("Error al editar: " + e.Message);
            return Page();  // Si ocurre un error, vuelve a cargar la página
        }

        return RedirectToPage("/Index");  // Redirige a la página de índice después de editar
    }








}