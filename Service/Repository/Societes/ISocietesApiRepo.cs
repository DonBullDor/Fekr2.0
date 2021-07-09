using Domain.Models;
using System.Collections.Generic;

namespace Service.Repository.Societes
{
    public interface ISocietesApiRepo
    {
        IEnumerable<Societe> GetAllSocietes();

        Societe GetSociete(string id);

        void PutSociete(string id, Societe societe);

        void PostSociete(Societe societe);

        void DeleteSociete(string id);
    }
}
