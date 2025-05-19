using CsvHelper;
using Aplicacion.DTOs;
using FluentValidation;
using AutoMapper;
using Dominio.Interfaces;
using Dominio.Entidades;
using CsvHelper.TypeConversion;
using CsvHelper.Configuration;
using System.Globalization;
using Aplicacion.Utilidades;
using Microsoft.Win32;

namespace Aplicacion.Servicios
{
    public class ImportarCsvService
    {
        private readonly IMapper _mapper;
        private readonly IValidator<ProductoDetalleDTO> _productoValidator;
        private readonly IProductoRepository _productoRepository;
        public ImportarCsvService(IMapper mapper, IValidator<ProductoDetalleDTO> productoValidator, IProductoRepository productoRepository)
        {
            _productoValidator = productoValidator;
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        public async Task<ResultadoImportacionCsvDTO> LeerCsv(Stream stream)
        {
            //Variables para almacenar resultados y errores
            var resultados = new List<ProductoImportadoDTO>();
            var erroresGlobales = new List<string>();

            //Contador de filas para errores
            int filaActual = 1;
            //Comparación de IDs y nombres existentes
            var idsEncontrados = new HashSet<int>();
            var nombresEncontrados = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            //Validación global, si un dato es corrupto se agrega a erroresGlobales
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null,
                BadDataFound = context =>
                {
                    erroresGlobales.Add($"[Fila {filaActual}] Dato corrupto: {context.Field}");
                }
            };

            //Configuración de CsvHelper
            using var reader = new StreamReader(stream);
            using var csv = new CsvReader(reader, config);

            try
            {
                // Leer el encabezado y avanzar a la siguiente fila
                await csv.ReadAsync();
                csv.ReadHeader();
                filaActual++;

                // Leer cada fila del CSV
                while (await csv.ReadAsync())
                {
                    var resultado = new ProductoImportadoDTO();
                    

                    try
                    {
                        // Leer el registro y mapearlo a ProductoCsvDTO
                        var registro = csv.GetRecord<ProductoCsvDTO>();

                        // Pasar el atributo a Conversión
                        var conversionId = ConversionCsvHelper.ConvertirID(registro.ProductoID, idsEncontrados);
                        var conversionNombre = ConversionCsvHelper.ConvertirNombre(registro.Nombre, nombresEncontrados);
                        var descripcion = ConversionCsvHelper.ConvertirDescripcion(registro.Descripcion);
                        var cantidad = ConversionCsvHelper.ConvertirCantidad(registro.Cantidad);
                        var precio = ConversionCsvHelper.ConvertirPrecio(registro.Precio);
                        var fecha = ConversionCsvHelper.ConvertirFechaCreacion(registro.FechaCreacion);
                        var categoria = ConversionCsvHelper.ConvertirCategoria(registro.Categoria);
                        var rutaImagen = ConversionCsvHelper.ConvertirRutaImagen(registro.RutaImagen);


                        //Agregar éxitos
                        resultado.Exitos.AddRange(conversionId.Exitos);
                        resultado.Exitos.AddRange(conversionNombre.Exitos);
                        resultado.Exitos.AddRange(descripcion.Exitos);
                        resultado.Exitos.AddRange(cantidad.Exitos);
                        resultado.Exitos.AddRange(precio.Exitos);
                        resultado.Exitos.AddRange(fecha.Exitos);
                        resultado.Exitos.AddRange(categoria.Exitos);
                        resultado.Exitos.AddRange(rutaImagen.Exitos);
                        //Agregar advertencias
                        resultado.Advertencias.AddRange(conversionId.Advertencias);
                        resultado.Advertencias.AddRange(conversionNombre.Advertencias);
                        resultado.Advertencias.AddRange(descripcion.Advertencias);
                        resultado.Advertencias.AddRange(cantidad.Advertencias);
                        resultado.Advertencias.AddRange(precio.Advertencias);
                        resultado.Advertencias.AddRange(fecha.Advertencias);
                        resultado.Advertencias.AddRange(categoria.Advertencias);
                        resultado.Advertencias.AddRange(rutaImagen.Advertencias);
                        //Agregar errores
                        resultado.Errores.AddRange(conversionId.Errores);
                        resultado.Errores.AddRange(conversionNombre.Errores);
                        resultado.Errores.AddRange(descripcion.Errores);
                        resultado.Errores.AddRange(cantidad.Errores);
                        resultado.Errores.AddRange(precio.Errores);
                        resultado.Errores.AddRange(fecha.Errores);
                        resultado.Errores.AddRange(categoria.Errores);
                        resultado.Errores.AddRange(rutaImagen.Errores);

                        // Verificar si el producto es válido
                        if (resultado.Errores.Any())
                        {
                            resultado.Errores = resultado.Errores
                                .Select(e => $"[Fila {filaActual}] {e}")
                                .ToList();
                        }
                        else
                        {
                            //Agregar el producto al DTO con valores convertidos
                            var productoDto = new ProductoDetalleDTO
                            {
                                ProductoID = conversionId.Valor,
                                Nombre = conversionNombre.Valor,
                                Descripcion = descripcion.Valor,
                                Cantidad = cantidad.Valor,
                                Precio = precio.Valor,
                                FechaCreacion = fecha.Valor,
                                Categoria = categoria.Valor,
                                RutaImagen = rutaImagen.Valor
                            };
                            // Validar contra la BD
                            var yaExiste = await _productoRepository.ExistenciaPorIdONombre(productoDto.ProductoID, productoDto.Nombre);
                            if (yaExiste)
                            {
                                resultado.Errores.Add($"[Fila {filaActual}] Ya existe un producto con el mismo nombre o ID en la base de datos.");
                            }
                            else
                            {
                                //Finalizar con validación de FluentValidation
                                var validationResult = await _productoValidator.ValidateAsync(productoDto);
                                if (validationResult.IsValid)
                                {
                                    resultado.Producto = productoDto;
                                    resultado.Exitos.Add($"[Fila {filaActual}] Producto válido: {productoDto.Nombre}");
                                    resultado.Agregado = true;
                                }
                                else
                                {
                                    resultado.Errores.AddRange(validationResult.Errors.Select(e =>
                                        $"[Fila {filaActual}] {e.PropertyName}: {e.ErrorMessage}"));
                                }
                            } 
                        }
                    }
                    catch (TypeConverterException ex)
                    {
                        resultado.Errores.Add($"[Fila {filaActual}] Error de tipo: {ex.Text}");
                    }
                    catch (Exception ex)
                    {
                        resultado.Errores.Add($"[Fila {filaActual}] Error inesperado: {ex.Message}");
                    }

                    resultados.Add(resultado);
                    filaActual++;
                }
            }
            catch (Exception ex)
            {
                erroresGlobales.Add($"[Error global] {ex.Message}");
            }
            var resultadoFinal = new ResultadoImportacionCsvDTO
            {
                ProductosImportados = resultados,
                ErroresGlobales = erroresGlobales
            };

            return resultadoFinal;
        }

        public async Task GuardarProductos(List<ProductoDetalleDTO> productosDto)
        {
            var productos = productosDto.Select(dto => _mapper.Map<Producto>(dto)).ToList();
            await _productoRepository.AgregarRangoAsync(productos);
        }
    }
}
