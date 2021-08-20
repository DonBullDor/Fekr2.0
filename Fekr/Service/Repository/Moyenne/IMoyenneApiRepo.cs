using Domain.Models;
using System.Collections.Generic;

namespace Service.Repository.Moyenne
{
    public interface IMoyenneApiRepo
    {
        bool SaveChanges();
        IEnumerable<AMoyenne> GetAllMoyenne();

        AMoyenne GetMoyenneById(string idEtudiant);

        void UpdateMoyenne(AMoyenne idEtudiant);

        void CreateMoyenne(AMoyenne idEtudiant);

        void DeleteMoyenne(AMoyenne idEtudiant);
    }
}
