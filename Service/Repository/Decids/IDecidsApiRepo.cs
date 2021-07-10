using Domain.Models;
using System.Collections.Generic;

namespace Service.Repository.Decids
{
    public interface IDecidsApiRepo
    {
        bool SaveChanges();
        
        IEnumerable<Decid> GetAllDecids();

        Decid GetDecid(string id);

        void UpdateDecid(string id, Decid decid);

        void CreateDecid(Decid decid);

        void DeleteDecid(string id);
    }
}
