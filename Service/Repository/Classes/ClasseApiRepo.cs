
using Data;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repository.Classes
{
    public class ClasseApiRepo : IClassesApiRepo
    {
        private readonly Oracle1Context _context;

        public ClasseApiRepo(Oracle1Context context)
        {
            _context = context;
        }
        public void DeleteClasse(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Classe> GetAllClasses()
        {
            return _context.Classe.ToList();
        }

        public Classe GetClasse(string id)
        {
            throw new NotImplementedException();
        }

        public void PostClasse(Classe espEtudiant)
        {
            throw new NotImplementedException();
        }

        public void PutClasse(string id, Classe espEtudiant)
        {
            throw new NotImplementedException();
        }
    }
}
