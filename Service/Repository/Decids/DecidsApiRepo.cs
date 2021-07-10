using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Domain.Models;

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
            return _context.Decid.FirstOrDefault(p => p.IdDecid == id);
        }

        public void CreateDecid(Decid decid)
        {
            if (decid == null)
            {
                throw new ArgumentNullException(nameof(decid));
            }
            _context.Decid.Add(decid);
        }

        public void UpdateDecid(string id, Decid decid)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
