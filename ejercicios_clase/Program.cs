using System;
using static System.Math;
public class Principal
{
    public static void Main(string[] args)
    {
        string cadena,encriptada;
        Console.WriteLine("Seleccione el programa a ingresar:");
        int opc=Int32.Parse(Console.ReadLine());
        switch(opc)
        {
            case 1: //Antro
                Basicos.Antro();
            break;
            case 2: //Area del triangulo
                Figuras.Figura1();
            break; 
            case 3: //Figura con triangulos
                Figuras.Figura2();
            break;
            case 4: //Figura del android
                Figuras.Figura3();
            break;
            case 5: //Figura del cono
                Figuras.Figura4();
            break;
            case 6: //Rotación 13
                Console.WriteLine("Ingrese su cadena:");
                cadena=Console.ReadLine();
                encriptada=Basicos.Rot13(cadena);
                Console.WriteLine("Cadena:{0}\nEncriptada:{1}",cadena,encriptada);
            break;
            case 7: //Lenguaje Efe
                Console.WriteLine("Ingrese su cadena:");
                cadena=Console.ReadLine();
                encriptada=Basicos.IdiomaEfe(cadena);
                Console.WriteLine("Cadena:{0}\nEncriptada:{1}",cadena,encriptada);
            break;
            case 8: //Persona1 (Antes de herencia, sobrecarga)
                Persona1 p0=new Persona1();
                p0.Edad=12;
                p0.Nombre="Bananin";
                Console.WriteLine(p0);
                Persona1 q0=new Persona1("Juanito",12);
                Console.WriteLine(q0);
            break;
            case 9: //Persona2 (Alumno)
                Persona2 p1=new Alumno();
                p1.Edad=18;
                p1.Nombre="Bananas";
                Console.WriteLine(p1);
                Persona2 q1 = new Alumno("Juan", 18, "2021601997");
                Console.WriteLine(q1);
            break;
            case 10: //Persona2 (Profesor)
                Persona2 p2=new Profesor();
                p2.Edad=40;
                p2.Nombre="Platano";
                Console.WriteLine(p2);
                Persona2 q2 =new Profesor("Don Juan",40,"25364887");
                Console.WriteLine(q2);
            break;
            default:
                Console.WriteLine("Selección no encontrada");
            break;
            

            
        }

    }
}
public class Basicos
{
    public static void Antro()
    {
        int h, m;
        const float COVER = 500;
        const float DESC = 0.2f;
        Console.WriteLine("Cuantos hombres son:");
        h = Int32.Parse(Console.ReadLine());
        Console.WriteLine("Cuantas mujeres son:");
        m = Int32.Parse(Console.ReadLine());
        float total = h * COVER + m * COVER - m * COVER * DESC;
        Console.WriteLine(total);

    }
    public static string Rot13(string org)
    {
        string res = string.Empty;
        foreach (char c in org.ToCharArray())
        {
            int temp;
            if (c>='a' && c<='m')
            {
                temp=(int)c+13;
                res+=(char)temp;
            }
            else if(c>'m' && c<='z')
            {
                temp=(int)c-13;
                res+=(char)temp;
            }
        }
        return res+"";
    }
    public static string IdiomaEfe(string org)
    {
        //StringBuilder res=new StringBuilder();
        string res = string.Empty;
        foreach (char c in org.ToCharArray())
        {
            int temp;
            if (c == 'a') //Incluye todas las vocales
            {
                temp = (int)c;

                temp += 5;
                res += (char)c; //res.Append(c);
                res += (char)temp; //res.Append('f');
                res += (char)c; //res.Append('c');
            }
            else if (c == 'e')
            {
                temp = (int)c;
                temp += 1;
                res += (char)c;
                res += (char)temp;
                res += (char)c;
            }
            else if (c == 'i')
            {
                temp = (int)c;
                temp -= 3;
                res += (char)c;
                res += (char)temp;
                res += (char)c;
            }
            else if (c == 'o')
            {
                temp = (int)c;
                temp -= 9;
                res += (char)c;
                res += (char)temp;
                res += (char)c;
            }
            else if (c == 'u')
            {
                temp = (int)c;
                temp -= 15;
                res += (char)c;
                res += (char)temp;
                res += (char)c;
            }
            else
            {
                temp = (char)c;
                res += (char)temp; //res.Append(c);
            }
        }
        return res+"";
    }
}
public class Figuras
{
    public static void Figura1() //Area y perimetro de un triangulo
    {
        int b, a;
        double area, perimetro;
        Console.WriteLine("Introduce la base;");
        b = Int32.Parse(Console.ReadLine());
        Console.WriteLine("Introduce la altura;");
        a = Int32.Parse(Console.ReadLine());
        area = b * a / 2;
        perimetro = b + a + Math.Sqrt(a * a + b * b);
        Console.WriteLine("Area:{0}", area);
        Console.WriteLine("Perimetro{0}:", perimetro);
    }
    public static void Figura2() //3 triangulos, 1 cuadrado, 1 rectángulo
    {
        int L;
        double totalArea=0,totalPeri=0;
        
        Console.WriteLine ("Ingresa L:");
        L=Int32.Parse(Console.ReadLine());
        
        totalArea=
            (((L*L)/2)*2)+
            (((L*1.5)*L)/2)+
            (L*L)+
            ((0.25*L)*(2*L));
        totalPeri=
                Math.Sqrt((L*L)+(L*L))+ //Hipotenusa arriba
                Math.Sqrt(((1.5*L)*(1.5*L))+(L*L))+ //Hipotenusa izq
                L*1.5+
                Math.Sqrt((L*L)+(L*L))+
                L*0.5+
                L*0.25+
                2*L+
                0.25*L+
                0.5*L;
        Console.WriteLine("Area total:{0}",totalArea);
        Console.WriteLine("Perimetro total:{0}",totalPeri);
    }
    public static void Figura3()//Android
    {
        int L=7;
        double aCirc,aCuad1,aCuad2,aRect,pCirc,aTotal,pTotal;
        aCirc=PI*Pow(L/2,2);
        aCuad1=Pow(L,2);
        aCuad2=Pow(L/5,2);
        aRect=L/5*3*L/5;
        pCirc=PI*L;
        aTotal=aCirc/2+aCuad1+2*aCuad2+2*aRect;
        pTotal=23*L/5+pCirc/2;
        Console.WriteLine("Area:{0}, Perimetro:{1}:",aTotal,pTotal);

    }
    public static void Figura4()//Cono raro
    {
         int L;
        Console.WriteLine("Ingrese L:");
        L=Int32.Parse(Console.ReadLine());
        double areatotal=
            (2*3*L)+
            ((L*(3*L))/2)*2+
            (Math.PI*Math.Pow((3*L)/2,2))/2;
        double peritotal=
            2*Math.PI*((L*1.5)/2)+
            Math.Sqrt((L*L)+(Math.Pow((L*3),2)))*2+
            L*7;       
        Console.WriteLine("Area:{0}",areatotal);   
        Console.WriteLine("Perimetro:{0}",peritotal);   

    }
}

