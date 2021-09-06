using AutoMapper;
using Data.Admins;
using Domain.Models;

namespace ServerApp.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<Decid, AdminReadDto>();
            CreateMap<AdminCreateDto, Decid>();
            CreateMap<AdminUpdateDto, Decid>();
            CreateMap<Decid, AdminUpdateDto>();
        }
    }
}