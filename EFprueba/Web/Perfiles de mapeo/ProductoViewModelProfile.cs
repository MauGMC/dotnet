using Aplicacion.DTOs;
using AutoMapper;
using Dominio.Entidades;
using Web.ViewModels;
namespace Web.Perfiles_de_mapeo
{
    public class ProductoViewModelProfile : Profile
    {
        public ProductoViewModelProfile()
        {
            CreateMap<Producto, ProductoDetalleViewModel>()
                .ReverseMap();
            CreateMap<ProductoDetalleDTO, ProductoDetalleViewModel>()
                .ReverseMap();
            CreateMap<ProductoResumenDTO, ProductoResumenViewModel>()
                .ReverseMap();
        }
    }
}
