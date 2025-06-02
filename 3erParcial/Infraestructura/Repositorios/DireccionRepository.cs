
namespace Infraestructura.Repositorios
{
    public class DireccionRepository : IDireccionRepository
    {
        private readonly ApplicationDbContext _context;
        public DireccionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarAsync(Direccion entidad)
        {
            _context.Direcciones.Update(entidad);
            await GuardarCambiosAsync();
        }

        public async Task AgregarAsync(Direccion entidad)
        {
            _context.Direcciones.Add(entidad);
            await GuardarCambiosAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await ObtenerPorIdAsync(id);
            _context.Direcciones.Remove(entidad);
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
                throw new Exception("Hubo un problema al agregar los cambios.", ex);
            }
        }

        public async Task<Direccion> ObtenerPorIdAsync(int id)
        {
            return await _context.Direcciones.FindAsync(id);
        }

        public async Task<IEnumerable<Direccion>> ObtenerTodosAsync()
        {
            return await _context.Direcciones.ToListAsync();
        }
    }
}
