using Domain.Models;
using System.Collections.Generic;

namespace Service.Repository.Enseignant
{
    public interface IEnseignantApiRepo
    {
        bool SaveChanges();
        IEnumerable<EspEnseignant> GetAllEnseignants();

        EspEnseignant GetEnseignant(string id);

        void UpdateEnseignant(string id, EspEnseignant enseignant);

        void CreateEnseignant(EspEnseignant enseignant);

        void DeleteEnseignant(string id);
    }
}
