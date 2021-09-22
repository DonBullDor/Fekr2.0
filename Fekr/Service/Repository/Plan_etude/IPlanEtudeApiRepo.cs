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

        EspModulePanierClasseSaiso GetPlanEtudeById(string classe);
        IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudeByClasse(string classe);

        IEnumerable<EspModulePanierClasseSaiso>
            GetAllPlanEtudeByClasseAndEnseignant(string classe, string idEnseignant);

        IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudeByClasseAndAnnee(string classe, string annee);
        IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudeByClasseAndEnseignantAndAnnee(string classe,
            string idEnseignant, string annee);
        
        IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudeByClasseAndAnneeAndSemestre(string classe, string annee, decimal semestre);
        IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudeByClasseAndEnseignantAndAnneeAndModule(string classe,
            string idEnseignant, string annee, string module);

        IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudesByClasseAndModule(string classe, string module);

        IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudesByClasseAndModuleAndAnnee(string classe, string module,
            string annee);

        IEnumerable<EspModulePanierClasseSaiso> GetPlanEtudeByClasseAndAnne(string classe, string annee);
        IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudeByModule(string codeModule);
        IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudeByCritere(string[] criteres);
        IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudeByEnseignant(string idEnseignant);
        IEnumerable<EspModulePanierClasseSaiso> GetAllPlanEtudeByAnnee(string annee);
        IEnumerable<EspModulePanierClasseSaiso> GetPlanEtudeByEnseignantAndAnnee(string idEnseignant, string year);
        IEnumerable<EspModulePanierClasseSaiso> GetPlanEtudeByEnseignantAndModule(string idEnseignant, string module);

        IEnumerable<EspModulePanierClasseSaiso> GetPlanEtudeByEnseignantAndModuleAndAnnee(string idEnseignant,
            string module, string annee);

        IEnumerable<EspModulePanierClasseSaiso> GetPlanEtudeByAnneeAndModule(string annee, string module);
        void UpdatePlanEtude(EspModulePanierClasseSaiso planEtude);
        void CreatePlanEtude(EspModulePanierClasseSaiso planEtude);
        void DeletePlanEtude(EspModulePanierClasseSaiso planEtude);
    }
}