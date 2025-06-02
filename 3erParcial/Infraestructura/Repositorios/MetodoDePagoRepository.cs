
namespace Infraestructura.Repositorios
{
    public class MetodoDePagoRepository : IMetodoDePagoRepository
    {
        private readonly ApplicationDbContext _context;
        public MetodoDePagoRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task ActualizarAsync(MetodoDePago entidad)
        {
            _context.Update(entidad);
            await GuardarCambiosAsync();
        }

        public async Task AgregarAsync(MetodoDePago entidad)
        {
            _context.MetodosDePago.Add(entidad);
            await GuardarCambiosAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await ObtenerPorIdAsync(id);
            _context.MetodosDePago.Remove(entidad);
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
                throw new Exception("Error al guardar los cambios en el método de pago.", ex);
            }
        }
        public async Task<MetodoDePago> ObtenerPorIdAsync(int id)
        {
            return await _context.MetodosDePago.FindAsync(id)
                   ?? throw new KeyNotFoundException($"Método de pago with ID {id} not found.");
        }

        public async Task<IEnumerable<MetodoDePago>> ObtenerTodosAsync()
        {
            return await _context.MetodosDePago.ToListAsync();
        }
    }
}
