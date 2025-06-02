using Aplicacion.DTOs;
using Dominio.Entidades;
using AutoMapper;
using Aplicacion.Utilidades;

namespace Aplicacion.Perfiles_de_mapeo
{
    public class ProductoProfile : Profile
    {
        public ProductoProfile()
        {
            CreateMap<Producto, ProductoResumenDTO>()
                .ReverseMap();
            CreateMap<Producto, ProductoDetalleDTO>()
                .ReverseMap();
        }
    }
}
