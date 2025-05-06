using Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SQLprueba.Pages
{
    public class EditarModal
    {
        public Producto Producto { get; set; } = new Producto();
        public List<SelectListItem> CategoriasLista { get; set; } = new List<SelectListItem>();
        public EditarModal editarModal { get; set; }

    }
}
