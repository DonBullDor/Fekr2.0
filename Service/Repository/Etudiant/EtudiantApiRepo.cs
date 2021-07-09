using Data;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Repository.Etudiant
{
    public class EtudiantApiRepo : IEtudiantApiRepo
    {
        private readonly Oracle1Context _context;

        public EtudiantApiRepo(Oracle1Context context)
        {
            _context = context;
        }

        public IEnumerable<EspEtudiant> GetAllEspEtudiant()
        {
            return _context.EspEtudiant.ToList();
        }

        public EspEtudiant GetEspEtudiant(string id)
        {
            var espEtudiant = _context.EspEtudiant.Find(id);

            if (espEtudiant == null)
            {
                return null;
            }

            return espEtudiant;
        }

        public void PostEspEtudiant(EspEtudiant espEtudiant)
        {
            _context.EspEtudiant.Add(espEtudiant);
            _context.SaveChanges();
        }

        private bool EspEtudiantExists(string id)
        {
            return _context.EspEtudiant.Any(e => e.IdEt == id);
        }

        public void PutEspEtudiant(string id, EspEtudiant espEtudiant)
        {
            throw new NotImplementedException();
        }

        public void DeleteEspEtudiant(string id)
        {
            _context.EspEtudiant.Remove(new EspEtudiant() { IdEt = id });
            _context.SaveChanges();
        }
    }
}