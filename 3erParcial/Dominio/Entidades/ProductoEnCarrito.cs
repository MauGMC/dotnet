namespace Dominio.Entidades
{
    public class ProductoEnCarrito
    {
        //PK
        public int ProductoEnCarritoID { get; set; }
        //FKs
        public int CarritoID { get; set; }
        public int ProductoID { get; set; }
        //Atributos
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
        //Propiedades de navegación EF
        public Carrito Carrito { get; set; } = new Carrito();
        public Producto Producto { get; set; } = new Producto();
        //Constructores
        public ProductoEnCarrito() { }
        public ProductoEnCarrito(int productoEnCarritoID, int carritoID, int productoID, int cantidad,
                                 decimal precioUnitario)
        {
            if (productoEnCarritoID < 0)
                throw new ArgumentOutOfRangeException("El ID del producto en el carrito no puede ser negativo.", nameof(productoEnCarritoID));
            if (carritoID < 0)
                throw new ArgumentOutOfRangeException("El ID del carrito no puede ser negativo.", nameof(carritoID));
            if (productoID < 0)
                throw new ArgumentOutOfRangeException("El ID del producto no puede ser negativo.", nameof(productoID));
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor que cero.", nameof(cantidad));
            if (precioUnitario <= 0)
                throw new ArgumentException("El precio unitario debe ser mayor que cero.", nameof(precioUnitario));
            ProductoEnCarritoID = productoEnCarritoID;
            CarritoID = carritoID;
            ProductoID = productoID;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
            Subtotal = cantidad * precioUnitario;
        }
    }
}
