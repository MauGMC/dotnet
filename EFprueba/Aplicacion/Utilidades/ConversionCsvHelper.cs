namespace Aplicacion.Utilidades
{
    public class ConversionCsvHelper
    {
        public static ResultadoConversion<int> ConvertirID(string idStr, HashSet<int> idsEncontrados)
        {
            var resultado=new ResultadoConversion<int>();
            if (int.TryParse(idStr, out int id))
            {
                if (id <= 0)
                {
                    resultado.Valor = 0;
                    resultado.Advertencias.Add($"Advertencia: ID {idStr} es menor o igual a 0. Se asignó valor nuevo");
                }
                else if(!idsEncontrados.Add(id))
                {
                    resultado.Valor = 0;
                    resultado.Errores.Add($"Error: ID {id} duplicado.");
                }
                else
                {
                    resultado.Valor = id;
                    resultado.Exitos.Add($"ID recibido correctamente.");
                }
            }
            else
            {
                resultado.Valor=0;
                resultado.Advertencias.Add($"Advertencia: ID {idStr} contiene elementos no válidos. Se asigna valor nuevo");
            }
            return resultado;
        }
        public static ResultadoConversion<string> ConvertirNombre(string nombre, HashSet<string> nombresEncontrados)
        {
            var resultado = new ResultadoConversion<string>();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                resultado.Valor = null;
                resultado.Errores.Add("Error: El nombre del producto no puede estar vacío.");
            }
            else if (nombre.Length > 30)
            {
                resultado.Valor = null;
                resultado.Errores.Add($"Error: Nombre '{nombre}' excede el límite de 30 caracteres.");
            }
            else if (!nombresEncontrados.Add(nombre.Trim()))
            {
                resultado.Valor = null;
                resultado.Errores.Add($"Error: Nombre '{nombre}' duplicado.");
            }
            else
            {
                resultado.Valor = nombre;
                resultado.Exitos.Add($"Nombre recibido correctamente.");
            }
            return resultado;
        }
        public static ResultadoConversion<string> ConvertirDescripcion(string descripcion)
        {
            var resultado = new ResultadoConversion<string>();
            if(string.IsNullOrEmpty(descripcion))
            {
                resultado.Valor = string.Empty;
                resultado.Advertencias.Add("Advertencia: Descripción vacía.");
            }
            else if (descripcion.Length > 100)
            {
                resultado.Valor = descripcion.Substring(0, 100); 
                resultado.Advertencias.Add($"Advertencia: Descripción '{descripcion}' excedió el límite de 100 caracteres. Se recortó la cadena");
            }
            else
            {
                resultado.Valor = descripcion;
                resultado.Exitos.Add($"Descripción recibida correctamente.");
            }
            return resultado;
        }
        public static ResultadoConversion<int> ConvertirCantidad(string cantidad)
        {
            var resultado = new ResultadoConversion<int>();
            if (int.TryParse(cantidad, out int cantidadConvertida))
            {
                
                if (cantidadConvertida <= 0)
                {
                    resultado.Valor = 1;
                    resultado.Advertencias.Add($"Advertencia: Un valor en cantidad ({cantidad}) es igual o menor a 0. Se asignó valor '1'.");
                }
                else
                {
                    resultado.Valor = cantidadConvertida;
                    resultado.Exitos.Add($"Cantidad recibida correctamente.");
                }
            }
            else
            {
                resultado.Valor = 1;
                resultado.Advertencias.Add($"Advertencia: Un valor en cantidad ({cantidad}) contiene elementos inválidos. Se asignó valor '1'.");
            }
                return resultado;
        }
        public static ResultadoConversion<decimal> ConvertirPrecio(string precio)
        {
            var resultado = new ResultadoConversion<decimal>();
            if(decimal.TryParse(precio, out decimal precioConvertido))
            {
                if(precioConvertido <= 0)
                {
                    resultado.Valor = 1.5m;
                    resultado.Advertencias.Add($"Advertencia: Un valor en precio ({precio}) es igual o menor a 0. Se asignó valor '1.5'.");
                }
                else
                {
                    resultado.Valor= precioConvertido;
                    resultado.Exitos.Add($"Precio recibido correctamente.");
                }
            }
            else
            {
                resultado.Valor = 1.5m;
                resultado.Advertencias.Add($"Advertencia: Un valor en precio ({precio}) contiene elementos inválidos. Se asignó valor '1.5'.");
            }
            return resultado;
        }
        public static ResultadoConversion<DateTime> ConvertirFechaCreacion(string fecha)
        {
            var resultado = new ResultadoConversion<DateTime>();
            if (DateTime.TryParse(fecha, out DateTime fechaConvertida))
            {
                if(string.IsNullOrWhiteSpace(fecha))
                {
                    resultado.Valor = DateTime.Today;
                    resultado.Advertencias.Add($"Advertencia: Un valor en fecha está vacío. Se asignó la fecha del día de hoy.");
                }
                else if (fechaConvertida > DateTime.Today)
                {
                    resultado.Valor = DateTime.Today;
                    resultado.Advertencias.Add($"Advertencia: Un valor en fecha ({fecha}) es mayor a la fecha actual. Se asignó la fecha de día de hoy.");
                }
                else if(fechaConvertida < new DateTime(2000, 1, 1))
                {
                    resultado.Valor = DateTime.Today;
                    resultado.Advertencias.Add($"Advertencia: Un valor en fecha ({fecha}) es menor al año 2000. Se asignó la fecha del día de hoy.");
                }
                else
                {
                    resultado.Valor = fechaConvertida;
                    resultado.Exitos.Add($"Fecha recibida correctamente.");
                }
            }
            else
            {
                resultado.Valor = DateTime.Now;
                resultado.Advertencias.Add($"Advertencia: Un valor en fecha ({fecha}) contiene elementos inválidos. Se asignó valor 'hoy'.");
            }
            return resultado;
        }
        public static ResultadoConversion<int> ConvertirCategoria(string categoria)
        {
            var resultado = new ResultadoConversion<int>();
            if (int.TryParse(categoria, out int categoriaConvertida))
            {
                if (categoriaConvertida <= 0)
                {
                    resultado.Valor = 1;
                    resultado.Advertencias.Add($"Advertencia: Un valor en categoría ({categoria}) es igual o menor a 0. Se asignó la categoría número 1.");
                }
                else if (categoriaConvertida > 10)
                {
                    resultado.Valor = 1;
                    resultado.Advertencias.Add($"Advertencia: Un valor en categoría ({categoria}) no entra en ninguna categoría. Se asignó la categoría número 1.");
                }
                else
                {
                    resultado.Valor = categoriaConvertida;
                    resultado.Exitos.Add($"Categoría recibida correctamente.");
                }
            }
            else
            {
                resultado.Valor = 1;
                resultado.Advertencias.Add($"Advertencia: Un valor en categoría ({categoria}) contiene elementos inválidos. Se asignó la categoría número 1");
            }
            return resultado;
        }
        public static ResultadoConversion<string> ConvertirRutaImagen(string rutaImagen)
        {
            var resultado = new ResultadoConversion<string>();
            if (string.IsNullOrWhiteSpace(rutaImagen))
            {
                resultado.Valor = string.Empty;
                resultado.Advertencias.Add("Advertencia: Ruta de imagen vacía.");
            }
            else if (rutaImagen.Length > 100)
            {
                resultado.Valor = rutaImagen.Substring(0, 100);
                resultado.Advertencias.Add($"Advertencia: Ruta de imagen '{rutaImagen}' excede el límite de 100 caracteres. Se recortó la cadena a 100 caracteres");
            }
            else
            {
                resultado.Valor = rutaImagen;
                resultado.Exitos.Add($"Ruta de imagen recibida correctamente.");
            }
            return resultado;
        }

    }
}
