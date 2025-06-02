namespace Dominio.Entidades
{
    public class Producto
    {
        //PK
        public int ProductoID { get; set; }
        //Atributos
        public string Nombre { get; set; } = string.Empty;
        public int Categoria { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public DateTime FechaCreacion { get; set; }
        //Propiedades de navegación EF
        public ICollection<DetalleCompra> DetallesCompras { get; set; } = new HashSet<DetalleCompra>();
        public ICollection<DetallePedido> DetallesPedidos { get; set; } = new HashSet<DetallePedido>();
        public ICollection<DetalleVenta> DetallesVentas { get; set; } = new HashSet<DetalleVenta>();
        public ICollection<ProductoEnCarrito> ProductosEnCarrito { get; set; } = new HashSet<ProductoEnCarrito>();
        public Inventario? Inventario { get; set; }
        //Constructores
        public Producto() { }
        public Producto(int productoId, string nombre, int categoria, decimal precio, int stock,
            string? descripcion = null)
        {
            if (productoId < 0)
                throw new ArgumentOutOfRangeException("El ID del producto no puede ser negativo.");
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del producto no puede estar vacío.", nameof(nombre));
            if (categoria < 0)
                throw new ArgumentOutOfRangeException("La categoría del producto no puede ser negativa.");
            if (precio < 0)
                throw new ArgumentOutOfRangeException("El precio del producto no puede ser negativo.");
            if (stock < 0)
                throw new ArgumentOutOfRangeException("El stock del producto no puede ser negativo.");
            ProductoID = productoId;
            Nombre = nombre;
            Categoria = categoria;
            Descripcion = descripcion;
            Precio = precio;
            Stock = stock;
            FechaCreacion = DateTime.Now; // Por defecto, la fecha de creación es hoy
        }
    }
}
