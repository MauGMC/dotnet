namespace Dominio.Interfaces
{
    public interface IImagenRepository : IRepositorioBase<Imagen>
    {
        Task<IEnumerable<Imagen>> ObtenerImagenesPorTablaEIdAsync(string tablaOrigen, string idRegistro);
    }
}
