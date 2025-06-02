
namespace Infraestructura.Repositorios
{
    public class RolDeUsuarioRepository : IRolDeUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        public RolDeUsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarAsync(RolDeUsuario entidad)
        {
            _context.RolesUsuarios.Update(entidad);
            await GuardarCambiosAsync();
        }

        public async Task AgregarAsync(RolDeUsuario entidad)
        {
            _context.RolesUsuarios.Add(entidad);
            await GuardarCambiosAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await ObtenerPorIdAsync(id);
            _context.RolesUsuarios.Remove(entidad);
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
                throw new Exception("Hubo un error al guardar los cambios de los roles", ex);
            }
        }

        public async Task<RolDeUsuario> ObtenerPorIdAsync(int id)
        {
            return await _context.RolesUsuarios.FindAsync(id);
        }

        public async Task<IEnumerable<RolDeUsuario>> ObtenerRolesDeUsuario(string usuarioId)
        {
            return _context.RolesUsuarios.Where(ru=>ru.UsuarioID == usuarioId);
        }

        public async Task<IEnumerable<RolDeUsuario>> ObtenerTodosAsync()
        {
            return await _context.RolesUsuarios.ToListAsync();
        }
    }
}
