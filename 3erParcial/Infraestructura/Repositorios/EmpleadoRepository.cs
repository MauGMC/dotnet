
using Aplicacion.Enums;
using Aplicacion.Extensions;

namespace Infraestructura.Repositorios
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly ApplicationDbContext _context;
        public EmpleadoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarAsync(Empleado entidad)
        {
            _context.Update(entidad);
            await GuardarCambiosAsync();
        }

        public async Task AgregarAsync(Empleado entidad)
        {
            _context.Add(entidad);
            await GuardarCambiosAsync();
        }

        public async Task DarDeBajaAEmpleados(IEnumerable<int> empleadosIds)
        {
            var empleados = await _context.Empleados
                .Where(e => empleadosIds.Contains(e.EmpleadoID))
                .ToListAsync();

            _context.Empleados.RemoveRange(empleados);
            await GuardarCambiosAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await ObtenerPorIdAsync(id);
            _context.Remove(entidad);
            await GuardarCambiosAsync();
        }

        public async Task GuardarCambiosAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error al guardar los cambios en el empleado.", ex);
            }
        }

        public async Task<IEnumerable<Empleado>> ObtenerEmpleadosFiltradoGeneralAsync(int ordenarPor=1, bool ordenAscendente=true,int pagina = 1, int tamanoPagina = 10, string? nombreCompleto = null, string? telefono = null, DateTime? fechaContratacionDesde = null, DateTime? fechaContratacionHasta = null, char? sexo = null, string? puesto = null, int? estado = null)
        {
            var query = _context.Empleados.AsQueryable();
            if (!string.IsNullOrWhiteSpace(nombreCompleto))
            {
                query = query.Where(e => (e.Nombres + " " + e.ApellidoPaterno + " " + e.ApellidoMaterno).Contains(nombreCompleto));
            }
            if (!string.IsNullOrWhiteSpace(telefono))
            {
                query = query.Where(e => e.Telefono.Contains(telefono));
            }
            if (fechaContratacionDesde.HasValue)
            {
                query = query.Where(e => e.FechaContratacion >= fechaContratacionDesde.Value);
            }
            if (fechaContratacionHasta.HasValue)
            {
                query = query.Where(e => e.FechaContratacion <= fechaContratacionHasta.Value);
            }
            if (sexo.HasValue && (sexo == 'M' || sexo == 'F'))
            {
                query = query.Where(e => e.Sexo == sexo);
            }
            if (!string.IsNullOrWhiteSpace(puesto))
            {
                query = query.Where(e => e.Puesto.Contains(puesto));
            }
            if (estado.HasValue)
            {
                query = query.Where(e => e.Estado == estado.Value);
            }

            OrdenarEmpleadoPor orden;
            if (!Enum.IsDefined(typeof(OrdenarEmpleadoPor), ordenarPor))
                orden = OrdenarEmpleadoPor.EmpleadoID;
            else
                orden = (OrdenarEmpleadoPor)ordenarPor;

            query = query.OrdenarPor(orden, ordenAscendente);

                query = query
                    .Skip((pagina - 1) * tamanoPagina)
                    .Take(tamanoPagina);

            return await query.ToListAsync();

        }

        public async Task<Empleado> ObtenerPorIdAsync(int id)
        {
            return await _context.Empleados.FindAsync(id);
        }

        public async Task<IEnumerable<Empleado>> ObtenerTodosAsync()
        {
            return await _context.Empleados.ToListAsync();
        }
    }
}
