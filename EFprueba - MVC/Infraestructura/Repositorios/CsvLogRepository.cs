using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Repositorios
{
    public class CsvLogRepository : ICsvLogRepository
    {
        private readonly AppDbContext _context;
        public CsvLogRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AgregarAsync(CsvLog csvLog)
        {
            _context.CsvLogs.Add(csvLog);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var csvlog = await _context.CsvLogs.FindAsync(id);
            if (csvlog != null)
            {
                _context.CsvLogs.Remove(csvlog);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CsvLog?> ObtenerPorIdAsync(int id)
        {
            return await _context.CsvLogs.FindAsync(id);
        }

        public async Task<IEnumerable<CsvLog>> ObtenerTodosAsync()
        {
            return await _context.CsvLogs
                .OrderByDescending(l=>l.FechaImportacion)
                .ToListAsync();
        }

        public async Task<IEnumerable<CsvLog>> Filtrar(string? usuario, string? filtroFecha)
        {
            var query = _context.CsvLogs.AsQueryable();

            // Filtrar por usuario (si se provee)
            if (!string.IsNullOrWhiteSpace(usuario))
            {
                query = query.Where(l => l.Usuario != null && l.Usuario.ToLower() == usuario.ToLower());
            }

            // Filtro de fecha
            switch (filtroFecha?.ToLower())
            {
                case "asc":
                    query = query.OrderBy(l => l.FechaImportacion);
                    break;
                case "desc":
                    query = query.OrderByDescending(l => l.FechaImportacion);
                    break;
                case "mes":
                    var hoy = DateTime.Now;
                    query = query.Where(l =>
                        l.FechaImportacion.Month == hoy.Month &&
                        l.FechaImportacion.Year == hoy.Year)
                        .OrderByDescending(l => l.FechaImportacion);
                    break;
                case "año":
                case "anio":
                case "year":
                    var año = DateTime.Now.Year;
                    query = query.Where(l => l.FechaImportacion.Year == año)
                        .OrderByDescending(l => l.FechaImportacion);
                    break;
                default:
                    query = query.OrderByDescending(l => l.FechaImportacion);
                    break;
            }

            return await query.ToListAsync();
        }
        public async Task<(List<CsvLog> Logs, int TotalRegistros)> ObtenerLogsPaginados(int pagina, int tamañoPagina)
        {
            var query = _context.CsvLogs.AsQueryable();

            // Total antes de paginar
            var totalRegistros = await query.CountAsync();

            // Paginación
            var logs = await query
                .OrderByDescending(l => l.FechaImportacion)
                .Skip((pagina - 1) * tamañoPagina)
                .Take(tamañoPagina)
                .ToListAsync();

            return (logs, totalRegistros);
        }

    }
}
