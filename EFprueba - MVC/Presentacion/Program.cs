using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presentacion.Data;
using Infraestructura;
using Dominio.Interfaces;
using Aplicacion.Servicios;
using Infraestructura.Repositorios;
using Aplicacion.Validaciones;
using FluentValidation.AspNetCore;
using FluentValidation;
using DinkToPdf;
using DinkToPdf.Contracts;
using Presentacion.Servicios;
using Presentacion.Helpers;
using Aplicacion.Servicios.Interfaces;

namespace Presentacion
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Cargar DLL de DinkToPdf
            var context = new CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "dll", "libwkhtmltox.dll"));

            // Cadena de conexión
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                   ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            // DbContext único para tu app e Identity
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            // Identity usando AppDbContext
            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<AppDbContext>();

            // FluentValidation
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<ProductoDetalleValidator>();

            // Repositorios
            builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
            builder.Services.AddScoped<ICsvLogRepository, CsvLogRepository>();

            // Servicios
            builder.Services.AddScoped<ICsvLogService, CsvLogService>();
            builder.Services.AddScoped<ObtenerProductoService>();
            builder.Services.AddScoped<ProductoService>();
            builder.Services.AddScoped<ImportarCsvService>();
            builder.Services.AddScoped<ReportePdfService>();
            builder.Services.AddScoped<CustomAssemblyLoadContext>();
            builder.Services.AddSingleton<RazorViewToStringRenderer>();
            builder.Services.AddSingleton<IConverter>(new SynchronizedConverter(new PdfTools()));

            // AutoMapper
            builder.Services.AddAutoMapper(
                typeof(Aplicacion.Perfiles_de_mapeo.ProductoProfile).Assembly,
                typeof(Aplicacion.Perfiles_de_mapeo.CsvLogProfile).Assembly,
                typeof(Presentacion.Perfiles_de_mapeo.CsvLogsProfile).Assembly,
                typeof(Presentacion.Perfiles_de_mapeo.ProductoViewModelProfile).Assembly
            );

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}
