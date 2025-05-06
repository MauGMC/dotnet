using System.Data;

namespace Entities;

public class Producto
{
    public required int? Id { get; set; }
    public required string Nombre { get; set; }
    public required decimal Precio { get; set; }
    public string? Descripcion { get; set; }
    public Producto(){}
    public static List<Producto> ConvierteALista(DataSet dst) {
        List<Producto> lst = new List<Producto>();
        foreach (DataRow row in dst.Tables[0].Rows)
        {
            Producto p = new Producto() {
                Id = Convert.ToInt32(row["id_producto"]),
                Nombre = row["nombre"].ToString() ?? "",
                Precio = Convert.ToDecimal(row["precio"]),
                Descripcion = row["descripcion"].ToString() ?? ""
            };
            lst.Add(p);
        }
        return lst;
    }
}