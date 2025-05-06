//dotnet add package MySqlConnector
using System.Data;
using Entities;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
namespace Models;
class MySqlAux {
    private string cnxstr;
    public MySqlAux(string srv, int pto, string bd, string usr, string pass) {
        this.cnxstr = string.Format($"Server={srv};Port={pto};Database={bd};Uid={usr};Pwd={pass};");
    }
    private readonly ILogger<MySqlAux> _logger;

    public MySqlAux(ILogger<MySqlAux> logger)
    {
        _logger = logger;
    }
    public DataSet SeleccionaTodo(string tbl) {
        DataSet dst = new DataSet();

        //Lógica para seleccionar todo
        MySqlConnection cnx = new MySqlConnection(cnxstr);
        MySqlDataAdapter adp = new MySqlDataAdapter(string.Format("SELECT * FROM {0}", tbl), cnx);
        adp.Fill(dst);
        cnx.Close();
        //Fin de lógica
        
        return dst;
    }
    public Producto Buscar(int id)
    {
        Producto producto = null;

        using (MySqlConnection cnx = new MySqlConnection(cnxstr))
        {
            string query = "SELECT * FROM producto WHERE id_producto = @Id";
            using (MySqlCommand cmd = new MySqlCommand(query, cnx))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cnx.Open();

            
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        producto = new Producto
                        {
                            Id = reader.GetInt32("id_producto"),
                            Nombre = reader.GetString("nombre"),
                            Precio = reader.GetDecimal("precio"),
                            Descripcion = reader.GetString("descripcion")
                        };
                    }
                }
            }
        }

    return producto;
    }

    
}