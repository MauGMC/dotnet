using Microsoft.AspNetCore.Mvc.RazorPages;
using Aplicacion.DTOs;
using AutoMapper;
using Presentacion.ViewModels;
using Aplicacion.Servicios;
using Aplicacion.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Presentacion.Pages.Productos.Modales;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Presentacion.Servicios;
using Presentacion.Helpers;
using System.Linq;
using Aplicacion.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc.Infrastructure;
namespace Presentacion.Pages.Productos
{
    public class ListaModel : PageModel
    {
        private readonly ObtenerProductoService _obtenerProductoService;
        private readonly ProductoService _productoService;
        private readonly ImportarCsvService _importarCsvService;
        private readonly ReportePdfService _reportePdfService;
        private readonly RazorViewToStringRenderer _razorViewToStringRenderer;
        private readonly ICsvLogService _csvLogService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public int PaginaActual { get; set; } 
        public int TotalPaginas { get; set; }
        public int TamañoPagina { get; set; } = 10;
        public int PaginaAtras => Math.Max(PaginaActual - 2, 1);
        public int PaginaAdelante => Math.Min(PaginaActual + 2, TotalPaginas);
        [BindNever]
        public ProductoResumenViewModel ProductoResumenViewModel { get; set; }
        [BindProperty]
        public ProductoDetalleViewModel ProductoDetalleViewModel { get; set; }
        [BindProperty]
        public CsvImportViewModel CsvImportViewModel { get; set; } = new CsvImportViewModel();
        [BindNever]
        public IEnumerable<ProductoResumenDTO> Productos { get; set; }

        //Traducción Categorías - Enum
        public List<SelectListItem> ObtenerCategorias()
        {
            return Enum.GetValues(typeof(Categorias))
                .Cast<Categorias>()
                .Select(c => new SelectListItem
                {
                    Value = ((int)c).ToString(),
                    Text = c.ToString()
                }).ToList();
        }

        // Constructor
        public ListaModel(
            ObtenerProductoService obtenerProductoService, 
            ProductoService productoService, 
            ImportarCsvService importarCsvService,
            ICsvLogService csvLogService,
            ReportePdfService reportePdfService,
            RazorViewToStringRenderer razorViewToStringRenderer,
            IWebHostEnvironment webHostEnviroment,
            IMapper mapper
            )
        {
            _obtenerProductoService = obtenerProductoService;
            _productoService = productoService;
            _importarCsvService = importarCsvService;
            _csvLogService = csvLogService;
            _reportePdfService = reportePdfService;
            _razorViewToStringRenderer = razorViewToStringRenderer;
            _mapper = mapper;
            _webHostEnvironment = webHostEnviroment;
            ProductoResumenViewModel = new ProductoResumenViewModel();
        }
        // Método OnGet para cargar la página inicial
        public async Task OnGet(int pagina = 1, int tamañoPagina = 10)
        {
            PaginaActual = pagina;
            TamañoPagina = tamañoPagina;
            var(productosDto, totalRegistros) = await _obtenerProductoService.ObtenerProductosResumenPaginado(pagina, tamañoPagina);

            decimal precioTotal = productosDto.Sum(p => p.Precio * p.Cantidad);

            ProductoResumenViewModel = new ProductoResumenViewModel
            {
                Productos = productosDto,
                PrecioTotal = precioTotal
            };
            // Asignar valores para la paginación
            TotalPaginas = (int)Math.Ceiling((double)totalRegistros / tamañoPagina);
        }
        // Método para cargar el modal de producto con los datos del producto
        public async Task<IActionResult> OnGetProductoFormAsync(int productoId = 0, string accion = "agregar")
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

            return Partial("Productos/Modales/_ProductoForm", viewModel);
        }

        public IActionResult OnGetImportarCsvForm()
        {
            var viewModel = new CsvImportViewModel();
            return Partial("Productos/Modales/_CsvForm", viewModel);
        }

        // Método para manejar la acción del modal (Detalles, Agregar, Editar, Eliminar)
        public async Task<IActionResult> OnPost(
            [FromForm(Name = "Producto.Accion")] string accion,
            [Bind(Prefix = "Producto")] ProductoDetalleViewModel producto
            )
        {
            ModelState.Clear();
            //Validar la existencia de la imagen
            if (producto.Imagen != null && producto.Imagen.Length > 0)
            {
                //Obtener el nombre del archivo y la ruta
                string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(producto.Imagen.FileName);
                string rutaCarpeta = Path.Combine(_webHostEnvironment.WebRootPath, "imagenes");
                //Crear la carpeta si no existe
                if (!Directory.Exists(rutaCarpeta))
                    Directory.CreateDirectory(rutaCarpeta);
                //Darle ruta a la imagen
                string rutaArchivo = Path.Combine(rutaCarpeta, nombreArchivo);
                //Guardar la imagen en el servidor
                using (var stream = new FileStream(rutaArchivo, FileMode.Create))
                {
                    await producto.Imagen.CopyToAsync(stream);
                }
                //Asignar la ruta de la imagen al producto
                producto.RutaImagen = "imagenes/" + nombreArchivo;
            }
            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                Console.WriteLine("Errores del modelo:");
                errores.ForEach(e => Console.WriteLine("- " + e));
                return new JsonResult(new
                {
                    exito = false,
                    mensaje = "El producto tiene errores."
                });
            }

