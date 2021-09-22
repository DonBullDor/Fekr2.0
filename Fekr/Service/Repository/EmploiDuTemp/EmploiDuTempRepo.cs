using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Service.Repository.EmploiDuTemp
{
    public class EmploiDuTempRepo : IEmploiDuTempRepo
    {
        private readonly Oracle1Context _context;

        public EmploiDuTempRepo(Oracle1Context context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public IEnumerable<EspEmploi> GetAllEmploiDuTemp()
        {
            return _context.EspEmploi.ToList();
        }

        public EspEmploi GetEmploiDuTempByCodeModule(string codeModule)
        {
            return _context.EspEmploi.Find(codeModule);
        }

        public IEnumerable<EspEmploi> GetAllEmploiDuTempByCodeModule(string codeModule)
        {
            return _context.EspEmploi.Where(p => p.CodeModule == codeModule);
        }

        public IEnumerable<EspEmploi> GetAllEmploiDuTempByCodeModuleAndAnnee(string codeModule, string annee)
        {
            return _context.EspEmploi.Where(p => p.CodeModule == codeModule && p.AnneeDeb == annee );
        }

        public IEnumerable<EspEmploi> GetAllEmploiDuTempByCodeModuleAndAnneeAndClasse(string codeModule, string annee, string classe)
        {
            return _context.EspEmploi.Where(p => p.CodeModule == codeModule && p.AnneeDeb == annee && p.CodeCl == classe);
        }

        public IEnumerable<EspEmploi> GetAllEmploiDuTempByCodeModuleAndAnneeAndNumSeance(string codeModule, string annee, decimal numSeance)
        {
            return _context.EspEmploi.Where(p => p.CodeModule == codeModule && p.AnneeDeb == annee && p.NumSeance == numSeance);
        }

        public IEnumerable<EspEmploi> GetAllEmploiDuTempByCodeModuleAndAnneeAndJour(string codeModule, string annee, string jour)
        {
            return _context.EspEmploi.Where(p => p.CodeModule == codeModule && p.AnneeDeb == annee && p.Jour == jour);
        }

        public IEnumerable<EspEmploi> GetAllEmploiDuTempByCodeModuleAndClasse(string codeModule, string classe)
        {
            return _context.EspEmploi.Where(p => p.CodeModule == codeModule && p.CodeCl == classe);
        }

        public IEnumerable<EspEmploi> GetAllEmploiDuTempByCodeModuleAndNumSeance(string codeModule, decimal numSeance)
        {
            return _context.EspEmploi.Where(p => p.CodeModule == codeModule && p.NumSeance == numSeance);
        }

        public IEnumerable<EspEmploi> GetAllEmploiDuTempByCodeModuleAndJour(string codeModule, string jour)
        {
            return _context.EspEmploi.Where(p => p.CodeModule == codeModule && p.Jour == jour);
        }

        public IEnumerable<EspEmploi> GetAllEmploiDuTempByCodeModuleAndTypeSeance(string codeModule, string typeSeance)
        {
            return _context.EspEmploi.Where(p => p.CodeModule == codeModule && p.TypeSeance == typeSeance);
        }

        public IEnumerable<EspEmploi> GetAllEmploiDuTempByClasse(string classe)
        {
            return _context.EspEmploi.Where(p => p.CodeCl == classe);
        }

        public IEnumerable<EspEmploi> GetAllEmploiDuTempByClasseAndAnnee(string classe, string annee)
        {
            return _context.EspEmploi.Where(p => p.CodeCl == classe && p.AnneeDeb == annee);
        }

        public IEnumerable<EspEmploi> GetAllEmploiDuTempByClasseAndSemestre(string classe, decimal semestre)
        {
            return _context.EspEmploi.Where(p => p.CodeCl == classe && p.Semestre == semestre);
        }

        public IEnumerable<EspEmploi> GetAllEmploiDuTempByClasseAndNumSeance(string classe, decimal numSeance)
        {
            return _context.EspEmploi.Where(p => p.CodeCl == classe && p.NumSeance == numSeance);
        }

        public IEnumerable<EspEmploi> GetAllEmploiDuTempByClasseAndJour(string classe, string jour)
        {
            return _context.EspEmploi.Where(p => p.CodeCl == classe && p.Jour == jour);
        }

        public IEnumerable<EspEmploi> GetAllEmploiDuTempByClasseAndTypeSeance(string classe, string typeSeance)
        {
            return _context.EspEmploi.Where(p => p.CodeCl == classe && p.TypeSeance == typeSeance);
        }

        public IEnumerable<EspEmploi> GetAllEmploiDuTempByAnnee(string codeModule)
        {
            return _context.EspEmploi.Where(p => p.CodeModule == codeModule);
        }

        public IEnumerable<EspEmploi> GetAllEmploiDuTempByAnneeAndNumSeance(string codeModule, decimal numSeance)
        {
            return _context.EspEmploi.Where(p => p.CodeModule == codeModule && p.NumSeance == numSeance);
        }

        public IEnumerable<EspEmploi> GetAllEmploiDuTempByAnneeAndSemestre(string codeModule, decimal semestre)
        {
            return _context.EspEmploi.Where(p => p.CodeModule == codeModule && p.Semestre == semestre);
        }

        public IEnumerable<EspEmploi> GetAllEmploiDuTempBySemestre(decimal semestre)
        {
            return _context.EspEmploi.Where(p => p.Semestre == semestre);
        }

        public IEnumerable<EspEmploi> GetAllEmploiDuTempBySemestreAndJour(decimal semestre, string jour)
        {
            return _context.EspEmploi.Where(p => p.Semestre == semestre && p.Jour == jour);
        }

        /*
        public IEnumerable<EspEmploi> GetAllEmploiByCritere(string[] criteres)
        {
            object[] criteria = null;
            IEnumerable<EspEmploi> listEmploiDuTemp = new List<EspEmploi>();
            try
            {
                listEmploiDuTemp = _context.EspEmploi.FromSqlRaw(
                    $"Select * from ESP_EMPLOI " +
                    " WHERE " +
                    ((string) criteria[0] != "" ? " CODE_MODULE = {0}" : "") +
                    ((string) criteria[1] != "" ? " AND ANNEE_DEB = {1}" : "") +
                    ((decimal) criteria[2] != "" ? " AND SEMESTRE = {2}" : "") +
                    (criteria[3] != "" ? " AND CODE_CL = {3}" : "") +
                    (criteria[4] != "" ? " AND NUM_SEANCE = {4}" : "") +
                    (criteria[5] != "" ? " AND JOUR = {5}" : "") +
                    (criteria[6] != "" ? " AND TYPE_SEANCE  = {6}" : "")
                    , criteria[0]
                    , criteria[1]
                    , criteria[2]
                    , criteria[3]
                    , criteria[4]
                    , criteria[5]
                    , criteria[6]
                ).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return listEmploiDuTemp;
        }
        */
        public void UpdateEmploiDuTemp(EspEmploi idEtudiant)
        {
        }

        public void CreateEmploiDuTemp(EspEmploi emploi)
        {
            if (emploi == null)
            {
                throw new ArgumentNullException(nameof(emploi));
            }

            _context.EspEmploi.Add(emploi);
        }

        public void DeleteEmploiDuTemp(EspEmploi emploi)
        {
            if (emploi == null)
            {
                throw new ArgumentNullException(nameof(emploi));
            }

            _context.EspEmploi.Remove(emploi);
        }
    }
}