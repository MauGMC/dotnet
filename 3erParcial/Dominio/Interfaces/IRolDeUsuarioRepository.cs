namespace Dominio.Interfaces
{
    public interface IRolDeUsuarioRepository : IRepositorioBase<RolDeUsuario>
    {
        Task<IEnumerable<RolDeUsuario>> ObtenerRolesDeUsuario(string usuarioId);

    }
}
