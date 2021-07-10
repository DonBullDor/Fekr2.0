using AutoMapper;
using Data;
using Data.Societes;
using Domain.Models;

namespace ServerApp.Profiles
{
    public class SocieteProfile : Profile
    {
        public SocieteProfile()
        {
            CreateMap<Societe, SocieteReadDto>();
            CreateMap<SocieteCreateDto, Societe>();
        }
    }
}