public class Persona1
{
    private string nombre;
    private int edad;
    public string Nombre
    {
        set { nombre = value; }
        get { return nombre; }
    }
    public int Edad
    {
        set { edad = (value >= 0 && value <= 150) ? value : 0; }
        get { return edad; }
    }
    public override string ToString()
    {
        return string.Format("Ejemplo\n{{\nNombre:{0}\nEdad:{1}\n}}", nombre, edad);
    }
    public Persona1() { }
    public Persona1(string nombre, int edad)
    {
        this.Nombre = nombre;
        this.Edad = edad;
    }
}
abstract class Persona2
{
    private string nombre;
    private int edad;
    public string Nombre
    {
        set { nombre = value; }
        get { return nombre; }
    }
    public int Edad
    {
        set { edad = (value >= 0 && value <= 150) ? value : 0; }
        get { return edad; }
    }
    public override string ToString()
    {
        return string.Format("Ejemplo\n{{\nNombre:{0}\nEdad:{1}\n", nombre, edad);
    }
    public Persona2() { }
    public Persona2(string nombre, int edad)
    {
        this.Nombre = nombre;
        this.Edad = edad;
    }
}
class Alumno : Persona2
{
    private string Boleta { set; get; }
    public Alumno() { }
    public Alumno(string nombre, int edad, string boleta) : base(nombre, edad)
    {
        this.Boleta = boleta;
    }
    public override string ToString()
    {
        return base.ToString() + string.Format("Boleta:{0}\n}}", Boleta);
    }

}
class Profesor:Persona2
{
    private string Clave { set; get; }
    public Profesor(){}
    public Profesor(string nombre, int edad, string clave):base(nombre,edad)
    {
        this.Clave=clave;
    }
    public override string ToString()
    {
        return base.ToString()+string.Format("Clave:{0}\n}}",Clave);
    }
}