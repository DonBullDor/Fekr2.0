using Domain.Models;
using System.Collections.Generic;

namespace Service.Repository.Notes
{
    public interface INotesApiRepo
    {
        bool SaveChanges();
        IEnumerable<ANote> GetAllNotes();

        ANote GetNotesById(string idEtudiant);

        void UpdateNotes(ANote idEtudiant);

        void CreateNotes(ANote idEtudiant);

        void DeleteNotes(ANote idEtudiant);
    }
}
