using Domain.Models;
using System.Collections.Generic;

namespace Service.Repository
{
    public interface IEtudiantApiRepo
    {
        IEnumerable<EspEtudiant> GetAllEspEtudiant();

        EspEtudiant GetEspEtudiant(string id);

        void PutEspEtudiant(string id, EspEtudiant espEtudiant);

        void PostEspEtudiant(EspEtudiant espEtudiant);

        void DeleteEspEtudiant(string id);
    }
}