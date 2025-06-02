using Microsoft.EntityFrameworkCore;
using Infraestructura;
using Dominio.Interfaces;
using Aplicacion.Servicios;
using Infraestructura.Repositorios;
using Aplicacion.Validaciones;
using FluentValidation.AspNetCore;
using FluentValidation;
using DinkToPdf;
using DinkToPdf.Contracts;
using Web.Servicios;
using Web.Helpers;
using Aplicacion.Servicios.Interfaces;
namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var context = new CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "dll", "libwkhtmltox.dll"));
            //Servicios 
            builder.Services.AddSingleton<RazorViewToStringRenderer>();
            builder.Services.AddSingleton(typeof(IConverter),new SynchronizedConverter(new PdfTools()));
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<ProductoDetalleValidator>();
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
                );
            });

            //Repositorios
            builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
            builder.Services.AddScoped<ICsvLogRepository, CsvLogRepository>();

            //Servicios
            builder.Services.AddScoped<ICsvLogService, CsvLogService>();
            builder.Services.AddScoped<ObtenerProductoService>();
            builder.Services.AddScoped<ProductoService>();
            builder.Services.AddScoped<ImportarCsvService>();
            builder.Services.AddScoped<ReportePdfService>();
            builder.Services.AddScoped<CustomAssemblyLoadContext>();
            //Configuración de AutoMapper
            builder.Services.AddAutoMapper(
                typeof(Aplicacion.Perfiles_de_mapeo.ProductoProfile).Assembly,
                typeof(Aplicacion.Perfiles_de_mapeo.CsvLogProfile).Assembly,
                typeof(Web.Perfiles_de_mapeo.CsvLogsProfile).Assembly,
                typeof(Web.Perfiles_de_mapeo.ProductoViewModelProfile).Assembly
            );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
