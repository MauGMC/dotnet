using Aplicacion.Servicios;
using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ProductoService _productoService;
        public ProductosController(ProductoService productoService)
        {
            _productoService = productoService;
        }

        //GET: Productos
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var productos = await _productoService.ObtenerFiltradosAsync();
            return View(productos);
        }
        [Authorize]
        //GET: Detalles
        public async Task<IActionResult> Detalles(int id)
        {
            var producto = await _productoService.ObtenerPorIDAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }
        [Authorize]
        //GET: Crear
        public IActionResult Crear()
        {
            return View();
        }
        [Authorize]
        //POST: Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Producto producto)
        {
            if (ModelState.IsValid)
            {
                await _productoService.CrearAsync(producto);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [Authorize]
        //GET: Editar
        public async Task<IActionResult> Editar(int id)
        {
            var producto = await _productoService.ObtenerPorIDAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }
        [Authorize]
        //POST: Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Producto producto)
        {
            if (id != producto.ProductoID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _productoService.ActualizarAsync(producto);
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }
        [Authorize]
        //GET: Eliminar
        public async Task<IActionResult> Eliminar(int id)
        {
            var producto = await _productoService.ObtenerPorIDAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }
        [Authorize]
        //POST: Eliminar
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarEliminacion(int id)
        {
            await _productoService.EliminarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
