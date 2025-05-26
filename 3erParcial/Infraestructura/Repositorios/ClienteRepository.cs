
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

        public async Task<IEnumerable<Cliente>> ObtenerClientePorNombreAsync(string cadena)
        {
            cadena = $"%{cadena.Trim()}%";
            return await _context.Clientes
                .Where(c => EF.Functions.Like(
                    c.Nombres + " " + c.ApellidoPaterno + " " + c.ApellidoMaterno,
                    cadena))
                .ToListAsync();
        }

        public async Task<IEnumerable<Cliente>> ObtenerClientesNacidosEntreAsync(DateTime desde, DateTime hasta)
        {
            return await _context.Clientes
                .Where(cliente => cliente.FechaNacimiento >= desde && cliente.FechaNacimiento <= hasta)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cliente>> ObtenerClientesPorSexoAsync(string sexo)
        {
            return await _context.Clientes
                .Where(cliente => cliente.Sexo.ToString() == sexo)
                .ToListAsync();
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
