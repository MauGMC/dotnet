using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml;
using System.Xml.Schema;
using System.Text;
using System.Text.Json;
using System.Collections; // Importante para serializar JSON
public class EstadisticasModel : PageModel
{

    public DataTable Alumnos=new DataTable();
    public void OnGet()
    {
        ObtenerEstadisticas();
    }
private void ObtenerEstadisticas()
{
    try
    {
        DataSet dst = new DataSet();
        dst.ReadXmlSchema("bin/Debug/net7.0/datos.xml");
        dst.ReadXml("bin/Debug/net7.0/datos.xml");
        Alumnos = dst.Tables[0];
        List<double> promedios = new List<double>();
        List<string> nombres = new List<string>();
        ArrayList Top5Mejores=new ArrayList();
        foreach (DataRow row in Alumnos.Rows)
        {
            var primerParcial = row["Primer_parcial"].ToString();
            var segundoParcial = row["Segundo_parcial"].ToString();
            var tercerParcial = row["Tercer_parcial"].ToString();
            var extraordinario = row["Extraordinario"].ToString();

            int Calificacion(string calificacion) => calificacion == "NP" ? -1 : Convert.ToInt32(calificacion);

            var primerParcialInt = Calificacion(primerParcial);
            var segundoParcialInt = Calificacion(segundoParcial);
            var tercerParcialInt = Calificacion(tercerParcial);
            var extraordinarioInt = Calificacion(extraordinario);

            var sumatoriaParciales = 0;
            var contadorParciales = 0;

            if (primerParcialInt != -1) { sumatoriaParciales += primerParcialInt; contadorParciales++; }
            if (segundoParcialInt != -1) { sumatoriaParciales += segundoParcialInt; contadorParciales++; }
            if (tercerParcialInt != -1) { sumatoriaParciales += tercerParcialInt; contadorParciales++; }

            double promedio = contadorParciales > 0 ? sumatoriaParciales / (double)contadorParciales : 0;

            if (extraordinarioInt != -1)
            {
                promedio = Math.Max(promedio, extraordinarioInt);
            }
            if (promedio >= 6)
            {
                promedio = Math.Round(promedio, 0, MidpointRounding.AwayFromZero);
            }
            else
            {
                promedio = Math.Floor(promedio);
            }
            promedios.Add(promedio);
            nombres.Add(row["Nombres"].ToString());
        }

        
        foreach(var a in nombres)
        {
            Top5Mejores.Add(nombres);
            Top5Mejores.Add(promedios);
            Console.WriteLine($"{nombres},{promedios}");
        }            
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
}

}
