
namespace Infraestructura.Repositorios
{
    public class RolRepository : IRolRepository
    {
        private readonly ApplicationDbContext _context;
        public RolRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarAsync(Rol entidad)
        {
            _context.Roles.Update(entidad);
            await GuardarCambiosAsync();
        }

        public async Task AgregarAsync(Rol entidad)
        {
            _context.Roles.Add(entidad);
            await GuardarCambiosAsync();
        }

        public async Task CambiarEstadoDeRolAsync(int idRol, bool estado)
        {
            var entidad = await ObtenerPorIdAsync(idRol);
            entidad.Activo = estado;
            await GuardarCambiosAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await ObtenerPorIdAsync(id);
            _context.Roles.Remove(entidad);
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
                throw new Exception("Hubo un error al guardar los cambios del rol.", ex);
            }
        }

        public async Task<Rol> ObtenerPorIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<IEnumerable<Rol>> ObtenerRolesPorEstadoAsync(bool activo)
        {
            return _context.Roles
                .Where(r => r.Activo == activo);
        }

        public async Task<IEnumerable<Rol>> ObtenerRolesPorNombreAsync(string nombre)
        {
            return _context.Roles
                .Where(r => EF.Functions.Like(r.Nombre, $"%{nombre}%"));
        }

        public async Task<IEnumerable<Rol>> ObtenerTodosAsync()
        {
            return await _context.Roles.ToListAsync();
        }
    }
}
