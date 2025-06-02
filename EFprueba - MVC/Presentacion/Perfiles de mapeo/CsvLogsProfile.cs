using Aplicacion.DTOs;
using AutoMapper;
using Presentacion.Models;

namespace Presentacion.Perfiles_de_mapeo
{
    public class CsvLogsProfile : Profile
    {
        public CsvLogsProfile() 
        {
            CreateMap<CsvLogDTO, CsvLogViewModel>();
        }
    }
}
