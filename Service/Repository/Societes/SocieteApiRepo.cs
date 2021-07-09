using Data;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Repository.Societes
{
    public class SocieteApiRepo : ISocietesApiRepo
    {
        private readonly Oracle1Context _context;
        public SocieteApiRepo(Oracle1Context context)
        {
            _context = context;
        }
        public void DeleteSociete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Societe> GetAllSocietes()
        {
            return _context.Societe.ToList();
        }

        public Societe GetSociete(string id)
        {
            throw new NotImplementedException();
        }

        public void PostSociete(Societe societe)
        {
            throw new NotImplementedException();
        }

        public void PutSociete(string id, Societe societe)
        {
            throw new NotImplementedException();
        }
    }
}
