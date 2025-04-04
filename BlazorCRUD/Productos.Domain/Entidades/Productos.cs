using Productos.Dominio.Enums;

namespace Productos.Dominio.Entidades
{
    public class Productos
    {
        public int id_productos { get; set; }
        public string nombre { get; set; }
        public int cantidad { get; set; }
        public float precio { get; set; }
        public Marca Marca { get; set; }
    }
}