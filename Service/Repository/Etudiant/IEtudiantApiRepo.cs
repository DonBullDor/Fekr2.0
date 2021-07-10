using Domain.Models;
using System.Collections.Generic;

namespace Service.Repository
{
    public interface IEtudiantApiRepo
    {
        bool SaveChanges();
        IEnumerable<EspEtudiant> GetAllEtudiant();

        EspEtudiant GetEtudiant(string id);

        void UpdateEtudiant(EspEtudiant espEtudiant);

        void CreateEtudiant(EspEtudiant espEtudiant);

        void DeleteEtudiant(string id);
    }
}