
namespace Infraestructura.Repositorios
{
    public class PermisoDeRolRepository : IPermisoDeRolRepository
    {
        private readonly ApplicationDbContext _context;
        public PermisoDeRolRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarAsync(PermisoDeRol entidad)
        {
            _context.PermisosRoles.Update(entidad);
            await GuardarCambiosAsync();
        }

        public async Task AgregarAsync(PermisoDeRol entidad)
        {
            _context.PermisosRoles.Add(entidad);
            await GuardarCambiosAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await ObtenerPorIdAsync(id);
            _context.PermisosRoles.Remove(entidad);
            await GuardarCambiosAsync();
        }

        public async Task GuardarCambiosAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException ex)
            {
                throw new Exception("Hubo un error al guardar los cambios", ex);
            }
        }

        public async Task<PermisoDeRol> ObtenerPorIdAsync(int id)
        {
            return await _context.PermisosRoles.FindAsync(id);
        }

        public async Task<IEnumerable<PermisoDeRol>> ObtenerTodosAsync()
        {
            return await _context.PermisosRoles.ToListAsync();
        }
    }
}
