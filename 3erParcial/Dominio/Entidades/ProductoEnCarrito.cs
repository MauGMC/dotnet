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
    }
}
