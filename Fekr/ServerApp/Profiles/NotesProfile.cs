using AutoMapper;
using Data.Notes;
using Domain.Models;

namespace ServerApp.Profiles
{
    public class NotesProfile : Profile
    {
        public NotesProfile()
        {
            CreateMap<ANote, NoteReadDto>();
            CreateMap<NoteCreateDto, ANote>();
            CreateMap<NoteUpdateDto, ANote>();
            CreateMap<ANote, NoteUpdateDto>();
        }
    }
}