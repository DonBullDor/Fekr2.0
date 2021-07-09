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

        public void DeleteModule(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EspModule> GetAllModules()
        {
            return _context.EspModule.ToList();
        }

        public EspModule GetModule(string id)
        {
            throw new NotImplementedException();
        }

        public void PostModule(EspModule espModule)
        {
            throw new NotImplementedException();
        }

        public void PutModule(string id, EspModule espModule)
        {
            throw new NotImplementedException();
        }
    }
}