﻿
namespace Dominio.Entidades
{
    public class Producto
    {
        public int ProductoID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public int Categoria { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string RutaImagen { get; set; }
    }
}
