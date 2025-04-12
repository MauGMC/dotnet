using Entidades;

namespace Colecciones
{
    public class ProductoLista
    {
        public List<Producto>? Productos { get; set; }
        private readonly ILogger<ProductoLista> _logger;

        // Constructor que toma un ILogger como parámetro
        public ProductoLista(ILogger<ProductoLista> logger)
        {
            _logger = logger;
        }

        // Puedes agregar métodos adicionales según tus necesidades
    }
}
