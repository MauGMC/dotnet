using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Entidades
{
    public class Producto
    {
        public Producto() { }

        public Producto(int id, string nombre, decimal precio, int cantidad, string descripcion, int categoria)
        {
            ID = id;
            Nombre = nombre;
            Precio = precio;
            Cantidad = cantidad;
            Descripcion = descripcion;
            Categoria = categoria;
        }

        [Required(ErrorMessage = "El campo ID es obligatorio.")]
        public int ID { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El precio del producto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0.")]
        public decimal Precio { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 0.")]
        [Required(ErrorMessage = "La cantidad disponible es obligatoria.")]
        public int Cantidad { get; set; }
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una categoría.")]
        public int Categoria { get; set; }

        public static List<Producto> ConvierteALista(DataSet dst)
        {
            List<Producto> lst = new List<Producto>();
            foreach (DataRow row in dst.Tables[0].Rows)
            {
                Producto p = new(
                    Convert.ToInt32(row["ID_prod"]),
                    row["Nombre"]?.ToString() ?? "",
                    Convert.ToDecimal(row["Precio"]),
                    Convert.ToInt32(row["Cantidad"]),
                    row["Descripcion"]?.ToString() ?? "",
                    Convert.ToInt32(row["Categorias"])
                );
                lst.Add(p);
            }
            return lst;
        }
    }
}
