using AutoMapper;
using Data;
using Data.Enseignant;
using Domain.Models;

namespace ServerApp.Profiles
{
    public class EnseignantProfile : Profile
    {
        public EnseignantProfile()
        {
            CreateMap<EspEnseignant, EnseignantReadDto>();
            CreateMap<EnseignantCreateDto, EspEnseignant>();
            CreateMap<EnseignantUpdateDto, EspEnseignant>();
            CreateMap<EspEnseignant, EnseignantUpdateDto>();
        }
    }
}