using Aplicacion.DTOs;
using AutoMapper;
using Web.ViewModels;

namespace Web.Perfiles_de_mapeo
{
    public class CsvLogsProfile : Profile
    {
        public CsvLogsProfile() 
        {
            CreateMap<CsvLogDTO, CsvLogViewModel>();
        }
    }
}
