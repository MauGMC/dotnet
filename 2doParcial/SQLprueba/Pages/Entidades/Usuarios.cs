using System.ComponentModel.DataAnnotations;

namespace SQLprueba.Pages.Entidades
{
    public class Usuarios
    {
        public Usuarios() { }
        [Required(ErrorMessage = "Ingrese su usuario, por favor.")]
        public string usuario { get; set; }
        [Required(ErrorMessage = "Ingrese su contraseña, por favor.")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string confirmacion { get; set; }
    }
}
