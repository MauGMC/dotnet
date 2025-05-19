using Aplicacion.DTOs;
using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios.Interfaces
{
    public interface ICsvLogService
    {
        Task<IEnumerable<CsvLogDTO>> ObtenerTodosAsync();
        Task<CsvLogDTO?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<CsvLogDTO>> FiltrarAsync(string? usuario, string? criterioFiltro);
        Task<(List<CsvLogDTO> Logs, int TotalRegistros)> ObtenerLogsPaginados(int pagina, int tamañoPagina);
        Task AgregarAsync(CsvLogDTO csvLogDto);
    }
}
