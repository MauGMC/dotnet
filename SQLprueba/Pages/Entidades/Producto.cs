using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.Extensions.Caching.Memory;

namespace Entidades;
public class Producto
{
    public Producto()
    {

    }
    public Producto(int id, string nombre, decimal precio, string descripcion,int categoria)
    {
        ID=id;
        Nombre=nombre;
        Precio=precio;
        Descripcion=descripcion;
        Categoria=categoria;
    }
    [Required]
    public int ID {get;set;}
    [Required]
    public string Nombre {get;set;}
    [Required]
    public decimal Precio {get;set;}
    [Required]
    public int Cantidad {get;set;}
    [Required]
    public string? Descripcion {get;set;}
    public int Categoria{get;set;}
    public static List<Producto> ConvierteALista(DataSet dst)
{
    List<Producto> lst = new List<Producto>();
    foreach (DataRow row in dst.Tables[0].Rows)
    {
        
            Producto p = new(
                Convert.ToInt32(row["Id"]),
                row["Nombre"]?.ToString() ?? "",
                Convert.ToDecimal(row["Precio"]),
                row["Descripcion"]?.ToString() ?? "",
                Convert.ToInt32(row["Categoria"])            );
            lst.Add(p);
    }
    return lst;
}



}