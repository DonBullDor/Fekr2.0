using AutoMapper;
using Data;
using Data.Societe;
using Domain.Models;

namespace ServerApp.Profiles
{
    public class SocieteProfile : Profile
    {
        public SocieteProfile()
        {
            CreateMap<Societe, SocieteReadDto>();
            CreateMap<SocieteCreateDto, Societe>();
            CreateMap<SocieteUpdateDto, Societe>();
            CreateMap<Societe, SocieteUpdateDto>();
        }
    }
}