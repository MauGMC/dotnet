using Entidades;
using Microsoft.AspNetCore.Mvc.Rendering;

public class AgregarModal
{
    public Producto Producto { get; set; } = new Producto();
    public List<SelectListItem> CategoriasLista { get; set; } = new List<SelectListItem>();
}
