using System.Data;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace SQLprueba.Pages;

public class IndexModel : PageModel
{
    public DataTable productos=new DataTable();
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        try
        {
        DataSet dst=new DataSet();
        conexionSQL aux=new conexionSQL("DESKTOP-N0549AQ","TEST","prueba","sa","SqL.258@","true");
        dst=aux.cargaDatos("productos");
        productos = dst.Tables[0];
        }
        catch(Exception e)
        {
            Console.WriteLine($"Error: {e}");
        }
    }
}
public class conexionSQL
{
    private string cnxstr;
    public conexionSQL(string srvrName, string instanceName, string db, string usr, string pass,string certificado)
    {
        this.cnxstr = @$"Server={srvrName}\{instanceName};Database={db};User Id={usr};Password={pass};TrustServerCertificate={certificado}";
    }
    public DataSet cargaDatos(string tabla)
    {   
        DataSet dst=new DataSet();
        try
        {
        SqlConnection cnx=new SqlConnection(this.cnxstr);
        SqlDataAdapter adp=new SqlDataAdapter(string.Format($"select * from {tabla}"),cnx);
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
    

}
