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
        public Cliente Cliente { get; set; } = new Cliente();
        public ICollection<ProductoEnCarrito> ProductosEnCarrito { get; set; } = new HashSet<ProductoEnCarrito>();
    }
}
