using Aplicacion.DTOs;
using Aplicacion.Servicios;
using Aplicacion.Enums;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentacion.Models;
using Presentacion.Servicios;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Aplicacion.Servicios.Interfaces;
using Presentacion.Helpers;

namespace Presentacion.Controllers
{
    public class ProductoController : Controller
    {
        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }
        public int TamañoPagina { get; set; } = 10;
        public int PaginaAtras => Math.Max(PaginaActual - 2, 1);
        public int PaginaAdelante => Math.Min(PaginaActual + 2, TotalPaginas);
        private readonly ObtenerProductoService _obtenerProductoService;
        private readonly ProductoService _productoService;
        private readonly ImportarCsvService _importarCsvService;
        private readonly ReportePdfService _reportePdfService;
        private readonly RazorViewToStringRenderer _razorViewToStringRenderer;
        private readonly ICsvLogService _csvLogService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductoController(
            ObtenerProductoService obtenerProductoService,
            ProductoService productoService,
            ImportarCsvService importarCsvService,
            ICsvLogService csvLogService,
            ReportePdfService reportePdfService,
            RazorViewToStringRenderer razorViewToStringRenderer,
            IWebHostEnvironment webHostEnvironment,
            IMapper mapper)
        {
            _obtenerProductoService = obtenerProductoService;
            _productoService = productoService;
            _importarCsvService = importarCsvService;
            _csvLogService = csvLogService;
            _reportePdfService = reportePdfService;
            _razorViewToStringRenderer = razorViewToStringRenderer;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

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

        // Acción para mostrar la lista de productos
        [HttpGet]
        public async Task<IActionResult> Index(int pagina = 1, int tamañoPagina = 10)
        {
            var (productosDto, totalRegistros) = await _obtenerProductoService.ObtenerProductosResumenPaginado(pagina, tamañoPagina);
            decimal precioTotal = productosDto.Sum(p => p.Precio * p.Cantidad);

            var viewModel = new ProductoResumenViewModel
            {
                Productos = productosDto,
                PrecioTotal = precioTotal
            };

            return View(viewModel);
        }

        // Cargar partial view con formulario del producto para modal (Agregar, Editar, Detalles)
        [HttpGet]
        public async Task<IActionResult> ProductoForm(int productoId = 0, string accion = "agregar")
        {
            ProductoDetalleViewModel detalle;

            if (productoId == 0)
            {
                detalle = new ProductoDetalleViewModel { ProductoID = 0 };
            }
            else
            {
                var productoDto = await _obtenerProductoService.ObtenerProductoDetalle(productoId);
                detalle = _mapper.Map<ProductoDetalleViewModel>(productoDto);
            }

            var viewModel = new ProductoFormViewModel
            {
                Producto = detalle,
                Accion = accion,
                CategoriasLista = ObtenerCategorias(),
            };

            return PartialView("_ProductoForm", viewModel);
        }

        // POST para manejar agregar, editar o eliminar producto desde modal
        [HttpPost]
        public async Task<IActionResult> GuardarProducto(
            [FromForm(Name = "Producto.Accion")] string accion,
            [Bind(Prefix = "Producto")] ProductoDetalleViewModel producto)
        {
            ModelState.Clear();

            if (producto.Imagen != null && producto.Imagen.Length > 0)
            {
                string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(producto.Imagen.FileName);
                string rutaCarpeta = Path.Combine(_webHostEnvironment.WebRootPath, "imagenes");
                if (!Directory.Exists(rutaCarpeta))
                    Directory.CreateDirectory(rutaCarpeta);
                string rutaArchivo = Path.Combine(rutaCarpeta, nombreArchivo);

                using var stream = new FileStream(rutaArchivo, FileMode.Create);
                await producto.Imagen.CopyToAsync(stream);

                producto.RutaImagen = "imagenes/" + nombreArchivo;
            }

            if (!TryValidateModel(producto))
            {
                var errores = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                Console.WriteLine("Errores del modelo:");
                errores.ForEach(e => Console.WriteLine("- " + e));
                return Json(new { exito = false, mensaje = "El producto tiene errores." });
            }

            var productoMapeado = _mapper.Map<Dominio.Entidades.Producto>(producto);

            switch (accion?.ToLower())
            {
                case "agregar":
                    await _productoService.AgregarAsync(productoMapeado);
                    break;
                case "editar":
                    await _productoService.ActualizarAsync(productoMapeado);
                    break;
                case "eliminar":
                    await _productoService.EliminarAsync(productoMapeado.ProductoID);
                    break;
                default:
                    ModelState.AddModelError("", "Acción no válida.");
                    var vm = new ProductoFormViewModel
                    {
                        Producto = producto,
                        CategoriasLista = ObtenerCategorias()
                    };
                    return PartialView("_ProductoForm", vm);
            }

            return RedirectToAction(nameof(Index));
        }

        // Mostrar formulario para importar CSV
        [HttpGet]
        public IActionResult ImportarCsvForm()
        {
            return PartialView("_CsvForm", new CsvImportViewModel());
        }

        // POST para importar CSV
        [HttpPost]
        public async Task<IActionResult> ImportarCsv(CsvImportViewModel model)
        {
            ModelState.Clear();
            var archivoCsv = model.ArchivoCsv;

            if (archivoCsv == null || archivoCsv.Length == 0)
            {
                return Json(new { exito = false, mensaje = "El archivo CSV está vacío o no se seleccionó." });
            }
            else if (Path.GetExtension(archivoCsv.FileName).ToLower() != ".csv")
            {
                return Json(new { exito = false, mensaje = "El archivo no tiene extensión CSV." });
            }

            if (!TryValidateModel(model))
            {
                var errores = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { exito = false, mensaje = "El archivo CSV no es válido.", detalles = errores });
            }

            try
            {
                using var stream = archivoCsv.OpenReadStream();
                var resultadoImportacion = await _importarCsvService.LeerCsv(stream);

                var productosValidos = resultadoImportacion.ProductosImportados
                    .Where(r => r.Producto != null && !r.Errores.Any())
                    .Select(r => r.Producto)
                    .ToList();

                if (productosValidos.Any())
                {
                    await _importarCsvService.GuardarProductos(productosValidos);
                }
                else
                {
                    return Json(new { exito = false, mensaje = "No se encontraron productos válidos" });
                }

                var html = await _razorViewToStringRenderer.RenderViewToStringAsync(this, "~/Views/Productos/Templates/Csv_log.cshtml", resultadoImportacion);
                var pdfBytes = _reportePdfService.GenerarReporte(html);

                var usuario = User?.Identity?.Name ?? "Desconocido";

                var csvLog = new CsvLogDTO
                {
                    Archivo = pdfBytes,
                    NombreArchivo = $"CSV_log_{DateTime.Now:yyyy-MM-dd_HH:mm}.pdf",
                    FechaImportacion = DateTime.Now,
                    Exitos = productosValidos.Count,
                    Advertencias = resultadoImportacion.ProductosImportados.Count(r => r.Advertencias.Any()),
                    Errores = resultadoImportacion.ProductosImportados.Count(r => r.Errores.Any()),
                    Usuario = usuario,
                    Observaciones = ""
                };

                await _csvLogService.AgregarAsync(csvLog);

                return Json(new
                {
                    exito = true,
                    mensaje = $"Se realizó la importación de productos.\n" +
                    $"Se importaron {productosValidos.Count} exitosamente, para advertencias y errores vea el log."
                });
            }
            catch (Exception ex)
            {
                return Json(new { exito = false, mensaje = $"Ocurrió un error al importar el archivo: {ex.Message}" });
            }
        }

        // Exportar productos como PDF
        [HttpGet]
        public async Task<IActionResult> ExportarPDF()
        {
            var productosDto = await _obtenerProductoService.ObtenerProductosResumen();
            var html = await _razorViewToStringRenderer.RenderViewToStringAsync(this, "~/Views/Productos/Templates/PdfTemplate.cshtml", productosDto);
            var pdfBytes = _reportePdfService.GenerarReporte(html);

            Response.Headers["Content-Disposition"] = "inline; filename=productos.pdf";
            return File(pdfBytes, "application/pdf");
        }
    }
}
