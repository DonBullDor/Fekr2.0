using Data;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Repository.Notes
{
    public class NotesApiRepo : INotesApiRepo
    {
        private readonly Oracle1Context _context;

        public NotesApiRepo(Oracle1Context context)
        {
            _context = context;
        }
        public void CreateNotes(ANote idEtudiant)
        {
            if (idEtudiant == null)
            {
                throw new ArgumentNullException(nameof(idEtudiant));
            }
            _context.ANote.Add(idEtudiant);
        }

        public void DeleteNotes(ANote idEtudiant)
        {
            if (idEtudiant == null)
            {
                throw new ArgumentNullException(nameof(idEtudiant));
            }
            _context.ANote.Remove(idEtudiant);
        }

        public IEnumerable<ANote> GetAllNotes()
        {
            return _context.ANote.ToList();
        }

        public ANote GetNotesById(string idEtudiant)
        {
            return _context.ANote.FirstOrDefault(p => p.IdEt == idEtudiant);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateNotes(ANote idEtudiant)
        {
        }
    }
}
