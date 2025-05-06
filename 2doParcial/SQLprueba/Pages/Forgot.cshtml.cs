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
    public class Forgot    {
        [BindProperty]
        public Usuarios Usuarios { get; set; }
        public void OnGet()
        {
        }
    }
}
