namespace Dominio.Entidades
{
    public class DetalleCompra
    {
        //PK
        public int DetalleCompraID { get; set; }
        //FKs
        public int CompraID { get; set; }
        public int ProductoID { get; set; }
        //Atributos
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
        //Propiedades de navegación EF
        public Compra Compra { get; set; } = new Compra();
        public Producto Producto { get; set; } = new Producto();
        //Constructores
        public DetalleCompra() { }
        public DetalleCompra(int detalleCompraID, int compraID, int productoID, int cantidad,
                             decimal precioUnitario)
        {
            if (detalleCompraID < 0)
                throw new ArgumentOutOfRangeException("El ID del detalle de compra no puede ser negativo.", nameof(detalleCompraID));
            if (compraID < 0)
                throw new ArgumentOutOfRangeException("El ID de la compra no puede ser negativo.", nameof(compraID));
            if (productoID < 0)
                throw new ArgumentOutOfRangeException("El ID del producto no puede ser negativo.", nameof(productoID));
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor que cero.", nameof(cantidad));
            if (precioUnitario <= 0)
                throw new ArgumentException("El precio unitario debe ser mayor que cero.", nameof(precioUnitario));
            
            DetalleCompraID = detalleCompraID;
            CompraID = compraID;
            ProductoID = productoID;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
            Subtotal = cantidad*precioUnitario;
        }
    }
}
