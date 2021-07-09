using Domain.Models;
using System.Collections.Generic;

namespace Service.Repository.Enseignant
{
    public interface IEnseignantApiRepo
    {
        IEnumerable<EspEnseignant> GetAllEnseignants();

        EspEnseignant GetEnseignant(string id);

        void PutEnseignant(string id, EspEnseignant enseignant);

        void PostEnseignant(EspEnseignant enseignant);

        void DeleteEnseignant(string id);
    }
}
