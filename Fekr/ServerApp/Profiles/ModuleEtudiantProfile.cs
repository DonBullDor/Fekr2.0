using Data.ModuleEtudiant;
using Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApp.Profiles
{
    public class ModuleEtudiantProfile : Profile
    {
        public ModuleEtudiantProfile()
        {
            CreateMap<EspModuleEtudiant, ModuleEtudiantReadDto>();
            CreateMap<ModuleEtudiantCreateDto, EspModuleEtudiant>();
            CreateMap<ModuleEtudiantUpdateDto, EspModuleEtudiant>();
            CreateMap<EspModuleEtudiant, ModuleEtudiantUpdateDto>();
        }
    }
}
