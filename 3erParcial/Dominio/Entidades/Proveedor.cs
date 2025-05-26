using Dominio.ValueObjects;

namespace Dominio.Entidades
{
    public class Proveedor
    {
        //PK
        public int ProveedorID { get; set; }
        //Atributos
        public string Nombre { get; set; } = string.Empty;
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public Email Email { get; set; } 
        public bool? Activo { get; set; }
        //Propiedades de navegación EF
        public ICollection<Compra> Compras { get; set; } = new HashSet<Compra>();


    }
}
