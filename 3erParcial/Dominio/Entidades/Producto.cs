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
        public Inventario Inventario { get; set; } = new Inventario();
    }
}
