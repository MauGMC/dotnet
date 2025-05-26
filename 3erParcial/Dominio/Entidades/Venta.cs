namespace Dominio.Entidades
{
    public class Venta
    {
        //PK
        public int VentaID { get; set; }
        //FKs
        public int ClienteID { get; set; }
        public int MetodoPagoID { get; set; }
        //Atributos
        public DateTime FechaVenta { get; set; }
        public decimal TotalSinIVA { get; set; }
        public decimal IVA { get; set; }
        public decimal TotalConIVA { get; set; }
        public int Estado { get; set; }
        //Propiedades de navegación EF
        public Cliente Cliente { get; set; } = new Cliente();
        public MetodoDePago MetodoDePago { get; set; } = new MetodoDePago();
        public ICollection<DetalleVenta> DetallesVentas { get; set; } = new HashSet<DetalleVenta>();
    }
}
