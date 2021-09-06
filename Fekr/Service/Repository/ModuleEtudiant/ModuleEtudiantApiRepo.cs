using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repository.ModuleEtudiant
{
    public class ModuleEtudiantApiRepo : IModuleEtudiant
    {
        private readonly Oracle1Context _context;

        public ModuleEtudiantApiRepo(Oracle1Context context)
        {
            _context = context;
        }
        public void CreateModuleEtudiant(EspModuleEtudiant espModuleEtudiant)
        {
            if (espModuleEtudiant == null)
            {
                throw new ArgumentNullException(nameof(espModuleEtudiant));
            }
            _context.EspModuleEtudiant.Add(espModuleEtudiant);
        }

        public void DeleteModuleEtudiant(EspModuleEtudiant module)
        {
            if (module == null)
            {
                throw new ArgumentNullException(nameof(module));
            }
            _context.EspModuleEtudiant.Remove(module);
        }

        public IEnumerable<EspModuleEtudiant> GetAllModulesEtudiant()
        {
            return _context.EspModuleEtudiant.ToList();
        }

        public EspModuleEtudiant GetModuleEtudiant(string id)
        {
            return _context.EspModuleEtudiant.FirstOrDefault(p => p.CodeCl == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateModuleEtudiant(EspModuleEtudiant EspModuleEtudiant)
        {
        }
    }
}
