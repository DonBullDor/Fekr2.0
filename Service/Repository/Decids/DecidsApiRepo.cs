using Data;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Repository.Decids
{
    public class DecidsApiRepo : IDecidsApiRepo
    {
        private readonly Oracle1Context _context;
        public DecidsApiRepo(Oracle1Context context)
        {
            _context = context;
        }
        public void DeleteDecid(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Decid> GetAllDecids()
        {
            return _context.Decid.ToList();
        }

        public Decid GetDecid(string id)
        {
            throw new NotImplementedException();
        }

        public void PostDecid(Decid decid)
        {
            throw new NotImplementedException();
        }

        public void PutDecid(string id, Decid decid)
        {
            throw new NotImplementedException();
        }
    }
}
