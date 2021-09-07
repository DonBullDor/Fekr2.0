using Data;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtude()
        {
            return _context.EspModulePanierClasseSaiso.ToList();
        }

        public EspModulePanierClasseSaiso GetPlanEtudeById(string codeClasse)
        {
            //return _context.EspModulePanierClasseSaiso.FirstOrDefault(p => p.CodeModule == planEtude);
            return _context.EspModulePanierClasseSaiso.Find(codeClasse);
        }

        public IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudeByClasse(string classe)
        {
            return _context.EspModulePanierClasseSaiso.Where(p => p.CodeCl == classe).Take(20);
        }

        public IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudeByModule(string codeModule)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudeByEnseignant(string idEnseignant)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudeByEtudiant(string idEtudiant)
        {
            throw new NotImplementedException();
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