namespace Aplicacion.Utilidades
{
    public class ResultadoConversion<T>
    {
        public T Valor { get; set; }
        public List<string> Advertencias { get; set; } = new();
        public List<string> Errores { get; set; } = new();
        public List<string> Exitos { get; set; } = new();
        public bool TieneErrores => Errores.Any(); 
    }
}
