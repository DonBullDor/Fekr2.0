using AutoMapper;
using Data;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApp.Profiles
{
    public class SocieteProfile : Profile
    {
        public SocieteProfile()
        {
            CreateMap<Societe, SocieteDto>();
        }
    }
}
