
namespace Infraestructura.Repositorios
{
    public class PermisoRepository : IPermisoRepository
    {
        private readonly ApplicationDbContext _context;
        public PermisoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarAsync(Permiso entidad)
        {
            _context.Permisos.Update(entidad);
            await GuardarCambiosAsync();
        }

        public async Task AgregarAsync(Permiso entidad)
        {
            _context.Permisos.Add(entidad);
            await GuardarCambiosAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await ObtenerPorIdAsync(id);
            _context.Permisos.Remove(entidad);
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
                throw new Exception("Hubo un problema al guardar los cambios del permiso",ex);
            }
        }

        public async Task<Permiso> ObtenerPorIdAsync(int id)
        {
            return await _context.Permisos.FindAsync(id);
        }

        public async Task<IEnumerable<Permiso>> ObtenerTodosAsync()
        {
            return await _context.Permisos.ToListAsync();
        }
    }
}
