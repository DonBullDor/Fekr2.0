using AutoMapper;
using Data.PlanEtude;
using Domain.Models;

namespace ServerApp.Profiles
{
    public class PlanEtudeProfile : Profile
    {
        public PlanEtudeProfile()
        {
            CreateMap<EspModulePanierClasseSaiso, PlanEtudeReadDto>();
            CreateMap<PlanEtudeCreateDto, EspModulePanierClasseSaiso>();
            CreateMap<PlanEtudeUpdateDto, EspModulePanierClasseSaiso>();
            CreateMap<EspModulePanierClasseSaiso, PlanEtudeUpdateDto>();
        }
    }
}