
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
        public void DeleteClasse(Classe classe)
        {
            if (classe == null)
            {
                throw new ArgumentNullException(nameof(classe));
            }
            _context.Classe.Remove(classe);
        }

        public IEnumerable<Classe> GetAllClasses()
        {
            return _context.Classe.ToList();
        }

        public Classe GetClasse(string id)
        {
            return _context.Classe.FirstOrDefault(p => p.CodeCl == id);
        }

        public void CreateClasse(Classe classe)
        {
            if (classe == null)
            {
                throw new ArgumentNullException(nameof(classe));
            }
            _context.Classe.Add(classe);
        }

        public void UpdateClasse(Classe espEtudiant)
        {
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
