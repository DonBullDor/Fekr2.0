using Domain.Models;
using System.Collections.Generic;

namespace Service.Repository.Decids
{
    public interface IDecidsApiRepo
    {
        IEnumerable<Decid> GetAllDecids();

        Decid GetDecid(string id);

        void PutDecid(string id, Decid decid);

        void PostDecid(Decid decid);

        void DeleteDecid(string id);
    }
}
