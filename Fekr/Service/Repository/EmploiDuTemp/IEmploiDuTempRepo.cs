using Domain.Models;
using System.Collections.Generic;

namespace Service.Repository.EmploiDuTemp
{
    public interface IEmploiDuTempRepo
    {
        bool SaveChanges();
        IEnumerable<EspEmploi> GetAllEmploiDuTemp();
        
        EspEmploi GetEmploiDuTempByCodeModule(string codeModule);
        IEnumerable<EspEmploi> GetAllEmploiDuTempByCodeModule(string codeModule);
        IEnumerable<EspEmploi> GetAllEmploiDuTempByCodeModuleAndAnnee(string codeModule, string annee);
        IEnumerable<EspEmploi> GetAllEmploiDuTempByCodeModuleAndAnneeAndClasse(string codeModule, string annee, string classe);
        IEnumerable<EspEmploi> GetAllEmploiDuTempByCodeModuleAndAnneeAndNumSeance(string codeModule, string annee, decimal numSeance);
        IEnumerable<EspEmploi> GetAllEmploiDuTempByCodeModuleAndAnneeAndJour(string codeModule, string annee, string jour);
        IEnumerable<EspEmploi> GetAllEmploiDuTempByCodeModuleAndClasse(string codeModule, string classe);
        IEnumerable<EspEmploi> GetAllEmploiDuTempByCodeModuleAndNumSeance(string codeModule, decimal numSeance);
        IEnumerable<EspEmploi> GetAllEmploiDuTempByCodeModuleAndJour(string codeModule, string jour);
        IEnumerable<EspEmploi> GetAllEmploiDuTempByCodeModuleAndTypeSeance(string codeModule, string typeSeance);
        
        
        IEnumerable<EspEmploi> GetAllEmploiDuTempByClasse(string classe);
        IEnumerable<EspEmploi> GetAllEmploiDuTempByClasseAndAnnee(string classe, string annee);
        IEnumerable<EspEmploi> GetAllEmploiDuTempByClasseAndSemestre(string classe, decimal semestre);
        IEnumerable<EspEmploi> GetAllEmploiDuTempByClasseAndNumSeance(string classe, decimal numSeance);
        IEnumerable<EspEmploi> GetAllEmploiDuTempByClasseAndJour(string classe, string jour);
        IEnumerable<EspEmploi> GetAllEmploiDuTempByClasseAndTypeSeance(string classe, string typeSeance);

        IEnumerable<EspEmploi> GetAllEmploiDuTempByAnnee(string codeModule);
        IEnumerable<EspEmploi> GetAllEmploiDuTempByAnneeAndNumSeance(string codeModule, decimal numSeance);
        IEnumerable<EspEmploi> GetAllEmploiDuTempByAnneeAndSemestre(string codeModule, decimal semestre);
        
        IEnumerable<EspEmploi> GetAllEmploiDuTempBySemestre(decimal semestre);
        IEnumerable<EspEmploi> GetAllEmploiDuTempBySemestreAndJour(decimal semestre, string jour);
        
        /*
        IEnumerable<EspEmploi> GetAllEmploiByCritere(string[] criteres);
        */

        void UpdateEmploiDuTemp(EspEmploi emploi);

        void CreateEmploiDuTemp(EspEmploi emploi);

        void DeleteEmploiDuTemp(EspEmploi emploi);
    }
}