
using Aplicacion.Enums;
using Aplicacion.Extensions;
using System.Reflection.Metadata.Ecma335;

namespace Infraestructura.Repositorios
{
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly ApplicationDbContext _context;
        public ProveedorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarAsync(Proveedor entidad)
        {
            _context.Proveedores.Update(entidad);
            await GuardarCambiosAsync();
        }

        public async Task AgregarAsync(Proveedor entidad)
        {
            _context.Proveedores.Add(entidad);
            await GuardarCambiosAsync();
        }

        public async Task CambiarEstadoDeProveedorAsync(int idProveedor, bool estado)
        {
            var entidad = await ObtenerPorIdAsync(idProveedor);
            entidad.Activo = estado;
            await GuardarCambiosAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await ObtenerPorIdAsync(id);
            _context.Proveedores.Remove(entidad);
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
                throw new Exception("No se pudieron guardar los cambios del proveedor.", ex);
            }
        }

        public async Task<Proveedor> ObtenerPorIdAsync(int id)
        {
            return await _context.Proveedores.FindAsync(id);
        }

        public async Task<IEnumerable<Proveedor>> ObtenerProveedoresFiltradoGeneralAsync(int ordenarPor = 1, bool ordenAscendente = true, int pagina = 1, int tamanoPagina = 10, string? nombre = null, string? telefono = null, string? email = null, bool? estado = null)
        {
            var query = _context.Proveedores.AsQueryable();

            if (!string.IsNullOrEmpty(nombre))
                query = query.Where(p => EF.Functions.Like(p.Nombre, $"%{nombre}%"));
            if (!string.IsNullOrEmpty(telefono))
                query = query.Where(p => p.Telefono == telefono);
            if (!string.IsNullOrEmpty(email))
                query = query.Where(p => p.Email == email);
            if (estado.HasValue)
                query = query.Where(p => p.Activo == estado);

            OrdenarProveedoresPor orden;
            if (!Enum.IsDefined(typeof(OrdenarProveedoresPor), ordenarPor))
                orden = OrdenarProveedoresPor.ProveedorID;
            else
                orden = (OrdenarProveedoresPor)ordenarPor;

            query = query.OrdenarPor(orden, ordenAscendente);

                query = query
                    .Skip((pagina - 1) * tamanoPagina)
                    .Take(tamanoPagina);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Proveedor>> ObtenerTodosAsync()
        {
            return await _context.Proveedores.ToListAsync();
        }
    }
}
