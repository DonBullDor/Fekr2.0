using AutoMapper;
using Data;
using Data.Module;
using Domain.Models;

namespace ServerApp.Profiles
{
    public class ModuleProfile : Profile
    {
        public ModuleProfile()
        {
            CreateMap<EspModule, ModuleReadDto>();
            CreateMap<ModuleCreateDto, EspModule>();
        }
    }
}