using AutoMapper;
using Data.EmploiDuTemp;
using Domain.Models;

namespace ServerApp.Profiles
{
    public class EmploiDuTempProfiler : Profile
    {
        public EmploiDuTempProfiler()
        {
            CreateMap<EspEmploi, EmploiDuTempReadDto>();
            CreateMap<EmploiDuTempCreateDto, EspEmploi>();
            CreateMap<EmploiDuTempUpdateDto, EspEmploi>();
            CreateMap<EspEmploi, EmploiDuTempUpdateDto>();  
        }
    }
}