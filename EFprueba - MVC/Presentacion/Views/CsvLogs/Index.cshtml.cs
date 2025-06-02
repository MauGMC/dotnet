using Aplicacion.Enums;
using Aplicacion.Servicios;
using Aplicacion.Servicios.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Presentacion.ViewModels;

namespace Presentacion.Pages.CsvLogs
{
    public class IndexModel : PageModel
    {
        //Paginaci�n
        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }
        public int Tama�oPagina { get; set; } = 10;
        public int PaginaAtras => Math.Max(PaginaActual - 2, 1);
        public int PaginaAdelante => Math.Min(PaginaActual + 2, TotalPaginas);

        //Servicios
        private readonly ICsvLogService _csvLogService;
        private readonly IMapper _mapper;

        //Vistas
        public CsvLogViewModel CsvLogViewModel { get; set; }

        //Datos
        public List<CsvLogViewModel> CsvLogs { get; set; } = new List<CsvLogViewModel>();
        
        //Constructor
        public IndexModel(IMapper mapper, ICsvLogService logService)
        {
            _mapper = mapper;
            _csvLogService = logService;
        }

        public async Task OnGet(int pagina = 1, int tama�oPagina = 10)
        {
            PaginaActual = pagina;
            Tama�oPagina = tama�oPagina;
            var (dtos, totalRegistros) = await _csvLogService.ObtenerLogsPaginados(pagina, tama�oPagina);
            TotalPaginas = (int)Math.Ceiling((double)totalRegistros / tama�oPagina);
            CsvLogs = _mapper.Map<List<CsvLogViewModel>>(dtos);
        }
        public async Task<IActionResult> OnGetDescargarArchivoAsync(int id)
        {
            var log = await _csvLogService.ObtenerPorIdAsync(id);

            if (log == null || log.Archivo == null)
                return NotFound();

            return File(log.Archivo, "text/csv", log.NombreArchivo + ".pdf");
        }


    }
}
