using Domain.Models;
using System.Collections.Generic;

namespace Service.Repository.Classes
{
    public interface IClassesApiRepo
    {
        bool SaveChanges();
        IEnumerable<Classe> GetAllClasses();

        Classe GetClasse(string id);

        void UpdateClasse(Classe classe);

        void CreateClasse(Classe classe);

        void DeleteClasse(string id);
    }
}