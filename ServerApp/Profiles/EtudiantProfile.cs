using AutoMapper;
using Data;
using Data.Etudiant;
using Domain.Models;

namespace ServerApp.Profiles
{
    public class EtudiantProfile : Profile
    {
        public EtudiantProfile()
        {
            CreateMap<EspEtudiant, EtudiantReadDto>();
            CreateMap<EtudiantCreateDto, EspEtudiant>();
        }
    }
}