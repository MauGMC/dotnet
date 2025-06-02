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
        //Constructores
        public Proveedor() { }
        public Proveedor(int proveedorId, string nombre, string? direccion, string? telefono, Email email, bool? activo = true)
        {
            if (proveedorId < 0)
                throw new ArgumentOutOfRangeException("El ID del proveedor no puede ser negativo.");
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del proveedor no puede estar vacío.", nameof(nombre));
            if (email == null)
                throw new ArgumentNullException(nameof(email), "El email no puede ser nulo.");
            ProveedorID = proveedorId;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            Email = email;
            Activo = activo;
        }


    }
}
