
namespace Infraestructura.Repositorios
{
    public class InventarioRepository : IInventarioRepository
    {
        private readonly ApplicationDbContext _context;
        public InventarioRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task ActualizarAsync(Inventario entidad)
        {
            _context.Update(entidad);
            await GuardarCambiosAsync();
        }

        public async Task AgregarAsync(Inventario entidad)
        {
            _context.Add(entidad);
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
                throw new Exception("Error al guardar los cambios en el inventario.", ex);
            }
        }   
        public async Task<IEnumerable<Inventario>> ObtenerInventarioQueSeEstaAcabandoAsync(int minimoStock = 10)
        {
            return await _context.Inventarios
                .Where(i => i.Cantidad <= minimoStock)
                .ToListAsync();
        }

        public async Task<Inventario> ObtenerPorIdAsync(int id)
        {
            return await _context.Inventarios.FindAsync(id)
                   ?? throw new KeyNotFoundException($"Inventario with ID {id} not found.");
        }

        public async Task<IEnumerable<Inventario>> ObtenerTodosAsync()
        {
            return await _context.Inventarios.ToListAsync();
        }
    }
}
