
using Aplicacion.Enums;
using Aplicacion.Extensions;

namespace Infraestructura.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarAsync(Usuario entidad)
        {
            _context.Usuarios.Update(entidad);
            await GuardarCambiosAsync();
        }

        public async Task AgregarAsync(Usuario entidad)
        {
            _context.Usuarios.Add(entidad);
            await GuardarCambiosAsync();
        }

        public async Task CambiarEstadoAsync(string idUsuario, bool estado)
        {
            var entidad = await _context.Usuarios.FirstOrDefaultAsync(u => u.UsuarioID == idUsuario);
            entidad.Activo= estado;
            await GuardarCambiosAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await ObtenerPorIdAsync(id);
            _context.Usuarios.Remove(entidad);
            await GuardarCambiosAsync();
        }

        public async Task EliminarUsuariosAsync(IEnumerable<string> idUsuarios)
        {
            var entidades = await _context.Usuarios
                .Where(u => idUsuarios.Contains(u.UsuarioID))
                .ToListAsync();
            _context.Usuarios.RemoveRange(entidades);
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
                throw new Exception("Hubo un problema al guardar los cambios del usuario");
            }
        }

        public async Task<Usuario> ObtenerPorIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<IEnumerable<Usuario>> ObtenerTodosAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> ObtenerUsuariosFiltradoGeneralAsync(int ordenarPor=1, bool ordenAscendente=true,int pagina = 1, int tamanoPagina = 10, string? nombre = null, string? email = null, DateTime? fechaDesde = null, DateTime? fechaHasta = null, bool? activo = null)
        {
            var query = _context.Usuarios.AsQueryable();

            if (!string.IsNullOrEmpty(nombre))
                query = query.Where(u => EF.Functions.Like(u.NombreUsuario, $"%{nombre}%"));
            if(!string.IsNullOrEmpty(email))
                query = query.Where(u=>u.Email == email);
            if(fechaDesde.HasValue)
                query = query.Where(u=>u.FechaCreacion >= fechaDesde.Value);
            if(fechaHasta.HasValue)
                query = query.Where(u=>u.FechaCreacion<= fechaHasta.Value);
            if (activo.HasValue)
                query = query.Where(u => u.Activo == activo);

            OrdenarUsuariosPor orden;
            if (!Enum.IsDefined(typeof(OrdenarUsuariosPor), ordenarPor))
                orden = OrdenarUsuariosPor.UsuarioID;
            else
                orden = (OrdenarUsuariosPor) ordenarPor;

            query = query.OrdenarPor(orden, ordenAscendente);
            
            query = query.Skip((pagina-1)*tamanoPagina).Take(tamanoPagina);

            return await query.ToListAsync();
        }
    }
}
