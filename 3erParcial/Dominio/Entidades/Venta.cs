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
        //Constructores
        public Venta() { }
        public Venta(int ventaId, int clienteId, int metodoPagoId, decimal totalSinIVA, decimal iva, decimal totalConIVA, int estado)
        {
            if (ventaId < 0)
                throw new ArgumentOutOfRangeException("El ID de la venta no puede ser negativo.");
            if (clienteId < 0)
                throw new ArgumentOutOfRangeException("El ID del cliente no puede ser negativo.");
            if (metodoPagoId < 0)
                throw new ArgumentOutOfRangeException("El ID del método de pago no puede ser negativo.");
            if (totalSinIVA < 0)
                throw new ArgumentOutOfRangeException("El total sin IVA no puede ser negativo.");
            if (iva < 0)
                throw new ArgumentOutOfRangeException("El IVA no puede ser negativo.");
            if (totalConIVA < 0)
                throw new ArgumentOutOfRangeException("El total con IVA no puede ser negativo.");
            if (estado < 0)
                throw new ArgumentOutOfRangeException("El estado de la venta está incorrecto.");
            VentaID = ventaId;
            ClienteID = clienteId;
            MetodoPagoID = metodoPagoId;
            FechaVenta = DateTime.Now; // Por defecto, la fecha de venta es hoy
            TotalSinIVA = totalSinIVA;
            IVA = iva;
            TotalConIVA = totalConIVA;
            Estado = estado;
        }
    }
}
