using CatalogoProductosDominio.Enums;
using System.ComponentModel.DataAnnotations;

namespace CatalogoProductosDominio.Entidades
{
    public class Productos
    {
        [Key]
        public int ID_prod { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "Favor de poner el nombre del producto")]
        public string Nombre { get; set; }
        [StringLength(100)]
        [Required]
        public string? Descripcion { get; set; }
        [Required(ErrorMessage = "Favor de poner el precio del producto")]
        public double Precio { get; set; }
        [Required(ErrorMessage = "Favor de poner la cantidad del producto")]
        public int Cantidad { get; set; }
        [EnumDataType(typeof(Categorias),ErrorMessage ="Favor de escoger una categoría")]
        public Categorias Categorias { get; set; }
    }
}
