using AutoMapper;
using Data.Moyenne;
using Domain.Models;

namespace ServerApp.Profiles
{
    public class MoyenneProfile : Profile
    {
        public MoyenneProfile()
        {
            CreateMap<AMoyenne, MoyenneReadDto>();
            CreateMap<MoyenneCreateDto, AMoyenne>();
            CreateMap<MoyenneUpdateDto, AMoyenne>();
            CreateMap<AMoyenne, MoyenneUpdateDto>();
        }
    }
}