
namespace Infraestructura.Repositorios
{
    public class CarritoRepository : ICarritoRepository
    {

        private readonly ApplicationDbContext _context;
        public CarritoRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Carrito>> ObtenerTodosAsync()
        {
            return await _context.Carritos.ToListAsync();
        }
        public async Task<Carrito> ObtenerPorIdAsync(int id)
        {
            return await _context.Carritos.FindAsync(id)
                   ?? throw new KeyNotFoundException($"Carrito with ID {id} not found.");
        }
        public async Task AgregarAsync(Carrito entidad)
        {
            _context.Carritos.Add(entidad);
            await GuardarCambiosAsync();
        }
        public async Task ActualizarAsync(Carrito entidad)
        {
            _context.Carritos.Update(entidad);
            await GuardarCambiosAsync();
        }
        public async Task EliminarAsync(int id)
        {
            var entidad = await ObtenerPorIdAsync(id);
            _context.Carritos.Remove(entidad);
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
                throw new Exception("Error al guardar los cambios en el carrito.", ex);
            }
        }
    }
}
