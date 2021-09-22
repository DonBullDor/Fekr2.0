using Data;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Repository.Modules
{
    public class ModuleApiRepo : IModuleApiRepo
    {
        private readonly Oracle1Context _context;

        public ModuleApiRepo(Oracle1Context context)
        {
            _context = context;
        }

        public void DeleteModule(EspModule module)
        {
            if (module == null)
            {
                throw new ArgumentNullException(nameof(module));
            }
            _context.EspModule.Remove(module);
        }

        public IEnumerable<EspModule> GetAllModules()
        {
            return _context.EspModule.ToList();
        }

        public EspModule GetModule(string id)
        {
            return _context.EspModule.FirstOrDefault(p => p.CodeModule == id);
        }

        public IEnumerable<EspModule> GetModulesByClasse(string codeCl){
            return _context.EspModule.Where(module => module.EspModuleEtudiant.All(p => p.CodeCl == codeCl));
        }

        public void CreateModule(EspModule espModule)
        {
            if (espModule == null)
            {
                throw new ArgumentNullException(nameof(espModule));
            }
            _context.EspModule.Add(espModule);
        }

        public void UpdateModule(EspModule espModule)
        {
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}