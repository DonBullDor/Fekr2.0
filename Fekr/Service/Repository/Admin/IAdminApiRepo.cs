using Domain.Models;
using System.Collections.Generic;

namespace Service.Repository.Decids
{
    public interface IAdminApiRepo
    {
        bool SaveChanges();
        
        IEnumerable<Decid> GetAllDecids();

        Decid GetDecid(string id);

        void UpdateDecid(Decid decid);

        void CreateDecid(Decid decid);

        void DeleteDecid(Decid decid);
    }
}
