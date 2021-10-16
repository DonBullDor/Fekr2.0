using Data;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Service.Repository.Plan_etude
{
    public class PlanEtudeApiRepo : IPlanEtudeApiRepo
    {
        private readonly Oracle1Context _context;

        public PlanEtudeApiRepo(Oracle1Context context)
        {
            _context = context;
        }

        public void CreatePlanEtude(EspModulePanierClasseSaiso planEtude)
        {
            if (planEtude == null)
            {
                throw new ArgumentNullException(nameof(planEtude));
            }

            _context.EspModulePanierClasseSaiso.Add(planEtude);
        }

        public void DeletePlanEtude(EspModulePanierClasseSaiso planEtude)
        {
            if (planEtude == null)
            {
                throw new ArgumentNullException(nameof(planEtude));
            }

            _context.EspModulePanierClasseSaiso.Remove(planEtude);
        }

        public int GetNumberOfClasses(string annee)
        {
            var query = _context.EspModulePanierClasseSaiso
                .Where(a => a.AnneeDeb == annee)
                .Select(p => p.CodeCl).Distinct().ToArray().Length;
            return query;
        }

        public int GetNumberOfModulesOfClassByYear(string classe, string annee)
        {
            var query = _context.EspModulePanierClasseSaiso
                .Where(p => p.AnneeDeb == annee && p.CodeCl == classe)
                .Select(a => a.CodeModule).ToArray().Length;

            return query;
        }

        public string[] GetModulesByClasseAndYear(string classe, string annee)
        {
            var query = _context.EspModulePanierClasseSaiso
                .Where(a => a.AnneeDeb == annee && a.CodeCl == classe)
                .Select(m=>m.CodeModule)
                .ToArray();

            return query;
        }

        public string[] GetListOfAllClassesByYear(string annee)
        {
            var query = _context.EspModulePanierClasseSaiso
                .Where(a => a.AnneeDeb == annee)
                .Select(p => p.CodeCl).ToArray();

            return query;
        }

        public string[] GetListOfEnseignantByModule(string annee, string module)
        {
            var query = _context.EspModulePanierClasseSaiso
                .Where(a => a.AnneeDeb == annee && a.CodeModule == module && a.IdEns != null)
                .Select(p => p.IdEns).Distinct().ToArray();

            return query;
        }

        public decimal GetNbHeureParModuleParClasse(string annee, string module, string classe)
        {
            try
            {
                var query = _context.EspModulePanierClasseSaiso
                    .Where(q => q.AnneeDeb == annee && q.CodeCl == classe && q.CodeModule == module)
                    .Select(a => a.NbHeures).FirstOrDefault();

                return query != null ? query.Value : 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }

        public IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtude()
        {
            return _context.EspModulePanierClasseSaiso.ToList();
        }

        public EspModulePanierClasseSaiso GetPlanEtudeById
            (string codeClasse, string codeModule, string annee, decimal numSemestre)
        {
            //return _context.EspModulePanierClasseSaiso.FirstOrDefault(p => p.CodeModule == planEtude);
            return _context.EspModulePanierClasseSaiso.FirstOrDefault(p =>
                p.CodeModule == codeModule &&
                p.CodeCl == codeClasse &&
                p.AnneeDeb == annee &&
                p.NumSemestre == numSemestre
            );
        }

        public IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudeByClasse(string classe)
        {
            return _context.EspModulePanierClasseSaiso.Where(p => p.CodeCl == classe).ToList();
        }

        public IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudeByClasseAndAnnee(string classe, string annee)
        {
            return _context.EspModulePanierClasseSaiso.Where(p => p.CodeCl == classe && p.AnneeDeb == annee).ToList();
        }

        public IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudeByClasseAndAnneeAndSemestre(string classe,
            string annee, decimal semestre)
        {
            return _context.EspModulePanierClasseSaiso.Where(p =>
                    p.CodeCl == classe && p.AnneeDeb == annee && p.NumSemestre == semestre)
                .ToList();
        }

        public IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudeByModule(string codeModule)
        {
            return _context.EspModulePanierClasseSaiso.Where(p => p.CodeModule == codeModule);
        }

        public IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudeByCritere(string[] critere)
        {
            string[] criteria = null;
            IEnumerable<EspModulePanierClasseSaiso> listPlanEtude = new List<EspModulePanierClasseSaiso>();
            /*
             * "codeModule": "KD-07",
             "codeCl": "7 B 2",
             "anneeDeb": "2017",
             "idEns": "S-32-16",
             "numSemestre": 1.0,
             */
            try
            {
                //string[] criteria = {"S-62-17","","2017",""};


                listPlanEtude = _context.EspModulePanierClasseSaiso.FromSqlRaw(
                    $"Select * from ESP_MODULE_PANIER_CLASSE_SAISO " +
                    " where " +
                    (criteria[0] != "" ? " ID_ENS = {0}" : "") +
                    (criteria[1] != "" ? " AND CODE_CL = {1}" : "") +
                    (criteria[2] != "" ? " AND ANNEE_DEB = {2}" : "") +
                    (criteria[3] != "" ? " AND CODE_MODULE = {3}" : "")
                    , criteria[0]
                    , criteria[1]
                    , criteria[2]
                    , criteria[3]
                ).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return listPlanEtude;
        }

        public IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudeByEnseignant(string idEnseignant)
        {
            return _context.EspModulePanierClasseSaiso.Where(p => p.IdEns == idEnseignant);
        }

        public IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudeByAnnee(string year)
        {
            return _context.EspModulePanierClasseSaiso.Where(p => p.AnneeDeb == year).ToList();
        }

        public IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudeByClasseAndEnseignantAndAnneeAndModule(
            string classe, string idEnseignant, string annee,
            string module)
        {
            return _context.EspModulePanierClasseSaiso.Where(p =>
                    p.CodeCl == classe && p.IdEns == idEnseignant && p.AnneeDeb == annee && p.CodeModule == module)
                .ToList();
        }

        public IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudesByClasseAndModule(string classe, string module)
        {
            return _context.EspModulePanierClasseSaiso
                .Where(p => p.CodeCl == classe && p.CodeModule == module)
                .ToList();
        }

        public IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudesByClasseAndModuleAndAnnee(string classe,
            string module, string annee)
        {
            return _context.EspModulePanierClasseSaiso.Where(p =>
                p.CodeModule == module && p.AnneeDeb == annee && p.CodeCl == classe);
        }

        public IEnumerable<EspModulePanierClasseSaiso> GetPlanEtudeByEnseignantAndModule(string idEnseignant,
            string module)
        {
            return _context.EspModulePanierClasseSaiso
                .Where(p => p.CodeCl == idEnseignant && p.CodeModule == module)
                .ToList();
        }

        public IEnumerable<EspModulePanierClasseSaiso> GetPlanEtudeByEnseignantAndModuleAndAnnee(string idEnseignant,
            string module, string annee)
        {
            return _context.EspModulePanierClasseSaiso
                .Where(p => p.IdEns == idEnseignant && p.CodeModule == module && p.AnneeDeb == annee).ToList();
        }

        public IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudeByClasseAndEnseignant(string classe,
            string idEnseignant)
        {
            return _context.EspModulePanierClasseSaiso
                .Where(p => p.CodeCl == classe && p.IdEns == idEnseignant)
                .ToList();
        }

        public IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudeByClasseAndEnseignantAndAnnee(string classe,
            string idEnseignant, string annee)
        {
            return _context.EspModulePanierClasseSaiso.Where(p => p.CodeCl == classe && p.IdEns == idEnseignant)
                .ToList();
        }

        public IEnumerable<EspModulePanierClasseSaiso> GetPlanEtudeByClasseAndAnne(string classe, string year)
        {
            return _context.EspModulePanierClasseSaiso
                .Where(p => p.CodeCl == classe && p.AnneeDeb == year)
                .ToList();
        }

        public IEnumerable<EspModulePanierClasseSaiso> GetPlanEtudeByEnseignantAndAnnee(string idEnseignant,
            string year)
        {
            return _context.EspModulePanierClasseSaiso
                .Where(p => p.IdEns == idEnseignant && p.AnneeDeb == year)
                .ToList();
        }

        public IEnumerable<EspModulePanierClasseSaiso> GetPlanEtudeByAnneeAndModule(string year, string module)
        {
            return _context.EspModulePanierClasseSaiso
                .Where(p => p.CodeModule == module && p.AnneeDeb == year)
                .ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdatePlanEtude(EspModulePanierClasseSaiso planEtude)
        {
        }
    }
}