using System.Data;
using MySqlConnector;

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
        DataSet dst=new DataSet();
        try
        {
        MySqlConnection cnx=new MySqlConnection(this.cnxstr);
        MySqlDataAdapter adp=new MySqlDataAdapter(string.Format($"select * from {tabla}"),cnx);
        adp.Fill(dst);
        cnx.Close();
        return dst;
        }
        catch(Exception e)
        {
            Console.WriteLine("Error: "+e);
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
        public void Actualizar(string tabla, int id, string nombre, string descripcion, decimal precio, int cantidad, int categoria)
        {
            
        }
    }

}

