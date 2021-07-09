using Domain.Models;
using System.Collections.Generic;

namespace Service.Repository.Classes
{
    public interface IClassesApiRepo
    {
        IEnumerable<Classe> GetAllClasses();

        Classe GetClasse(string id);

        void PutClasse(string id, Classe espEtudiant);

        void PostClasse(Classe espEtudiant);

        void DeleteClasse(string id);
    }
}