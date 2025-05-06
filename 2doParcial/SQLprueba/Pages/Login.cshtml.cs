using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;
using Microsoft.AspNetCore.Http;
using WebMySqlDemo.Modelos;
using Entidades;
using SQLprueba.Pages.Entidades;
using System;
namespace SQLprueba.Pages
{
    public class LoginModel : PageModel
    {
        private readonly MySQLaux aux = new MySQLaux("localhost", "3306", "prueba", "root", "admin");
        [BindProperty]
        public Usuarios Usuarios { get; set; }
        public Signup Signup { get; set; }
        public Forgot Forgot { get; set; }
        public void OnGet()
        {

        }
        public IActionResult OnPostLogin()
        {
            bool correcto = aux.BuscarUsuario(Usuarios.usuario, Usuarios.password);
            if (correcto)
            {
                HttpContext.Session.SetString("usuario", Usuarios.usuario);
                return RedirectToPage("/Tabla");
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }
        public IActionResult OnPostRegistro()
        {
            bool existe = aux.UsuarioExistente(Usuarios.usuario);
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (existe==true)
            {
                TempData["RegistroFallido"] = "El usuario ya existe";
                return Page();
            }

            aux.RegistroUsuario(Usuarios.usuario, Usuarios.password);
            TempData["RegistroExitoso"] = "Registro completado correctamente";
            return Page();
        }
        [ValidateAntiForgeryToken]
        public JsonResult OnPostOlvide([FromBody] Usuarios usuario)
        {
            if (usuario == null || string.IsNullOrWhiteSpace(usuario.usuario))
            {
                return new JsonResult(new { error = "Usuario inválido" });
            }

            bool existe = aux.UsuarioExistente(usuario.usuario);
            if (existe)
            {
                var pass = aux.MostrarPass(usuario.usuario);
                return new JsonResult(new { password = pass });
            }
            else
            {
                return new JsonResult(new { error = "Usuario no encontrado" });
            }
        }


    }
}
