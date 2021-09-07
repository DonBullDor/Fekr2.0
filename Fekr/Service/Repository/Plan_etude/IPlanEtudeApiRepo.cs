using Data;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Repository.Plan_etude
{
    public interface IPlanEtudeApiRepo
    {
        bool SaveChanges();
        IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtude();

        EspModulePanierClasseSaiso GetPlanEtudeById(string codeClasse);
        IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudeByClasse(string classe);
        IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudeByModule(string codeModule);
        IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudeByEnseignant(string idEnseignant);
        IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudeByEtudiant(string idEtudiant);

        void UpdatePlanEtude(EspModulePanierClasseSaiso planEtude);

        void CreatePlanEtude(EspModulePanierClasseSaiso planEtude);

        void DeletePlanEtude(EspModulePanierClasseSaiso planEtude);
    }
}