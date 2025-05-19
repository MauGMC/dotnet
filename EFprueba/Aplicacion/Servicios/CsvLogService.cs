using Aplicacion.DTOs;
using Aplicacion.Servicios.Interfaces;
using AutoMapper;
using Dominio.Entidades;
using Dominio.Interfaces;

namespace Aplicacion.Servicios
{
    public class CsvLogService : ICsvLogService
    {
        private readonly ICsvLogRepository _csvLogRepository;
        private readonly IMapper _mapper;
        public CsvLogService(ICsvLogRepository csvLogRepository, IMapper mapper)
        {
            _csvLogRepository = csvLogRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CsvLogDTO>> FiltrarAsync(string? usuario, string? filtroFecha)
        {
            var logs = await _csvLogRepository.ObtenerTodosAsync();
            var ahora = DateTime.Now;

            // Filtro por usuario (si está presente)
            if (!string.IsNullOrWhiteSpace(usuario))
            {
                logs = logs.Where(log => log.Usuario.Equals(usuario, StringComparison.OrdinalIgnoreCase));
            }

            // Filtro por fecha (si está presente)
            if (!string.IsNullOrWhiteSpace(filtroFecha))
            {
                logs = filtroFecha switch
                {
                    "asc" => logs.OrderBy(log => log.FechaImportacion),
                    "desc" => logs.OrderByDescending(log => log.FechaImportacion),
                    "hoy" => logs.Where(log => log.FechaImportacion.Date == ahora.Date),
                    "ayer" => logs.Where(log => log.FechaImportacion.Date == ahora.AddDays(-1).Date),
                    "mes" => logs.Where(log => log.FechaImportacion.Month == ahora.Month && log.FechaImportacion.Year == ahora.Year),
                    "año" => logs.Where(log => log.FechaImportacion.Year == ahora.Year),
                    _ => logs
                };
            }

            return _mapper.Map<IEnumerable<CsvLogDTO>>(logs);
        }
        public async Task<CsvLogDTO?> ObtenerPorIdAsync(int id)
        {
            var resultado = await _csvLogRepository.ObtenerPorIdAsync(id);
            return resultado == null ? null : _mapper.Map<CsvLogDTO>(resultado);
        }

        public async Task<IEnumerable<CsvLogDTO>> ObtenerTodosAsync()
        {
            var resultados = await _csvLogRepository.ObtenerTodosAsync();
            return _mapper.Map<IEnumerable<CsvLogDTO>>(resultados);
        }
        public async Task<(List<CsvLogDTO> Logs, int TotalRegistros)> ObtenerLogsPaginados(int pagina, int tamañoPagina)
        {
            var (logs, total) = await _csvLogRepository.ObtenerLogsPaginados(pagina, tamañoPagina);
            return (_mapper.Map<List<CsvLogDTO>>(logs), total);
        }

        public async Task AgregarAsync(CsvLogDTO csvLogDto)
        {
            if (csvLogDto == null)
                throw new ArgumentNullException(nameof(csvLogDto));

            var entidad = _mapper.Map<CsvLog>(csvLogDto);
            await _csvLogRepository.AgregarAsync(entidad);
        }

    }
}
