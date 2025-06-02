
using Aplicacion.Enums;
using Aplicacion.Extensions;
using Dominio.Entidades;

namespace Infraestructura.Repositorios
{
    public class ClienteRepository : IClienteRepository
    {
        public ApplicationDbContext _context;
        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task ActualizarAsync(Cliente entidad)
        {
            _context.Clientes.Update(entidad);
            await GuardarCambiosAsync();
        }

        public async Task AgregarAsync(Cliente entidad)
        {
            _context.Clientes.Add(entidad);
            await GuardarCambiosAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad= await ObtenerPorIdAsync(id);
            _context.Clientes.Remove(entidad);
            await GuardarCambiosAsync();
        }

        public Task GuardarCambiosAsync()
        {
            try
            {
                return _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error al guardar los cambios en el cliente.", ex);
            }
        }

        public async Task<IEnumerable<Cliente>> ObtenerClientesFiltradoGeneralAsync(int ordenarPor = 1, bool ordenAscendente = true, int pagina = 1, int tamanoPagina = 10, string? nombreCompleto = null, string? telefono = null, DateTime? fechaNacimientoDesde = null, DateTime? fechaNacimientoHasta = null, char? sexo = null)
        {
            var query = _context.Clientes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nombreCompleto))
                query = query.Where(c => (c.Nombres + " " + c.ApellidoPaterno + " " + c.ApellidoMaterno).Contains(nombreCompleto));
            if (!string.IsNullOrWhiteSpace(telefono))
                query = query.Where(c => c.Telefono.Contains(telefono));
            if (fechaNacimientoDesde.HasValue)
                query = query.Where(c => c.FechaNacimiento >= fechaNacimientoDesde.Value);
            if (fechaNacimientoHasta.HasValue)
                query = query.Where(c => c.FechaNacimiento <= fechaNacimientoHasta.Value);
            if (sexo is 'M' or 'F' or 'N')
                query = query.Where(c => c.Sexo == sexo);

            OrdenarClientePor orden;
            if (!Enum.IsDefined(typeof(OrdenarClientePor), ordenarPor))
                orden = OrdenarClientePor.ClienteID;
            else
                orden = (OrdenarClientePor)ordenarPor;

            query = query.OrdenarPor(orden, ordenAscendente);

            query = query.Skip((pagina - 1) * tamanoPagina).Take(tamanoPagina);

            return await query.ToListAsync();

        }

        public async Task<Cliente> ObtenerPorIdAsync(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<IEnumerable<Cliente>> ObtenerTodosAsync()
        {
            return await _context.Clientes.ToListAsync();
        }
    }
}
