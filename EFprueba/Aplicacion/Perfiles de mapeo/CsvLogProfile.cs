using Aplicacion.DTOs;
using AutoMapper;
using Dominio.Entidades;

namespace Aplicacion.Perfiles_de_mapeo
{
    public class CsvLogProfile : Profile
    {
        public CsvLogProfile() 
        {
            CreateMap<CsvLog, CsvLogDTO>()
                .ForMember(des => des.Exitos, opt => opt.MapFrom(src => src.ProductosImportados))
                .ForMember(des => des.Advertencias, opt => opt.MapFrom(src => src.ProductosConAdvertencias))
                .ForMember(des => des.Errores, opt => opt.MapFrom(src => src.ProductosConErrores));
            CreateMap<CsvLogDTO, CsvLog>()
                .ForMember(dest => dest.ProductosImportados, opt => opt.MapFrom(src => src.Exitos))
                .ForMember(dest => dest.ProductosConAdvertencias, opt => opt.MapFrom(src => src.Advertencias))
                .ForMember(dest => dest.ProductosConErrores, opt => opt.MapFrom(src => src.Errores));
        }
    }
}
