using Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SQLprueba.Pages.Entidades;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.X86;
using WebMySqlDemo.Modelos;

namespace SQLprueba.Pages
{
    public class Signup
    {
        [BindProperty]
        public Usuarios Usuarios { get; set; } = new Usuarios();
        
    }
}
