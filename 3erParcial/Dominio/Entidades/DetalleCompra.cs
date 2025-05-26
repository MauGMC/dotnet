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

    }
}
