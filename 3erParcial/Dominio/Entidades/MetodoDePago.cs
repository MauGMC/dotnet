namespace Dominio.Entidades
{
    public class MetodoDePago
    {
        //PK
        public int MetodoDePagoID { get; set; }
        //FKs
        public int ClienteID { get; set; }
        //Atributos
        public string Nombre { get; set; } = string.Empty;
        public string? NumeroTarjeta { get; set; }
        public string? NombreTitular { get; set; }
        public string? FechaExpiracion { get; set; }
        public string? CVV { get; set; }
        public string? Tipo { get; set; }
        public string? Banco { get; set; }
        public string? Referencia { get; set; }
        public int Estado { get; set; }
        public bool Activo { get; set; }
        //Propiedades de navegación EF
        public Cliente Cliente { get; set; } = new Cliente();
        public ICollection<Venta> Ventas { get; set; } = new HashSet<Venta>();
        public ICollection<Compra> Compras { get; set; } = new HashSet<Compra>();
        public ICollection<Pedido> Pedidos { get; set; } = new HashSet<Pedido>();

    }
}