            //Mapeado de DTO a Entidad
            var productoMapeado = _mapper.Map<Producto>(producto);
            //Elección de acción
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
                    return await HandleValidationFailure(new ProductoFormViewModel { Producto = producto });
            }

            return RedirectToPage();
        }

        // Método para manejar errores de validación (devuelve el modal con errores)
        private async Task<IActionResult> HandleValidationFailure(ProductoFormViewModel model)
        {
            model.CategoriasLista = ObtenerCategorias();
            return Partial("Productos/Modales/_ProductoForm", model);
        }
        
        // Método para importar productos desde un archivo CSV
        public async Task<IActionResult> OnPostImportarCsv(CsvImportViewModel model)
        {
            ModelState.Clear();
            var archivoCsv = model.ArchivoCsv;
            Console.WriteLine("Se entró al POST");
            // Verificar si el archivo CSV es nulo o vacío
            if (archivoCsv == null || archivoCsv.Length == 0)
            {
                return new JsonResult(new
                {
                    exito = false,
                    mensaje = "El archivo CSV está vacío o no se seleccionó."
                });
            }
            else if(Path.GetExtension(archivoCsv.FileName).ToLower() != ".csv")
            {
                return new JsonResult(new
                {
                    exito = false,
                    mensaje = "El archivo no tiene extensión CSV."
                });
            }
            if(!ModelState.IsValid)
            {
                var errores = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                Console.WriteLine("Errores del modelo:");
                errores.ForEach(e => Console.WriteLine("- " + e));

                return new JsonResult(new
                {

                    exito = false,
                    mensaje = "El archivo CSV no es válido o no se seleccionó ningún archivo.",
                    detalles = errores
                });
            }
            try
            {
                // Verificar la extensión del archivo
                using var stream = archivoCsv.OpenReadStream();
                var resultadoImportacion = await _importarCsvService.LeerCsv(stream);
                
                // Separar los productos válidos para guardar
                var productosValidos = resultadoImportacion.ProductosImportados
                    .Where(r => r.Producto != null && !r.Errores.Any())
                    .Select(r => r.Producto)
                    .ToList();
                
                // Verificar si se encontraron productos en el archivo CSV
                if (productosValidos.Any())
                {
                    await _importarCsvService.GuardarProductos(productosValidos);
                }
                else
                {
                    return new JsonResult(new
                    {
                        exito = false,
                        mensaje = "No se encontraron productos válidos"
                    });
                }

                //Crear PDF
                var html = await _razorViewToStringRenderer.RenderViewToStringAsync(this, "/Pages/Productos/Templates/Csv_log.cshtml", resultadoImportacion);
                var pdfBytes = _reportePdfService.GenerarReporte(html);
                var usuario = User?.Identity?.Name ?? "Desconocido"; // o el usuario que tengas
                var csvLog = new CsvLogDTO
                {
                    Archivo=pdfBytes,
                    NombreArchivo =  $"CSV_log_{DateTime.Now:yyyy-MM-dd_HH:mm}.pdf",
                    FechaImportacion = DateTime.Now,
                    Exitos = productosValidos.Count,
                    Advertencias = resultadoImportacion.ProductosImportados.Count(r => r.Advertencias.Any()),
                    Errores = resultadoImportacion.ProductosImportados.Count(r => r.Errores.Any()),
                    Usuario = usuario,
                    Observaciones = "" // si quieres agregar algo más
                };
                
                await _csvLogService.AgregarAsync(csvLog);
                return new JsonResult(new
                {
                    exito = true,
                    mensaje = $"Se realizó la importación de productos.\n" +
                    $"Se importaron {productosValidos.Count} exitosamente, para advertencias y errores vea el log con nombre de archivo 'CSV_log_{DateTime.Now:yyyy-MM-dd_HH:mm}.pdf."
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { exito = false, mensaje = $"Ocurrió un error al importar el archivo: {ex.Message}" });
            }
        }
        //Método para exportar como PDF
        public async Task<IActionResult> OnGetExportarPDF()
        {
            // Obtener la lista de productos
            var productosDto = await _obtenerProductoService.ObtenerProductosResumen();
            var html = await _razorViewToStringRenderer.RenderViewToStringAsync(this, "/Pages/Productos/Templates/PdfTemplate.cshtml", productosDto);
            
            var pdfBytes =  _reportePdfService.GenerarReporte(html);
            // Devolver el archivo PDF como respuesta
            Response.Headers["Content-Disposition"] = "inline; filename=productos.pdf";
            return File(pdfBytes, "application/pdf");

        }

    }

}
