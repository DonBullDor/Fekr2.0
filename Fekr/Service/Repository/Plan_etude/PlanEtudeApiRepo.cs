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

        public EspModulePanierClasseSaiso GetPlanEtudeById(string planEtude)
        {
            return _context.EspModulePanierClasseSaiso.FirstOrDefault(p => p.CodeModule == planEtude);
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
