namespace Dominio.Entidades
{
    public class Carrito
    {
        //PK
        public int CarritoID { get; set; }
        //FKs
        public int ClienteID { get; set; }
        //Atributos
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public int Estado { get; set; }
        //Propiedades de navegación EF
        public Cliente Cliente { get; set; }
        public ICollection<ProductoEnCarrito> ProductosEnCarrito { get; set; } = new HashSet<ProductoEnCarrito>();
        //Constructores
        public Carrito() { }
        public Carrito(int carritoId, int clienteId, int estado)
        {
            if(carritoId<0)
                throw new ArgumentOutOfRangeException("El ID del carrito no puede ser negativo.");
            if (clienteId < 0)
                throw new ArgumentOutOfRangeException("El ID del cliente no puede ser negativo.");
            if (estado < 0)
                throw new ArgumentOutOfRangeException("El estado del carrito está incorrecto.");
            CarritoID = carritoId;
            ClienteID= clienteId;
            Estado= estado;
            FechaCreacion = DateTime.Now;
            FechaExpiracion = FechaCreacion.AddDays(1);
        }
    }
}
