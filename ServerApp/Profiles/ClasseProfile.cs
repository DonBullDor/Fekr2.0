using AutoMapper;
using Data;
using Data.Classes;
using Domain.Models;

namespace ServerApp.Profiles
{
    public class ClasseProfile : Profile
    {
        public ClasseProfile()
        {
            CreateMap<Classe, ClasseReadDto>();
            CreateMap<ClasseCreateDto, Classe>();
            CreateMap<ClasseUpdateDto, Classe>();
        }
    }
}