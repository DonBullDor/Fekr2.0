using Domain.Models;
using System.Collections.Generic;

namespace Service.Repository.Societes
{
    public interface ISocietesApiRepo
    {
        bool SaveChanges();
        IEnumerable<Societe> GetAllSocietes();

        Societe GetSociete(string id);

        void UpdateSociete(Societe societe);

        void CreateSociete(Societe societe);

        void DeleteSociete(string id);
    }
}
