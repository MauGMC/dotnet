using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml;
using System.Xml.Schema;
using System.Text;
using System.IO;

namespace _1erProyecto.Pages;

public class IndexModel : PageModel
{
    
    public DataTable Alumnos=new DataTable();
    
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        ObtenerDatos();
    }
    private void ObtenerDatos()
    {
        try
        {
            DataSet dst = new DataSet();
            dst.ReadXmlSchema("bin/Debug/net7.0/datos.xml");
            dst.ReadXml("bin/Debug/net7.0/datos.xml");
            Alumnos = dst.Tables[0];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
