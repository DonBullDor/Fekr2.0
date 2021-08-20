using Data;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Repository.Moyenne
{
    public class MoyenneApiRepo : IMoyenneApiRepo
    {
        private readonly Oracle1Context _context;

        public MoyenneApiRepo(Oracle1Context context)
        {
            _context = context;
        }
        public void CreateMoyenne(AMoyenne idEtudiant)
        {
            if (idEtudiant == null)
            {
                throw new ArgumentNullException(nameof(idEtudiant));
            }
            _context.AMoyenne.Add(idEtudiant);
        }

        public void DeleteMoyenne(AMoyenne idEtudiant)
        {
            if (idEtudiant == null)
            {
                throw new ArgumentNullException(nameof(idEtudiant));
            }
            _context.AMoyenne.Remove(idEtudiant);
        }

        public IEnumerable<AMoyenne> GetAllMoyenne()
        {
            return _context.AMoyenne.ToList();
        }

        public AMoyenne GetMoyenneById(string idEtudiant)
        {
            return _context.AMoyenne.FirstOrDefault(p => p.IdEt == idEtudiant);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateMoyenne(AMoyenne idEtudiant)
        {
        }
    }
}
