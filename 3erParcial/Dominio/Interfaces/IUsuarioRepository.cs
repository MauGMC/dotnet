namespace Dominio.Interfaces
{
    public interface IUsuarioRepository : IRepositorioBase<Usuario>
    {
        //Acciones especificas
        Task CambiarEstadoAsync(string idUsuario, bool estado);
        //Acciones masivas
        Task EliminarUsuariosAsync(IEnumerable<string> idUsuarios);
        Task<IEnumerable<Usuario>> ObtenerUsuariosFiltradoGeneralAsync(
            int ordenarPor=1,
            bool ordenAscendente=true,
            int pagina=1,
            int tamanoPagina = 10,
            string? nombre=null,
            string? email=null,
            DateTime? fechaDesde = null,
            DateTime? fechaHasta = null,
            bool? activo = null
        );

    }
}
