namespace Dominio.Entidades
{
    public class Compra
    {
        //PK
        public int CompraID { get; set; }
        //FKs
        public int ProveedorID { get; set; }
        public int EmpleadoID { get; set; } 
        public int MetodoDePagoID { get; set; }
        //Atributos
        public DateTime FechaCompra { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public  string NumeroFactura { get; set; } = string.Empty;
        public int Estado { get; set; }
        public decimal Total { get; set; }
        //Propiedades de navegación EF
        public Proveedor Proveedor { get; set; } = new Proveedor();
        public Empleado Empleado { get; set; } = new Empleado();
        public ICollection<DetalleCompra> DetallesCompras { get; set; } = new HashSet<DetalleCompra>();
        public MetodoDePago MetodoDePago { get; set; } = new MetodoDePago();
        //Constructores
        public Compra() { }
        public Compra(int compraId, int proveedorId, int empleadoId, int metodoPagoId,
            string numeroFactura, int estado, decimal total)
        {
            CompraID = compraId;
            ProveedorID = proveedorId;
            EmpleadoID = empleadoId;
            MetodoDePagoID = metodoPagoId;
            FechaCompra = DateTime.Now;
            FechaEntrega = DateTime.Now.AddDays(7); // Por defecto, entrega en 7 días
            NumeroFactura = numeroFactura;
            Estado = estado;
            Total = total;
        }
    }
}
