using AutoMapper;
using Data;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApp.Profiles
{
    public class EnseignantProfile : Profile
    {
        public EnseignantProfile()
        {
            CreateMap<EspEnseignant, EnseignantDto>();
        }
    }
}
