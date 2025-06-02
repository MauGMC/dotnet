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
        public Producto? Producto { get; set; }
        //Constructores
        public Inventario() { }
        public Inventario(int inventarioID, int productoID, int cantidad, int cantidadMinima,
                          int cantidadMaxima, string ubicacion, DateTime fechaUltimoMovimiento,
                          int estado, bool activo)
        {
            if (inventarioID < 0)
                throw new ArgumentOutOfRangeException("El ID del inventario no puede ser negativo.", nameof(inventarioID));
            if (productoID < 0)
                throw new ArgumentOutOfRangeException("El ID del producto no puede ser negativo.", nameof(productoID));
            if (cantidad < 0)
                throw new ArgumentOutOfRangeException("La cantidad no puede ser negativa.", nameof(cantidad));
            if (cantidadMinima < 0)
                throw new ArgumentOutOfRangeException("La cantidad mínima no puede ser negativa.", nameof(cantidadMinima));
            if (cantidadMaxima < 0)
                throw new ArgumentOutOfRangeException("La cantidad máxima no puede ser negativa.", nameof(cantidadMaxima));
            if (string.IsNullOrWhiteSpace(ubicacion))
                throw new ArgumentException("La ubicación no puede estar vacía.", nameof(ubicacion));
            InventarioID = inventarioID;
            ProductoID = productoID;
            Cantidad = cantidad;
            CantidadMinima = cantidadMinima;
            CantidadMaxima = cantidadMaxima;
            Ubicacion = ubicacion;
            FechaUltimoMovimiento = fechaUltimoMovimiento;
            Estado = estado;
            Activo = activo;
        }

    }
}
