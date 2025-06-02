
namespace Infraestructura.Repositorios
{
    public class ImagenRepository : IImagenRepository
    {
        private readonly ApplicationDbContext _context;
        public ImagenRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task ActualizarAsync(Imagen entidad)
        {
            _context.Imagenes.Update(entidad);
            await GuardarCambiosAsync();
        }

        public async Task AgregarAsync(Imagen entidad)
        {
            _context.Imagenes.Add(entidad);
            await GuardarCambiosAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad=ObtenerPorIdAsync(id);
            _context.Remove(entidad);
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
                throw new Exception("Error al guardar los cambios en la imagen.", ex);
            }
        }
        public async Task<IEnumerable<Imagen>> ObtenerImagenesPorTablaEIdAsync(string tablaOrigen, string idRegistro)
        {
            return await _context.Imagenes
                .Where(i => i.TablaOrigen == tablaOrigen && i.IDOrigen == idRegistro)
                .ToListAsync();
        }

        public async Task<Imagen> ObtenerPorIdAsync(int id)
        {
            return await _context.Imagenes.FindAsync(id)
                   ?? throw new KeyNotFoundException($"Imagen with ID {id} not found.");
        }

        public async Task<IEnumerable<Imagen>> ObtenerTodosAsync()
        {
            return await _context.Imagenes.ToListAsync();
        }
    }
}
