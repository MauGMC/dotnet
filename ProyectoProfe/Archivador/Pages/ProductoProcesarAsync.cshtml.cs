using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;
using System.Data;
using System.Data.SqlClient;
using Entities;
using Microsoft.Extensions.Configuration;
using Models;

namespace WebAppCRUDMySQL.Pages;

public class ProductoProcesarAsyncModel : PageModel
{
    private readonly IConfiguration _configuration;

    public ProductoProcesarAsyncModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    [ValidateAntiForgeryToken]
    public JsonResult OnPost([FromBody] Producto producto)
    {
        Console.WriteLine("Si entra");

        if (producto == null)
        {
            Console.WriteLine("Producto es null");
            return new JsonResult(new { success = 0, message = "El objeto producto no se recibió." });
        }

        try
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                if (producto.Id == -1)
                {
                    string query = "INSERT INTO producto (Nombre, Precio, Descripcion) VALUES (@Nombre, @Precio, @Descripcion)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nombre", producto.Nombre);
                        command.Parameters.AddWithValue("@Precio", producto.Precio);
                        command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return new JsonResult(new { success = 1, message = "Producto insertado con éxito." });
                        }
                        else
                        {
                            return new JsonResult(new { success = 0, message = "No se pudo insertar el producto." });
                        }
                    }
                }
                else
                {
                    string query = "UPDATE producto SET nombre = @Nombre, precio = @Precio, descripcion = @Descripcion WHERE id_producto = @id";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nombre", producto.Nombre);
                        command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                        command.Parameters.AddWithValue("@Precio", producto.Precio);
                        command.Parameters.AddWithValue("@id", producto.Id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return new JsonResult(new { success = 1, message = "Producto actualizado con éxito." });
                        }
                        else
                        {
                            return new JsonResult(new { success = 0, message = "No se pudo actualizar el producto." });
                        }
                    }
                }
            }
        }
        
        catch (Exception ex)
        {
            return new JsonResult(new { success = 0, message = $"Error: {ex.Message}" });
        }
    }
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostEliminar([FromBody]int id)
    {
        string connectionString = _configuration.GetConnectionString("DefaultConnection");
        Console.WriteLine($"{id}");
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();
            string query = "DELETE FROM producto WHERE id_producto = @id";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                int rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    return new JsonResult(new { success = true, message = "Producto eliminado con éxito." });
                }
                else
                {
                    return new JsonResult(new { success = false, message = "No se pudo eliminar el producto." });
                }
            }
        }
    }



}