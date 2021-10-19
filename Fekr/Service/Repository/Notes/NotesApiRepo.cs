using Data;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

        public IEnumerable<ANote> GetNotesByClasseAndEnseignantAndAnneDebAndNumSemestre
        (string classe, string enseignant, string anneeDeb, decimal numSemestre)
        {
            return _context.ANote.Where(p =>
                p.CodeCl == classe &&
                p.IdEns == enseignant &&
                p.AnneeDeb == anneeDeb &&
                p.Semestre == numSemestre).ToList();
        }

        public IEnumerable<ANote> RechercheNotes(string[] criteria)
        {
            //string[] criteria = null;
            IEnumerable<ANote> listeDesNotes = new List<ANote>();
            try
            {
                listeDesNotes = _context.ANote.FromSqlRaw(
                    $"Select * from A_NOTE " +
                    " WHERE " +
                    (criteria[0] != "" ? " ID_ET = {0}" : "") +
                    (criteria[1] != "" ? " ID_ENS = {1}": "") +
                    (criteria[2] != "" ? " CODE_CL = {2}": "") +
                    (criteria[3] != "" ? " ANNEE_DEB = {3}": "") +
                    (criteria[4] != "" ? " CODE_MODULE = {4}": "") +
                    (criteria[5] != "" ? " SEMESTRE = {5}": "")
                    , criteria[0]
                    , criteria[1]
                    , criteria[2]
                    , criteria[3]
                    , criteria[4]
                    , criteria[5]
                ).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return listeDesNotes;
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
