using System.Data;
using Entidades;
using MySqlConnector;
using SQLprueba.Pages.Entidades;

namespace WebMySqlDemo.Modelos
{
    public class MySQLaux
    {
        private string cnxstr;
        public MySQLaux(string srvrName, string puerto, string db, string usr, string pass)
        {
            this.cnxstr = @$"Server={srvrName};Port={puerto};Database={db};Uid={usr};Pwd={pass}";
        }
        public DataSet SeleccionTodo(string tabla)
        {
            DataSet dst = new DataSet();
            try
            {
                MySqlConnection cnx = new MySqlConnection(this.cnxstr);
                MySqlDataAdapter adp = new MySqlDataAdapter(string.Format($"select * from {tabla}"), cnx);
                adp.Fill(dst);
                cnx.Close();
                return dst;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }
            return dst;
        }
        public void AgregarNuevo(string tabla, string nombre, string descripcion, decimal precio, int cantidad, int categoria)
        {
            using (var cnx = new MySqlConnection(this.cnxstr))
            {
                cnx.Open();

                string query = $"INSERT INTO {tabla} (Nombre, Descripcion, Precio, Cantidad, Categorias) VALUES (@nombre, @descripcion, @precio, @cantidad, @categoria)";

                using (var cmd = new MySqlCommand(query, cnx))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@precio", precio);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@categoria", categoria);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public Producto SeleccionPorID(int id)
        {
            Producto producto = null;
            using (var cnx = new MySqlConnection(this.cnxstr))
            {
                cnx.Open();
                string query = $"SELECT * FROM productos WHERE ID_prod = @ID";
                using (var cmd = new MySqlCommand(query, cnx))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            producto = new Producto
                            {
                                ID = reader.GetInt32("ID_prod"),
                                Nombre = reader.GetString("Nombre"),
                                Descripcion = reader.GetString("Descripcion"),
                                Precio = reader.GetDecimal("Precio"),
                                Cantidad = reader.GetInt32("Cantidad"),
                                Categoria = reader.GetInt32("Categorias")
                            };
                        }
                    }


                }
            }

            return producto;
        }
        public void EditarProducto(string tabla, int id, string nombre, string descripcion, decimal precio, int cantidad, int categoria)
        {
            using var cnx = new MySqlConnection(this.cnxstr);
            cnx.Open();

            var query = $"UPDATE {tabla} SET Nombre = @nombre, Descripcion = @descripcion, Precio = @precio, Cantidad = @cantidad, Categorias = @categoria WHERE ID_prod = @id";

            using var cmd = new MySqlCommand(query, cnx);

            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@descripcion", descripcion);
            cmd.Parameters.AddWithValue("@precio", precio);
            cmd.Parameters.AddWithValue("@cantidad", cantidad);
            cmd.Parameters.AddWithValue("@categoria", categoria);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }
        public void EliminarProducto(string tabla, int id)
        {
            using var cnx = new MySqlConnection(this.cnxstr);
            cnx.Open();
            var query = $"DELETE FROM {tabla} WHERE ID_prod = @id";
            using var cmd = new MySqlCommand(query, cnx);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
        public List<Producto> ObtenerProductos()
        {
            var productos = new List<Producto>();

            using (var conn = new MySqlConnection(this.cnxstr))
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM Productos", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productos.Add(new Producto
                        {
                            ID = reader.GetInt32("ID_prod"),
                            Nombre = reader.GetString("Nombre"),
                            Descripcion = reader.GetString("Descripcion"),
                            Precio = reader.GetDecimal("Precio"),
                            Cantidad = reader.GetInt32("Cantidad"),
                            Categoria = reader.GetInt32("Categorias")
                        });
                    }
                }
            }

            return productos;
        }
        public bool BuscarUsuario(string usuario, string password)
        {
            using var connection = new MySqlConnection(cnxstr);
            connection.Open();

            string query = "SELECT COUNT(*) FROM usuarios WHERE usuario = @usuario AND pass = @password";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@usuario", usuario);
            cmd.Parameters.AddWithValue("@password", password);

            var result = Convert.ToInt32(cmd.ExecuteScalar());
            return result > 0;
        }
        public void RegistroUsuario(string usuario, string password)
        {
            using var connection = new MySqlConnection(cnxstr);
            connection.Open();
            string query = "INSERT INTO usuarios (usuario, pass) VALUES (@usuario, @password)";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@usuario", usuario);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.ExecuteNonQuery();
        }
        public bool UsuarioExistente(string usuario)
        {
            using var connection = new MySqlConnection(cnxstr);
            connection.Open();
            string query = "SELECT COUNT(*) FROM usuarios WHERE usuario = @usuario";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@usuario", usuario);
            var result = Convert.ToInt32(cmd.ExecuteScalar());
            return result > 0;
        }
        public string MostrarPass(string usuario)
        {
            using var connection = new MySqlConnection(cnxstr);
            connection.Open();
            string query = "SELECT pass FROM usuarios WHERE usuario = @usuario";

            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@usuario", usuario);

            var result = cmd.ExecuteScalar();
            return result?.ToString();
        }

    }
}

