using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections;
namespace Agenda;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine(Menu());
    }
    public static string Menu()
    {
        int opc;
        int x=1;
        ArrayList cad = new();
        do
        {
            Persona p=new Persona();
            Console.Clear();
            Console.WriteLine("\nIngreza tu opcion:\n1.-Registrar\n2.-Editar\n3.-Eliminar\n4.-Consultar\n5.-Salir");
            opc = Int32.Parse(Console.ReadLine());
            
            switch (opc)
            {
                case 1: //Registro
                    Console.Clear();
                    Console.WriteLine("Ingrese su nombre:");
                    p.Nombre=Console.ReadLine(); 
                    cad.Add(p.Nombre);
                    Console.WriteLine("Ingrese su teléfono celular:");
                    p.Telefono=Console.ReadLine();
                    cad.Add(p.Telefono);
                    Console.WriteLine("Ingrese su correo electrónico:");
                    p.Email=Console.ReadLine();
                    cad.Add(p.Email);
                    Console.WriteLine("Su numero de registro es: {0}",x++);
                    Console.ReadLine();
                    Console.Clear();
                    break;
                case 2: //Editar
                    Console.Clear();
                    Console.WriteLine("Ingrese su numero de registro:");
                    int y=Int32.Parse(Console.ReadLine());
                    Console.WriteLine("¿Qué elemento desea modificar?\n1.-Nombre\n2.-Telefono\n3.-Correo\n");
                    opc=Int32.Parse(Console.ReadLine());
                    if(opc==1)
                    {
                        Console.WriteLine("Ingrese su nombre:");
                        p.Nombre=Console.ReadLine(); 
                        cad[(y - 1) * 3]=p.Nombre;
                        Console.WriteLine("Nombre modificado exitosamente");
                    }
                    else if(opc==2)
                    {
                        Console.WriteLine("Ingrese su telefono:");
                        p.Telefono=Console.ReadLine(); 
                        cad[((y - 1) * 3) + 1]=p.Telefono;
                        Console.WriteLine("Telefono modificado exitosamente");
                    }
                    else if(opc==3)
                    {
                        Console.WriteLine("Ingrese su correo:");
                        p.Email=Console.ReadLine(); 
                        cad[((y - 1) * 3) + 2]=p.Email;
                        Console.WriteLine("Correo modificado exitosamente");
                    }
                    else
                    {
                        Console.WriteLine("Dato no válido");
                    }
                    Console.ReadLine();
                    Console.Clear();
                    break;
                case 3: //Eliminar
                    Console.Clear();
                    Console.WriteLine("¿Qué registro desea eliminar?");
                    y=Int32.Parse(Console.ReadLine());
                    cad.RemoveRange((y - 1) * 3, 3);
                    Console.WriteLine("Registro eliminado exitosamente");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                case 4: //Mostrar
                    Console.Clear();
                    if (cad.Count == 0)
                    {
                        Console.WriteLine("No hay registros.");
                    }
                    else
                    {
                        Console.WriteLine("Ingrese la opción deseada:\n1.-Mostrar todo\n2.-Buscar");
                        opc=Int32.Parse(Console.ReadLine());
                        if(opc==1)
                        {
                            Console.Clear();
                            Console.WriteLine("Registros actuales:");
                            for (int i = 0; i < cad.Count; i += 3)
                            {
                               Console.WriteLine("{0}.- {1} {2} {3}", (i / 3) + 1, cad[i], cad[i + 1], cad[i + 2]);
                            }
                        }
                        else if(opc==2)
                        {
                            Console.Clear();
                            Console.WriteLine("Ingrese el numero de registro:");
                            y=Int32.Parse(Console.ReadLine());
                            Console.WriteLine("{0}.- {1} {2} {3}",y, cad[(y - 1) * 3], cad[((y - 1) * 3)+1], cad[((y - 1) * 3)+2]);
                        }
                        
                    }
                    Console.ReadLine();
                    Console.Clear();
                    break;
                case 5: //Salir
                    Console.WriteLine("Hasta luego");
                    break;
                default:
                    Console.WriteLine("Opcion no valida");
                    break;
            }
        } while (opc != 5);
        return "";
    }
}
public class Persona
{
    private string nombre;
    private string telefono;
    private string email;
    public string Nombre { set; get; }
    public string Telefono
    {
        set
        {
            Regex cadena = new Regex(@"^(?:\d{2}\s?\d{4}\s?\d{4})$");
            if (cadena.IsMatch(value))
            {
                telefono = value;
            }
            else
            {
                telefono = "##########";
            }
        }
        get { return telefono; }
    }
    public string Email
    {
        set
        {
            Regex cadena = new Regex(@"^[\w.-]+@[a-zA-Z\d.-]+\.[a-zA-Z]{2,}(?:\.[a-zA-Z]{2,})?$");
            if (cadena.IsMatch(value))
            {
                email = value;
            }
            else
            {
                email = "usuario@dominio.com";
            }
        }
        get { return email; }
    }
    public override string ToString()
    {
        return string.Format("Ejemplo:\nNombre:{0}\nTelefono:{1}\nEmail:{2}", nombre, telefono, email);
    }
    public Persona(){}
    public Persona(string nombre, string telefono, string email)
    {
        this.Nombre = nombre;
        this.Telefono = telefono;
        this.Email = email;
    }
}