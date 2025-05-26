namespace Dominio.Entidades
{
    public class Direccion
    {
        //PK
        public int DireccionID { get; set; }
        //Atributos
        public string Calle { get; set; } = string.Empty;
        public int NumeroInterior { get; set; }
        public int NumeroExterior { get; set; }
        public string Colonia { get; set; } = string.Empty;
        public string CodigoPostal { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string? Referencia { get; set; }
        public string? Telefono { get; set; }
        //Propiedades de navegación EF
        public Cliente Cliente { get; set; } = new Cliente();
        public ICollection<Pedido> Pedidos { get; set; } = new HashSet<Pedido>();
        public ICollection<Empleado> Empleados { get; set; } = new HashSet<Empleado>();

    }
}
