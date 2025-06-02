using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface ICsvLogRepository
    {
        Task AgregarAsync(CsvLog csvLog);
        Task<IEnumerable<CsvLog>> ObtenerTodosAsync();
        Task<CsvLog?> ObtenerPorIdAsync(int id);
        Task EliminarAsync(int id);
        Task<(List<CsvLog> Logs, int TotalRegistros)> ObtenerLogsPaginados(int pagina, int tamañoPagina);
    }
}
