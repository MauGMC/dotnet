namespace Dominio.Entidades
{
    public class DetalleVenta
    {
        //PK
        public int DetalleVentaID { get; set; }
        //FKs
        public int VentaID { get; set; }
        public int ProductoID { get; set; }
        //Atributos
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
        //Propiedades de navegación EF
        public Venta Venta { get; set; } = new Venta();
        public Producto Producto { get; set; } = new Producto();
    }
}
