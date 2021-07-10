using AutoMapper;
using Data;
using Data.Decids;
using Domain.Models;

namespace ServerApp.Profiles
{
    public class DecidProfile : Profile
    {
        public DecidProfile()
        {
            CreateMap<Decid, DecidReadDto>();
            CreateMap<DecidCreateDto, Decid>();
        }
    }
}