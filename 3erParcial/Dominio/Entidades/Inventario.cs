namespace Dominio.Entidades
{
    public class Inventario
    {
        //PK
        public int InventarioID { get; set; }
        //FKs
        public int ProductoID { get; set; }
        //Atributos
        public int Cantidad { get; set; }
        public int CantidadMinima { get; set; }
        public int CantidadMaxima { get; set; }
        public string Ubicacion { get; set; } = string.Empty;
        public DateTime FechaUltimoMovimiento { get; set; }
        public int Estado { get; set; }
        public bool Activo { get; set; }
        //Propiedades de navegación EF
        public Producto Producto { get; set; } = new Producto();

    }
}
