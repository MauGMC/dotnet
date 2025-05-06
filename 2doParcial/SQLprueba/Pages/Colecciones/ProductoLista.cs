using Entidades;

namespace Colecciones
{
    public class ProductoLista
    {
        public List<Producto>? Productos { get; set; }
        private readonly ILogger<ProductoLista> _logger;

        public ProductoLista(ILogger<ProductoLista> logger)
        {
            _logger = logger;
        }

    }
}
