using Data;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Repository.Enseignant
{
    public class EnseignantApiRepo : IEnseignantApiRepo
    {
        private readonly Oracle1Context _context;
        public EnseignantApiRepo(Oracle1Context context)
        {
            _context = context;
        }
        public void DeleteEnseignant(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EspEnseignant> GetAllEnseignants()
        {
            return _context.EspEnseignant.ToList();
        }

        public EspEnseignant GetEnseignant(string id)
        {
            return _context.EspEnseignant.FirstOrDefault(p => p.IdEns == id);
        }

        public void CreateEnseignant(EspEnseignant enseignant)
        {
            if (enseignant == null)
            {
                throw new ArgumentNullException(nameof(enseignant));
            }
            _context.EspEnseignant.Add(enseignant);
        }

        public void UpdateEnseignant(string id, EspEnseignant enseignant)
        {
            throw new NotImplementedException();
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
