
using Aplicacion.Enums;
using Aplicacion.Extensions;

namespace Infraestructura.Repositorios
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarAsync(Producto entidad)
        {
            _context.Productos.Update(entidad);
            await GuardarCambiosAsync();
        }

        public async Task AgregarAsync(Producto entidad)
        {
            _context.Productos.Add(entidad);
            await GuardarCambiosAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await ObtenerPorIdAsync(id);
            _context.Productos.Remove(entidad);
            await GuardarCambiosAsync();
        }

        public async Task EliminarProductosAsync(IEnumerable<int> productosIds)
        {
            var productos = await _context.Productos
                .Where(p=>productosIds.Contains(p.ProductoID))
                .ToListAsync();
            _context.Productos.RemoveRange(productos);
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
                throw new Exception("Hubo un error al guardar los cambios del producto.", ex);
            }
        }

        public async Task<Producto> ObtenerPorIdAsync(int id)
        {
            return await _context.Productos.FindAsync(id);
        }

        public async Task<IEnumerable<Producto>> ObtenerProductosFiltradoGeneralAsync(int ordenarPor=1, bool ordenAscendente=true,int pagina = 1, int tamanoPagina = 10, string? nombre = null, int? categoriaId = null, decimal? precioMinimo = null, decimal? precioMaximo = null, DateTime? fechaRegistroDesde = null, DateTime? fechaRegistroHasta = null)
        {
            var query = _context.Productos
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(nombre))
                query = query.Where(p => EF.Functions.Like(p.Nombre, $"%{nombre}%"));
            if(categoriaId.HasValue)
                query = query.Where(p=>p.Categoria==categoriaId);
            if(precioMinimo.HasValue)
                query = query.Where(p=>p.Precio>=precioMinimo);
            if(precioMaximo.HasValue)
                query = query.Where(p=>p.Precio<=precioMaximo);
            if(fechaRegistroDesde.HasValue)
                query = query.Where(p=>p.FechaCreacion>=fechaRegistroDesde);
            if(fechaRegistroHasta.HasValue)
                query = query.Where(p=>p.FechaCreacion<=fechaRegistroHasta);

            OrdenarProductosPor orden;
            if (!Enum.IsDefined(typeof(OrdenarProductosPor), ordenarPor))
                orden = OrdenarProductosPor.ProductoID;
            else
                orden = (OrdenarProductosPor)ordenarPor;

            query = query.OrdenarPor(orden, ordenAscendente);

                query = query
                    .Skip((pagina - 1) * tamanoPagina)
                    .Take(tamanoPagina);

            return await query.ToListAsync();

        }

        public async Task<IEnumerable<Producto>> ObtenerTodosAsync()
        {
            return await _context.Productos.ToListAsync();
        }
    }
}
